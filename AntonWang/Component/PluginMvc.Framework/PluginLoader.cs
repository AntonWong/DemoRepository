﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginMvc.Framework
{
    /// <summary>
    ///     插件加载器。
    /// </summary>
    public static class PluginLoader
    {
        #region Const

        public static string InstalledPluginsFilePath =
            Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"App_Data\InstalledPlugins.txt");

        public static string PluginsPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            @"Plugins");

        public static string ShadowCopyPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            @"App_Data\Plugins");

        #endregion

        /// <summary>
        ///     插件目录。
        /// </summary>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        ///     插件临时目录。
        /// </summary>
        private static readonly DirectoryInfo TempPluginFolder;

        private static readonly List<string> FrameworkPrivateBinFiles;

        /// <summary>
        ///     初始化。
        /// </summary>
        static PluginLoader()
        {
            PluginFolder = new DirectoryInfo(PluginsPath);
            TempPluginFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
#if DEBUG
            TempPluginFolder = new DirectoryInfo(ShadowCopyPath);
#endif
            var FrameworkPrivateBin = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            FrameworkPrivateBinFiles = FrameworkPrivateBin.GetFiles().Select(p => p.Name).ToList();
        }

        /// <summary>
        ///     加载插件。
        /// </summary>
        public static IEnumerable<PluginDescriptor> Load()
        {
            IList<string> installedPluginSystemNames =
                PluginFileParser.ParseInstalledPluginsFile(GetInstalledPluginsFilePath());

            var plugins = new List<PluginDescriptor>();

            if (PluginFolder == null)
                throw new ArgumentNullException("pluginFolder");

            foreach (DirectoryInfo pluginFolder in PluginFolder.GetDirectories())
            {
                if (installedPluginSystemNames.Count > 0 &&
                    installedPluginSystemNames.Contains(pluginFolder.Name) == false)
                {
                    continue;
                }
                string descriptionFilepath = Path.Combine(pluginFolder.FullName, "Description.txt");
                if (File.Exists(descriptionFilepath))
                {
                    PluginDescriptor pluginDescriptor = PluginFileParser.ParsePluginDescriptionFile(descriptionFilepath);
                    pluginDescriptor.Name = pluginFolder.Name;
                    plugins.Add(pluginDescriptor);
                }
                else
                {
                    var pluginDescriptor = new PluginDescriptor();
                    pluginDescriptor.Name = pluginFolder.Name;
                    plugins.Add(pluginDescriptor);
                }
            }
            plugins.Sort((firstPair, nextPair) => firstPair.DisplayOrder.CompareTo(nextPair.DisplayOrder));

            //程序集复制到临时目录。
            CopyToTempPluginFolderDirectory(plugins);

            //加载 bin 目录下的所有程序集。
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //加载临时目录下的所有程序集。
            assemblies =
                assemblies.Union(
                    TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                        .Select(x => Assembly.LoadFile(x.FullName))
                        .ToList());
            InitPlugins(assemblies, plugins);

            return plugins.Where(p => p.Plugin != null);
        }

        /// <summary>
        ///     Gets the full path of InstalledPlugins.txt file
        /// </summary>
        /// <returns></returns>
        public static string GetInstalledPluginsFilePath()
        {
            string filePath = InstalledPluginsFilePath;
            return filePath;
        }

        /// <summary>
        ///     获得插件信息。
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static IPlugin GetPluginInstance(Type pluginType, Assembly assembly, IEnumerable<Assembly> assemblies)
        {
            if (pluginType != null)
            {
                var plugin = (IPlugin) Activator.CreateInstance(pluginType);
                return plugin;
                //if (plugin != null)
                //{
                //    var assems = assemblies.Where(p => plugin.DependentAssembly.Contains(p.GetName().Name)).ToList();
                //    return new PluginDescriptor(plugin, assembly, assembly.GetTypes(), assems);//
                //}
            }

            return null;
        }

        /// <summary>
        ///     程序集复制到临时目录。
        /// </summary>
        private static void CopyToTempPluginFolderDirectory(List<PluginDescriptor> pluginDescriptions)
        {
            Directory.CreateDirectory(PluginFolder.FullName);
            Directory.CreateDirectory(TempPluginFolder.FullName);

            //清理临时文件。
            Debug.WriteLine("清理临时文件");
            IEnumerable<FileInfo> pluginsTemp =
                TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                    .Where(p => FrameworkPrivateBinFiles.Contains(p.Name) == false);
            foreach (FileInfo file in pluginsTemp)
            {
                try
                {
                    Debug.WriteLine(file.FullName);
                    file.Delete();
                }
                catch (Exception)
                {
                }
            }

            //复制插件进临时文件夹。
            foreach (PluginDescriptor plugin in pluginDescriptions)
            {
                string[] PluginFileNames = plugin.PluginFileName == null
                    ? new string[] {}
                    : plugin.PluginFileName.Split(',');
                var dir = new DirectoryInfo(Path.Combine(PluginFolder.FullName, Path.Combine(plugin.Name, "bin")));
                FileInfo[] list = dir.GetFiles("*.dll", SearchOption.AllDirectories);
                var plugindlls = new List<FileInfo>();
                foreach (FileInfo item in list)
                {
                    if ((PluginFileNames.Length > 0 && PluginFileNames.Contains(item.Name) ||
                         PluginFileNames.Length == 0) && FrameworkPrivateBinFiles.Contains(item.Name) == false)
                        plugindlls.Add(item);
                }
                foreach (FileInfo plugindll in plugindlls)
                {
                    try
                    {
                        string srcPath = plugindll.FullName;
                        string toPath = Path.Combine(TempPluginFolder.FullName, plugindll.Name);
#if DEBUG
                        Debug.WriteLine(string.Format("from:\t{0}", srcPath));
                        Debug.WriteLine(string.Format("to:\t{0}", toPath));
#endif
                        File.Copy(srcPath, toPath, true);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        ///     根据程序集列表获得该列表下的所有插件信息。
        /// </summary>
        /// <param name="assemblies">程序集列表</param>
        /// <returns>插件信息集合。</returns>
        private static void InitPlugins(IEnumerable<Assembly> assemblies, List<PluginDescriptor> descriptors)
        {
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    IEnumerable<Type> pluginTypes =
                        assembly.GetTypes()
                            .Where(
                                type =>
                                    type.GetInterface(typeof (IPlugin).Name) != null && type.IsClass && !type.IsAbstract);

                    foreach (Type pluginType in pluginTypes)
                    {
                        if (pluginType != null)
                        {
                            var plugin = (IPlugin) Activator.CreateInstance(pluginType);
                            PluginDescriptor descriptor = descriptors.Where(p => p.Name == plugin.Name).FirstOrDefault();
                            if (descriptor != null)
                            {
                                descriptor.Init(plugin, assemblies);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(assembly.FullName);
                    Debug.WriteLine(ex.Message);
                    //    throw ex;
                }
            }
        }
    }
}
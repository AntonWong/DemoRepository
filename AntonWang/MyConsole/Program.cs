using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Extension;
using System.Diagnostics;
using System.Threading;

namespace MyConsole
{
    class Program111111
    {

        static int threadID = 0;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);


        static void Main1(string[] args)
        {
            Console.Out.WriteLine("Forking off threads...");
            for (int i = 0; i < 2; i++)
            {
                Thread t = new Thread(RunInfinite);
                t.Start();
                Console.Out.WriteLine("Thread " + t.Name + "(ManagedThreadId: " + t.ManagedThreadId + ") created!");
            }

            ProcessThreadCollection processThreads = Process.GetCurrentProcess().Threads;
            Console.Out.WriteLine("=> Total threads: " + processThreads.Count);
            foreach (ProcessThread pt in processThreads)
            {
                int timerSeconds = 2;
                while (timerSeconds-- > 0)
                {
                    Console.Out.Write(" Seconds before thread " + pt.Id + " dies: " + timerSeconds);
                    System.Threading.Thread.Sleep(1000);
                }



                IntPtr ptrThread = OpenThread(1, false, (uint)pt.Id);
                if (AppDomain.GetCurrentThreadId() != pt.Id)
                {
                    try
                    {
                        TerminateThread(ptrThread, 1);
                        Console.Out.Write(". Thread killed.");
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.ToString());
                    }
                }
                else
                    Console.Out.Write(". Not killing... It's the current thread!");
            }
            Console.Out.WriteLine("=> Total threads now: " + Process.GetCurrentProcess().Threads.Count);
            Console.ReadLine();
            //C# ManagedThreadId kill Thread
        }




        public static void RunInfinite()
        {
           
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString());
                System.Threading.Thread.Sleep(100);
                
            }
        }


    }
}

    #region 属性赋值
    public class CopyProperty1
    {
        public static void Copy()
        {
            A a = new A { Name = "aa", Age = 1 };
            B b = new B();
            a.CopyTo(b);
        }

    }
    public class A
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class B
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
    #endregion

    #region 条件组合
    public class Add
    {
        class CC
        {
            public int A1 { get; set; }
        }
        public static void AndAlso()
        {
            List<CC> list = new List<CC> {new CC {A1 = 1}, new CC {A1 = 2}, new CC {A1 = 3}, new CC {A1 = 4}};
            Expression<Func<CC, bool>> func = a => a.A1 == 1;
            Expression<Func<CC, bool>> fun2 = a => a.A1 == 3;
            //func = func.AndInvoke(fun2);
            func = func.And(fun2);
            var aaaa = list.AsQueryable().Where(func).ToList();
            Console.WriteLine("符合组合条件的数量是："+aaaa.Count);
            Console.ReadKey();
        }
    }
    #endregion
	//窗口


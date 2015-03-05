using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyConsole.Common
{
    public class Paper
    {
        /// <summary>
        /// 并行分页处理数据，提高系统利用率，提升系统性能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="method"></param>
        public static async Task DataPageProcessAsync<T>(
            IQueryable<T> item,
            Action<IEnumerable<T>> method) where T : class
        {
            await Task.Run(() =>
            {
                DataPageProcess(item, method);
            });
        }

        /// <summary>
        /// 在主线程上分页处理数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="method"></param>
        public static void DataPageProcess<T>(
            IQueryable<T> item,
            Action<IEnumerable<T>> method) where T : class
        {
            if (item != null && item.Count() > 0)
            {
                const int dataPageSize = 100;
                int dataTotalCount = item.Count();
                int dataTotalPages = item.Count()/dataPageSize;
                if (dataTotalCount%dataPageSize > 0)
                    dataTotalPages += 1;

                for (int pageIndex = 1; pageIndex <= dataTotalPages; pageIndex++)
                {
                    var currentItems = item.Skip((pageIndex - 1)*dataPageSize).Take(dataPageSize).ToList();
                    method(currentItems);
                }
            }
        }
    }
}

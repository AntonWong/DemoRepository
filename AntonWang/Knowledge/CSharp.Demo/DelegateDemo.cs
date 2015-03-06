using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Demo
{
    public delegate int CalculateDelegate(int x,int y);

    public delegate void Action();

    //系统封装的Action<T>委托，有参数但是没有返回值。
    public delegate int Action<in T1, in T2>(T1 arg1, T2 arg2);

    public class DelegateDemo
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int GetResult()
        {
            int s = 0;
            //原始方法  
            //CalculateDelegate calculateDelegate = new CalculateDelegate(Add);
            //calculateDelegate(1,2);
            //匿名委托
            //s = AnonymousMethod(1,2);
            Console.WriteLine(s);
            return s;
        }

        public static void GetAction()
        {
            //原始方法  
            /*
            Action action = new Action(Method);
            action();
            */
            //匿名委托方法
            //Action actionAnonymous = () => Console.WriteLine("i am is a action2");
            //actionAnonymous();
            GetActionAnonymous();
        }

        public static void GetActionAnonymous()
        {
            //Action<int, int> action = (i, i1) =>
            //{
            //    Console.WriteLine("i am is a action1");
            //};

            Action<int, int> action = (x, y) =>
            {
                Console.WriteLine("i am is a action");
                return x + y;
            };
            action(2, 3);

           int s  =  action(1,2);
            Console.WriteLine(s);

        }


        //匿名方法
        public static int AnonymousMethod(int a,int b)
        {
            CalculateDelegate calculateDelegate = (x, y) => x + y;
            return calculateDelegate(a, b);
        }

        private static void Method()
        {
            Console.WriteLine("i am is a action1");
        }
        private static int MethodParam(int x,int y)
        {
            
            Console.WriteLine("i am is a action1");
            return x + y;
        }
        //Action<T>
    }
    
}

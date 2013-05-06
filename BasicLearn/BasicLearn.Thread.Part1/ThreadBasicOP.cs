using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace BasicLearn.Thread.Part1
{
    public delegate int[] ShowMSGDelegate(int A);

    public class ThreadBasicOP
    {
        //声明锁标识
        private static object async = new object();

        //观察打印结果
        public void printInfo()
        {
            Console.WriteLine("MainThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            System.Threading.Thread[] testThread = new System.Threading.Thread[10];
            for (int i = 0; i < 10; i++)
            {
                testThread[i] = new System.Threading.Thread(new ThreadStart(Add));
            }
            for (int i = 0; i < 10; i++)
            {
                testThread[i].Start();
            }
            Console.ReadKey();
        }
        //常规多线程调用,会导致数据访问不安全
        private void Add()
        {
            Console.WriteLine("CurrentThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Random random = new Random();
            int sec = 200 * random.Next(5);
            System.Threading.Thread.Sleep(sec);
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
        }
        //采用lock语法糖方式进行同步,编译为IL其实是一样的
        private void Add2()
        {
            //这里如果是public的话,可以采用this,如果static对象和方法,就需要用声明一个object的变量来做锁标识
            lock (this)// == lock (async)
            {
                Console.WriteLine("CurrentThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
                Random random = new Random();
                int sec = 200 * random.Next(5);
                System.Threading.Thread.Sleep(sec);
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
            }
        }
        //采用Monitor方式进行同步,和lock类似
        public void Add3()
        {
            //获取排他锁
            Monitor.Enter(async);
            try
            {
                Console.WriteLine("CurrentThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
                Random random = new Random();
                int sec = 100 * random.Next(5);
                System.Threading.Thread.Sleep(sec);
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
            }
            catch
            {
            }
            finally
            {
                //释放排他锁
                Monitor.Exit(async);
            }
        }
    }
}

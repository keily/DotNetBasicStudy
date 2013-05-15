using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicLearn.Thread.Part1
{
    // 多播委托式 多线程调用
    public class MuticastDelegateThread
    {
        //声明调用子线程的委托
        public ShowMSGDelegate ComputeDelegate;
        //监控子线程调用过程运算结果的变量
        private int WatchVarInter = 0;
        //异步方式
        public void printInfo()
        {
            Console.WriteLine("MainThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //初始化委托
            ComputeDelegate = new ShowMSGDelegate(Fibonacci);
            //声明并初始化一个异步回调，完成后会调用CheckedCompeleted
            AsyncCallback callback = new AsyncCallback(CheckedCompeleted);
            //开始多线程调用
            IAsyncResult sayncResult = ComputeDelegate.BeginInvoke(9, callback, "hi,this is customize message!");

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Done!");

            Console.ReadKey();
        }
        //同步方式
        public void printInfo2()
        {
            Console.WriteLine("MainThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //初始化委托
            ComputeDelegate = new ShowMSGDelegate(Fibonacci);
            //声明并初始化一个异步回调，完成后会调用CheckedCompeleted
            IAsyncResult sayncResult = ComputeDelegate.BeginInvoke(9, null, null);
            //判断子线程是否完成，异步方式
            while (!sayncResult.IsCompleted)
            {
                //do other something
                Console.WriteLine("Current Value:" + WatchVarInter);
            }
            //结束委托调用，返回子线程调用结果
            int[] callResult = ComputeDelegate.EndInvoke(sayncResult);
            Console.WriteLine("Last value:" + callResult[0]);
            Console.WriteLine("Done!");

            Console.ReadKey();
        }
        //阻塞当前线程
        public void printInfo3()
        {
            Console.WriteLine("MainThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //初始化委托
            ComputeDelegate = new ShowMSGDelegate(Fibonacci);
            //声明并初始化一个异步回调，完成后会调用CheckedCompeleted
            IAsyncResult sayncResult = ComputeDelegate.BeginInvoke(9, null, null);
            //阻塞当前线程，等待子线程调用结束
            sayncResult.AsyncWaitHandle.WaitOne();
            ////结束委托调用，返回子线程调用结果
            int[] callResult = ComputeDelegate.EndInvoke(sayncResult);
            Console.WriteLine("Last value:" + callResult[0]);
            Console.WriteLine("Done!");

            Console.ReadKey();
        }        
        //回调
        private void CheckedCompeleted(IAsyncResult ar)
        {
            //结束调用
            int[] result = ComputeDelegate.EndInvoke(ar);
            Console.WriteLine("compeleted! result:" + result[0] + ar.AsyncState.ToString());
        }

        //计算斐波拉契数列
        private int[] Fibonacci(int A)
        {
            Console.WriteLine("CurrentThread ManagedThreadId is :" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            if (A <= 1)
                return new int[] { A, 1 };
            else
            {
                int[] temp = Fibonacci(A - 1);
                //获取观察值
                WatchVarInter = temp[0] + temp[1];
                //线程挂起，编译观察
                System.Threading.Thread.Sleep(10);
                return new int[] { temp[0] + temp[1], temp[0] };
            }
        }
    }
}

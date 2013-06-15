using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace BasicLearn.Thread.Part1
{
    

    class Program
    {
        static void Main(string[] args)
        {
            /*
            //多线程显示方式实现
            ThreadBasicOP asyncthread = new ThreadBasicOP();
            //不带参数的多线程基本调用
            asyncthread.printInfo();
            //带参数的多线程调用
            asyncthread.printInfo2();
           
            //委托方式异步调用
            MuticastDelegateThread muticastDelegateThread = new MuticastDelegateThread();
            //线程异步方式
            muticastDelegateThread.printInfo();
            //线程异步方式，子线程判断
            muticastDelegateThread.printInfo2();
            //线程阻塞（同步）方式
            muticastDelegateThread.printInfo3();
             * */

            //System.Threading.Timer 方式
            //SafeThreadTimer stimer1 = new SafeThreadTimer();
            //System.Timers.Timer方式
            SafeTimersTimer stimer2 = new SafeTimersTimer();

            Console.ReadKey();
        }
    }   
    
}

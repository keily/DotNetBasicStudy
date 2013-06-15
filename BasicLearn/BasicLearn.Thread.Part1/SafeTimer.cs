using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicLearn.Thread.Part1
{
    /// <summary>
    /// System.Threading.Timer 方式
    /// </summary>
    public class SafeThreadTimer
    {
        //declare a timer
        private System.Threading.Timer timer;
        //counter
        private int i = 0;
        //current thread situation
        private bool flag = false;
        //declare a sync lock
        private static object syncLock = new object();
        
        public SafeThreadTimer()
        {
            init();
        }
        private void init()
        {
            //初始化timer，定时从线程池申请线程执行callback方法
            timer = new System.Threading.Timer(myCallback, null, 0, 500);
        }
        private void myCallback(object obj) 
        {
            System.Threading.Thread.CurrentThread.IsBackground = false;

            lock (syncLock)
            {                
                if (flag)
                    return;

                i++;
                if (i > 10)
                {
                    flag = true;
                    timer.Dispose();
                }

                var path=AppDomain.CurrentDomain.BaseDirectory + "Config\\Data.xml";
                var doc = new System.Xml.XmlDocument();
                doc.Load(path);

                var node = doc.SelectSingleNode("/Thread/Time");
                node.InnerText = DateTime.Now.ToString();
                node = doc.SelectSingleNode("/Thread/ThreadId");
                node.InnerText = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                node = doc.SelectSingleNode("/Thread/Counter");
                var counter = Int32.Parse(node.InnerText);
                node.InnerText = (++counter).ToString();
                                
                doc.Save(path);

                Console.WriteLine(string.Format("Outer Now:i={0},Current Thread:{1}",
                    i, System.Threading.Thread.CurrentThread.ManagedThreadId));
            }
            //sleep current thread
            System.Threading.Thread.Sleep(500);
        }
    }

    /// <summary>
    /// System.Timers.Timer方式
    /// </summary>
    public class SafeTimersTimer { 
        //declare a timer
        private System.Timers.Timer timer;
        //counter
        private int i = 0;
        //current thread situation
        private bool flag = false;
        //declare a sync lock
        private static object syncLock = new object();

        public SafeTimersTimer()
        {
            init();
        }
        private void init()
        {
            //初始化timer，定时从线程池申请线程执行callback方法
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.IsBackground = false;

            lock (syncLock)
            {
                if (flag)
                    return;

                i++;
                if (i > 10)
                {
                    flag = true;
                    timer.Stop();
                }

                var path = AppDomain.CurrentDomain.BaseDirectory + "Config\\Data.xml";
                var doc = new System.Xml.XmlDocument();
                doc.Load(path);

                var node = doc.SelectSingleNode("/Thread/Time");
                node.InnerText = DateTime.Now.ToString();
                node = doc.SelectSingleNode("/Thread/ThreadId");
                node.InnerText = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                node = doc.SelectSingleNode("/Thread/Counter");
                var counter = Int32.Parse(node.InnerText);
                node.InnerText = (++counter).ToString();

                doc.Save(path);

                Console.WriteLine(string.Format("Outer Now:i={0},Current Thread:{1}",
                    i, System.Threading.Thread.CurrentThread.ManagedThreadId));
            }
            //sleep current thread
            System.Threading.Thread.Sleep(500);
        }
        
    }
}

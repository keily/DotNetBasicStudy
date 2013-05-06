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
            ThreadBasicOP asyncthread = new ThreadBasicOP();
            asyncthread.printInfo();
            
            MuticastDelegateThread muticastDelegateThread = new MuticastDelegateThread();
            muticastDelegateThread.printInfo3();
        }
    }   
    
}

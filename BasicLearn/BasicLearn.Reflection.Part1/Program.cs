using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace BasicLearn.Reflection.Part1
{
    public class ReflectionClass {
        private int varIntVal = 100;
        public ReflectionClass(int paramOne) {
            varIntVal = paramOne;
        }
        public ReflectionClass():this(0)
        {
        }
        public void MethodOne(object _object) {
            Console.WriteLine("MethodOne invoked on object and private variable is {0}, method param is {1}",
                varIntVal, _object.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a Demo of BasicLearn.Reflection.Part1 ");
            ReflectionClass ReflectionClass1 = new ReflectionClass(1);
            ReflectionClass ReflectionClass2 = new ReflectionClass(2);

            Type _type = ReflectionClass1.GetType();
            MethodInfo method = _type.GetMethod("MethodOne");

            for (int i = 1; i <= 5; i++)
                method.Invoke(ReflectionClass2, new object[]{i});

            Console.ReadKey();
        }
    }
}

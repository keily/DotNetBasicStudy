using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Reflection.Emit;

namespace BasicLearn.Reflection.Part2_Emit
{
    // Declare a delegate type that can be used to execute the completed
    // dynamic method. 
    public delegate int HelloDelegate(string msg, int ret);
    public delegate int AddDelegate(int A, int B);
    public class MathMatic {
        public int Movebits(int A, int B)
        {
            return A*B;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DynamicCreateMethod();
            //DynamicCallMethod();
            DynamicCallMethodByDelegate();
            Console.ReadKey();
        }        
        static void DynamicCallMethodByDelegate() {
            MethodInfo method = typeof(MathMatic).GetMethod("Movebits");
            Type[] parameterType = { typeof(MathMatic), typeof(int), typeof(int) };
            DynamicMethod dynamicMethod = new DynamicMethod("Movebits", typeof(int), parameterType);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Callvirt, method);
            il.Emit(OpCodes.Ret);
            AddDelegate hellodelegate = (AddDelegate)dynamicMethod.CreateDelegate(typeof(AddDelegate));
            Console.WriteLine(hellodelegate(10, 3));
        }

        static void DynamicCreateMethod()
        {
            // Create an array that specifies the types of the parameters
            // of the dynamic method. This dynamic method has a String
            // parameter and an Integer parameter.
            Type[] helloArgs = { typeof(string), typeof(int) };

            // Create a dynamic method with the name "Hello", a return type
            // of Integer, and two parameters whose types are specified by
            // the array helloArgs. Create the method in the module that
            // defines the String class.
            DynamicMethod hello = new DynamicMethod("Hello",
                typeof(int),
                helloArgs,
                typeof(string).Module);

            // Create an array that specifies the parameter types of the
            // overload of Console.WriteLine to be used in Hello.
            Type[] writeStringArgs = { typeof(string) };
            // Get the overload of Console.WriteLine that has one String parameter.
            MethodInfo writeString = typeof(Console).GetMethod("WriteLine", writeStringArgs);

            // Get an ILGenerator and emit a body for the dynamic method,
            // using a stream size larger than the IL that will be emitted.
            ILGenerator il = hello.GetILGenerator(256);
            // Load the first argument, which is a string, onto the stack.
            il.Emit(OpCodes.Ldarg_0);
            // Call the overload of Console.WriteLine that prints a string.
            il.EmitCall(OpCodes.Call, writeString, null);
            // The Hello method returns the value of the second argument;
            // to do this, load the onto the stack and return.
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ret);
            
            // Display MethodAttributes for the dynamic method, set when 
            // the dynamic method was created.
            Console.WriteLine("\r\nMethod Attributes: {0}", hello.Attributes);

            // Display the calling convention of the dynamic method, set when the dynamic method was created.
            Console.WriteLine("\r\nCalling convention: {0}", hello.CallingConvention);

            // Display the declaring type, which is always null for dynamic methods.
            if (hello.DeclaringType == null)
                Console.WriteLine("\r\nDeclaringType is always null for dynamic methods.");
            else
                Console.WriteLine("DeclaringType: {0}", hello.DeclaringType);

            // Display the default value for InitLocals.
            if (hello.InitLocals)
                Console.Write("\r\nThis method contains verifiable code.");
            else
                Console.Write("\r\nThis method contains unverifiable code.");
            Console.WriteLine(" (InitLocals = {0})", hello.InitLocals);

            // Display the module specified when the dynamic method was created.
            Console.WriteLine("\r\nModule: {0}", hello.Module);

            // Display the name specified when the dynamic method was created.
            // Note that the name can be blank.
            Console.WriteLine("\r\nName: {0}", hello.Name);

            // For dynamic methods, the reflected type is always null.
            if (hello.ReflectedType == null)
                Console.WriteLine("\r\nReflectedType is null.");
            else
                Console.WriteLine("\r\nReflectedType: {0}", hello.ReflectedType);
            if (hello.ReturnParameter == null)
                Console.WriteLine("\r\nMethod has no return parameter.");
            else
                Console.WriteLine("\r\nReturn parameter: {0}", hello.ReturnParameter);
            // If the method has no return type, ReturnType is System.Void.
            Console.WriteLine("\r\nReturn type: {0}", hello.ReturnType);

            // ReturnTypeCustomAttributes returns an ICustomeAttributeProvider
            // that can be used to enumerate the custom attributes of the
            // return value. At present, there is no way to set such custom
            // attributes, so the list is empty.
            if (hello.ReturnType == typeof(void))
                Console.WriteLine("The method has no return type.");
            else
            {
                ICustomAttributeProvider caProvider = hello.ReturnTypeCustomAttributes;
                object[] returnAttributes = caProvider.GetCustomAttributes(true);
                if (returnAttributes.Length == 0)
                    Console.WriteLine("\r\nThe return type has no custom attributes.");
                else
                {
                    Console.WriteLine("\r\nThe return type has the following custom attributes:");
                    foreach (object attr in returnAttributes)
                    {
                        Console.WriteLine("\t{0}", attr.ToString());
                    }
                }
            }

            Console.WriteLine("\r\nToString: {0}", hello.ToString());

            // Add parameter information to the dynamic method. (This is not
            // necessary, but can be useful for debugging.) For each parameter,
            // identified by position, supply the parameter attributes and a 
            // parameter name.
            ParameterBuilder parameter1 = hello.DefineParameter(1,ParameterAttributes.In,"message");
            ParameterBuilder parameter2 = hello.DefineParameter(2,ParameterAttributes.In,"valueToReturn");

            // Display parameter information.
            ParameterInfo[] parameters = hello.GetParameters();
            Console.WriteLine("\r\nParameters: name, type, ParameterAttributes");
            foreach (ParameterInfo p in parameters)
            {
                Console.WriteLine("\t{0}, {1}, {2}",
                    p.Name, p.ParameterType, p.Attributes);
            }
            
            // Create a delegate that represents the dynamic method. 
            //This action completes the method, and any further attempts to change the method will cause an exception.
            HelloDelegate hi = (HelloDelegate)hello.CreateDelegate(typeof(HelloDelegate));

            // Use the delegate to execute the dynamic method.
            Console.WriteLine("\r\nUse the delegate to execute the dynamic method:");
            int retval = hi("\r\nHello, World!", 42);
            Console.WriteLine("Invoking delegate hi(\"Hello, World!\", 42) returned: " + retval);

            // Execute it again, with different arguments.
            retval = hi("\r\nHi, Mom!", 5280);
            Console.WriteLine("Invoking delegate hi(\"Hi, Mom!\", 5280) returned: " + retval);

            Console.WriteLine("\r\nUse the Invoke method to execute the dynamic method:");
            // Create an array of arguments to use with the Invoke method.
            object[] invokeArgs = { "\r\nHello, World!", 42 };
            // Invoke the dynamic method using the arguments. This is much
            // slower than using the delegate, because you must create an
            // array to contain the arguments, and value-type arguments
            // must be boxed.
            object objRet = hello.Invoke(null, BindingFlags.ExactBinding, null, invokeArgs, new System.Globalization.CultureInfo("en-us"));
            Console.WriteLine("hello.Invoke returned: " + objRet);
        }

        static void DynamicCallMethod()
        {
            AppDomain myDomain = AppDomain.CurrentDomain;
            AssemblyName asmName = new AssemblyName();
            asmName.Name = "MyDynamicAsm";

            AssemblyBuilder myAsmBuilder = myDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder myModule = myAsmBuilder.DefineDynamicModule("MyDynamicAsm", "MyDynamicAsm.dll");

            TypeBuilder myTypeBld = myModule.DefineType("MyDynamicType", TypeAttributes.Public);

            // Get info from the user to build the method dynamically.
            Console.WriteLine("Let's build a simple method dynamically!");
            Console.WriteLine("Please enter a few numbers, separated by spaces.");
            string inputNums = Console.ReadLine();
            Console.Write("Do you want to [A]dd (default) or [M]ultiply these numbers? ");
            string myMthdAction = Console.ReadLine().ToUpper();
            Console.Write("Lastly, what do you want to name your new dynamic method? ");
            string myMthdName = Console.ReadLine();

            // Process inputNums into an array and create a corresponding Type array 
            int index = 0;
            string[] inputNumsList = inputNums.Split();

            Type[] myMthdParams = new Type[inputNumsList.Length];
            object[] inputValsList = new object[inputNumsList.Length];


            foreach (string inputNum in inputNumsList)
            {
                inputValsList[index] = (object)Convert.ToInt32(inputNum);
                myMthdParams[index] = typeof(int);
                index++;
            }

            // Now, call the method building method with the parameters, passing the 
            // TypeBuilder by reference.
            DemoMethodBuilder.AddMethodDynamically(myTypeBld,
                                 myMthdName,
                                 myMthdParams,
                                 typeof(int),
                                 myMthdAction);

            Type myType = myTypeBld.CreateType();

            Console.WriteLine("---");
            Console.WriteLine("The result of {0} the inputted values is: {1}",
                              ((myMthdAction == "M") ? "multiplying" : "adding"),
                              myType.InvokeMember(myMthdName,
                              BindingFlags.InvokeMethod | BindingFlags.Public |
                              BindingFlags.Static,
                              null,
                              null,
                              inputValsList));
            Console.WriteLine("---");

            // Let's take a look at the method we created.
            // If you are interested in seeing the MSIL generated dynamically for the method
            // your program generated, change to the directory where you ran the compiled
            // code sample and type "ildasm MyDynamicAsm.dll" at the prompt. When the list
            // of manifest contents appears, click on "MyDynamicType" and then on the name of
            // of the method you provided during execution.

            myAsmBuilder.Save("MyDynamicAsm.dll");

            MethodInfo myMthdInfo = myType.GetMethod(myMthdName);
            Console.WriteLine("Your Dynamic Method: {0};", myMthdInfo.ToString());
        }
    }

    public class DemoMethodBuilder
    {
        public static void AddMethodDynamically(TypeBuilder myTypeBld,
                                                 string mthdName,
                                                 Type[] mthdParams,
                                                 Type returnType,
                                                 string mthdAction)
        {

            MethodBuilder myMthdBld = myTypeBld.DefineMethod(
                                                 mthdName,
                                                 MethodAttributes.Public |
                                                 MethodAttributes.Static,
                                                 returnType,
                                                 mthdParams);

            ILGenerator ILout = myMthdBld.GetILGenerator();
            int numParams = mthdParams.Length;
            for (byte x = 0; x < numParams; x++)
            {
                ILout.Emit(OpCodes.Ldarg_S, x);
            }

            if (numParams > 1)
            {
                for (int y = 0; y < (numParams - 1); y++)
                {
                    switch (mthdAction)
                    {
                        case "A": ILout.Emit(OpCodes.Add);
                            break;
                        case "M": ILout.Emit(OpCodes.Mul);
                            break;
                        default: ILout.Emit(OpCodes.Add);
                            break;
                    }
                }
            }
            ILout.Emit(OpCodes.Ret);
        }
    }
}

// See https://aka.ms/new-console-template for more information

using System;
using System.Buffers;

namespace CalculatorApp
{
    public class AppDriver
    {

        string prevNumber;
        string nextNumber;
        string? opType;

        private Dictionary<string, Type> operations;
        public AppDriver()
        {
            operations = new Dictionary<string, Type>
            {
                { "+", typeof(Add) },
                { "-", typeof(Subtract) },
                { "*", typeof(Multiply) },
                { "/", typeof(Divide) },
                { "^", typeof(PowerOf) }
            };

            run();
        }
        void run()
        {
            Console.WriteLine("Please enter the first number.");
            prevNumber = Console.ReadLine();
            while (true)
            {

                Console.WriteLine("Please enter the the operation you'd like to perform.");
                opType = Console.ReadLine();

                if (opType == "=")
                {
                    Console.WriteLine(prevNumber);
                    break;
                }

                Console.WriteLine("Please enter the next number.");
                nextNumber = Console.ReadLine();

                double prevNumberDoub = Convert.ToDouble(prevNumber);
                double nextNumberDoub = Convert.ToDouble(nextNumber);
                prevNumberDoub = PerformOperation(opType, prevNumberDoub, nextNumberDoub);

                Console.WriteLine(prevNumberDoub);

                prevNumber = Convert.ToString(prevNumberDoub);
            }
            
        }
        public static void Main()
        {
            //Starts and runs the whole program.
            Console.WriteLine("This is a calculator.");

            AppDriver appDriver = new AppDriver();
            
            
            
            }
        double PerformOperation(string opType, double prevNumber, double nextNumber)
        {
            if (operations.TryGetValue(opType, out Type operationType))
            {
                IOperation operation = (IOperation)Activator.CreateInstance(operationType);
                return operation.compareTwoNums(prevNumber, nextNumber);
            }
            else
            {
                throw new Exception("not a valid operator, please use: +, -, *, /");
            }
        }

    }
}

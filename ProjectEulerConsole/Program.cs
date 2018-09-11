using System;
using System.IO;
using System.Linq;

namespace ProjectEulerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 25);
            start:


            Console.Clear();
            Console.WriteLine("Enter the problem number you wish to solve: ");
            string input = Console.ReadLine();

            bool IsnInt = false;
            bool IsnLong = false;
            bool IsnStr = false;
            long n = 0;
            int nInt = 0;
            string[] arguments = input.Split(',');
            if (arguments.Length == 2)
            {
                if (int.TryParse(arguments[1], out nInt))
                {
                    IsnInt = true;
                }
                else if (long.TryParse(arguments[1], out n))
                {
                    IsnLong = true;
                }
                else
                {
                    IsnStr = true;
                }
            }

            int problemNum;
            if (int.TryParse(arguments[0], out problemNum))
            {
                string result;
                if (IsnInt)
                    result = (Problems.DoProblem(problemNum, nInt));
                else if (IsnLong)
                    result = (Problems.DoProblem(problemNum, n));
                else if (IsnStr)
                    result = (Problems.DoProblem(problemNum, arguments[1]));
                else if (arguments.Length > 2)
                {
                    object[] myParameters = new object[arguments.Length - 1];
                    for (int i = 1; i < arguments.Length; i++)
                    {
                        if (int.TryParse(arguments[i], out int intResult))
                            myParameters[i - 1] = intResult;
                        else if (long.TryParse(arguments[i], out long longResult))
                            myParameters[i - 1] = longResult;
                        else
                            myParameters[i - 1] = arguments[i];
                    }
                    result = (Problems.DoProblem(problemNum, myParameters));
                }
                else
                    result = (Problems.DoProblem(problemNum));

                Console.WriteLine(result);
                using (StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "/outputlog.txt", true))
                {
                    writer.WriteLine(input + ": " + result);
                }
            }
            else
            {
                Console.WriteLine("You did not enter a valid number.");
            }

            Console.WriteLine("Press e to exit or any other key do another problem.");
            if (Console.ReadKey().Key == ConsoleKey.E)
                goto end;
            else
                goto start;

            end:
            { }

        }


    }
}

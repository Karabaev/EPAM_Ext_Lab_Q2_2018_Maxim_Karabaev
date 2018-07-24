using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Task6.Tasks;
namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            int switcher = -1;
            WriteLine("Enter number: 1 - Task 6.1, 2 - Task 6.2");
            try
            {
                switcher = int.Parse(ReadLine());
            }
            catch (FormatException)
            {
                WriteLine("Enter please integer value");
            }

            switch (switcher)
            {
                case 1:
                    Task6_1 task6_1 = new Task6_1();
                    task6_1.DisplayResult();
                    break;
                case 2:
                    Task6_2 task6_2 = new Task6_2();
                    task6_2.Start();
                    break;
                default:
                    break;
            }

            ReadLine();
        }
    }
}

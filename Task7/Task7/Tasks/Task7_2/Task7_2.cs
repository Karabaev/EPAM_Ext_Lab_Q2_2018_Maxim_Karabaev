using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task7.Tasks.Task7_2
{
    class Task7_2
    {
        private string[] strs =
        {
             "1,35",
             "-1,35",
             "1",
             "fref",
             "1,3efsffs",
             "5436",
             "-5436"
        };

        public Task7_2()
        {
            Console.WriteLine("Task7.2\n===================================================");
            Result();
        }
        private void Result()
        {
            foreach (var item in strs)
            {
                WriteLine("\"{0}\", is positive integer: {1}", item, item.IsPositiveNumber());
            }
        }
    }
}

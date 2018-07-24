using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Tasks.Model;
using static System.Console;
namespace Task6.Tasks
{
    class Task6_2
    {
        static Data data;
        

        public Task6_2()
        {
            WriteLine("Task 6.2.");
            WriteLine("/////////////////////////////////");
            data = new Data();
        }

        public void Start()
        {
            data.StartTimer();
        }
        public void Stop()
        {
            data.StopTimer();
        }
    }
}

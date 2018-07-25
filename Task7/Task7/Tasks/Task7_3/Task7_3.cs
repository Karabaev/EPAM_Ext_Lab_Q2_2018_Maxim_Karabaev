using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;
namespace Task7.Tasks.Task7_3
{
    class Task7_3
    {
        private const int MinRandValue = -100, MaxRandValue = 100;
        private int[] array = new int[100000];
        private Logger logger;
        public Task7_3()
        {
            logger = new Logger();
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            WriteLine("Task7.2\n===================================================");
            Result();
        }
        private void Result()
        {
            
            SimpleSearchPositiveEntries();
            
        }

        private bool Condition(int x)
        {
            if (x > 0) return true;
            return false;
        }
        private void SimpleSearchPositiveEntries()
        {
            Stopwatch sw = new Stopwatch();
            WriteLine("Simple search (1 subtask).");
            sw.Start();
            //foreach (var item in array)
            //    if (Condition(item)) Write("{0}, ", item);
            sw.Stop();
            WriteLine();           
            logger.Write("Elapsed miliseconds to simple search: {0}", sw.ElapsedMilliseconds);
        }
    }
}

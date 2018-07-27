namespace Task7.Tasks.Task7_3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using static System.Console;

    internal class Task7_3
    {
        private const int MinRandValue = -100, MaxRandValue = 100; // min и max значения элементов массивов
        private const int ArraySize = 100000; // размер массива
        private const int CountTests = 1000; // количество тестов
        private Logger logger; // ссылка на объект для записи логов в файл 

        public Task7_3()
        {
            this.logger = new Logger();
            WriteLine("Task7.3\n===================================================");
            this.Result();
        }

        private delegate bool IsPositive(int x);

        /// <summary>
        /// Главный метод задания.
        /// </summary>
        private void Result()
        {
            string log = string.Format("Running {0} tests", CountTests);
            WriteLine(log);
            this.logger.Write(log);
            for (int i = 0; i < CountTests; i++)
            {
                log = string.Format("Test {0} started", i);
                WriteLine(log);
                this.logger.Write(log);
                this.SimpleSearchPositiveEntries(); // обычный вызов метода
                this.DelegateSearchPositiveEntries(this.Condition, "delegate"); // передача метода через делегат
                this.DelegateSearchPositiveEntries(
                delegate(int x)
                {
                    if (x > 0)
                    {
                        return true;
                    }

                    return false;
                },
                "anonymous method"); // передача метода через анониный метод
                this.DelegateSearchPositiveEntries(
                    (x) => x > 0,
                    "lambda expression"); // передача через лямбда выражение
                this.LinqSearchPositiveEntries(); // поиск при помощи Linq
            }

            log = "The end of the tests";
            WriteLine(log);
            this.logger.Write(log);
        }

        /// <summary>
        /// Условие
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool Condition(int x)
        {
            if (x > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод подзадания 1 
        /// </summary>
        private void SimpleSearchPositiveEntries()
        {
            int[] array = new int[ArraySize];
            int[] resultArray = new int[ArraySize];
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            }

            Stopwatch sw = new Stopwatch();
            int j = 0;
            sw.Start();
            for (int i = 0; i < array.Length; i++)
            {
                if (this.Condition(array[i]))
                {
                    resultArray[j] = array[i];
                    j++;
                }
            }

            sw.Stop();
            this.logger.Write("Elapsed ticks to simple search: {0}", sw.ElapsedTicks);
        }

        /// <summary>
        /// Метод подзаданий 2,3,4
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="subtaskName"></param>
        private void DelegateSearchPositiveEntries(IsPositive condition, string subtaskName)
        {
            int[] array = new int[ArraySize];
            int[] resultArray = new int[ArraySize];
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            }

            Stopwatch sw = new Stopwatch();
            int j = 0;
            sw.Start();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    resultArray[j] = array[i];
                    j++;
                }
            }

            sw.Stop();
            this.logger.Write(string.Format("Elapsed ticks to {0} search: {1}", subtaskName, sw.ElapsedTicks));
        }

        /// <summary>
        /// Метод подзадания 5
        /// </summary>
        private void LinqSearchPositiveEntries()
        {
            int[] array = new int[ArraySize];
            IEnumerable<int> resultArray;
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            resultArray = array.Where(x => x > 0);
            sw.Stop();
            this.logger.Write("Elapsed ticks to linq search: {0}", sw.ElapsedTicks);
        }
    }
}

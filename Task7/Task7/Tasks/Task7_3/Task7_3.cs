namespace Task7.Tasks.Task7_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using static System.Console;
    class Task7_3
    {
        private const int MinRandValue = -100, MaxRandValue = 100; // min и max значения элементов массивов
        private const int arraySize = 100000; //размер массива
        private const int countTests = 1000; // количество тестов
        private Logger logger; // ссылка на объект для записи логов в файл 

        private delegate bool IsPositive(int x);
        public Task7_3()
        {
            logger = new Logger();
            WriteLine("Task7.3\n===================================================");
            Result();
        }
        /// <summary>
        /// Главный метод задания.
        /// </summary>
        private void Result()
        {
            string log = string.Format("Running {0} tests", countTests);
            WriteLine(log);
            logger.Write(log);
            for (int i = 0; i < countTests; i++)
            {
                log = string.Format("Test {0} started", i);
                WriteLine(log);
                logger.Write(log);
                SimpleSearchPositiveEntries(); // обычный вызов метода
                DelegateSearchPositiveEntries(Condition, "delegate"); // передача метода через делегат
                DelegateSearchPositiveEntries( // передача метода через анониный метод
                                                delegate (int x)
                                                {
                                                    if (x > 0) return true;
                                                    return false;
                                                },
                                                "anonymous method");
                DelegateSearchPositiveEntries((x) => x > 0, // передача через лямбда выражение
                                                "lambda expression");
                LinqSearchPositiveEntries(); // поиск при помощи Linq
            }
            log = "The end of the tests";
            WriteLine(log);
            logger.Write(log);
        }
        /// <summary>
        /// Условие
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool Condition(int x)
        {
            if (x > 0) return true;
            return false;
        }
        /// <summary>
        /// Метод подзадания 1 
        /// </summary>
        private void SimpleSearchPositiveEntries()
        {
            int[] array = new int[arraySize];
            int[] resultArray = new int[arraySize];
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            Stopwatch sw = new Stopwatch();
            int j = 0;
            sw.Start();
            for (int i = 0; i < array.Length; i++)
            {
                if (Condition(array[i]))
                {
                    resultArray[j] = array[i];
                    j++;
                }
            }
            sw.Stop();
            logger.Write("Elapsed ticks to simple search: {0}", sw.ElapsedTicks);
        }
        /// <summary>
        /// Метод подзаданий 2,3,4
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="subtaskName"></param>
        private void DelegateSearchPositiveEntries(IsPositive condition, string subtaskName)
        {
            int[] array = new int[arraySize];
            int[] resultArray = new int[arraySize];
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(MinRandValue, MaxRandValue);
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
            logger.Write(string.Format("Elapsed ticks to {0} search: {1}", subtaskName, sw.ElapsedTicks));
        }
        /// <summary>
        /// Метод подзадания 5
        /// </summary>
        private void LinqSearchPositiveEntries()
        {
            int[] array = new int[arraySize];
            IEnumerable<int> resultArray;
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(MinRandValue, MaxRandValue);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            resultArray = array.Where(x => x > 0);
            sw.Stop();
            logger.Write("Elapsed ticks to linq search: {0}", sw.ElapsedTicks);
        }
    }
}

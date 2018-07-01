namespace Task3.Model
{
    using System;
    using static System.Console;
    class Task3_7 : Task
    {
        int[] massive = new int[ElementNumber];
        int maxElem = int.MinValue, minElem = int.MaxValue;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_7()
        {
            Switcher = Switcher.Task3_7;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("Max = {0}, Min = {1}", maxElem, minElem);
            WriteLine("Sorted massive: ");
            DisplayMassive();
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            MassiveInit();
            WriteLine("Display original massive? {0} / {1}", YesDialog, NotDialog);
            /*Я решил не обрабатывать случаи, когда пользователь вводит не y или n.
            А просто считать за n все вводимые строки, кроме y.*/
            if (ReadLine() == YesDialog)
                DisplayMassive();
            GetMinMaxElements();
            Sort();
        }
        /// <summary>
        /// Инициализирет массив случайными значениями.
        /// </summary>
        private void MassiveInit()
        {
            Random rand = new Random();
            for (int i = 0; i < ElementNumber; i++)
                massive[i] = rand.Next(MinRand, MaxRand);
        }
        /// <summary>
        /// Выводит в консоль массив.
        /// </summary>
        private void DisplayMassive()
        {
            foreach (var item in massive)
                Write("{0} ", item);
            WriteLine();
        }
        /// <summary>
        /// Ищет максимальное и минимальное значения в массиве.
        /// </summary>
        private void GetMinMaxElements()
        {
            maxElem = int.MinValue;
            minElem = int.MaxValue;
            foreach (var item in massive)
            {
                if (item > maxElem)
                    maxElem = item;
                if (item < minElem)
                    minElem = item;
            }
        }
        /// <summary>
        /// Сортирует массив.
        /// </summary>
        private void Sort()
        {
            int buffer = 0;
            for (int i = 0; i < massive.Length; i++) // сортировать по убыванию
            {
                for (int j = 0; j < massive.Length; j++)
                {
                    if (massive[i] > massive[j])
                    {
                        buffer = massive[i];
                        massive[i] = massive[j];
                        massive[j] = buffer;
                    }
                }
            }
        }
    }
}

namespace Task3.Model
{
    using System;
    using static System.Console;
    /*Я хотел создать класс предок для задач с массивами, но не хватит времени.*/
    class Task3_9 : Task
    {
        int[] massive = new int[ElementNumber];
        int sum = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_9()
        {
            Switcher = Switcher.Task3_9;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("Sum of positive elements = {0}", sum);
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            MassiveInit();
            WriteLine("Display original massive? {0} / {1}", YesDialog, NotDialog);
            if (ReadLine() == YesDialog)
                DisplayMassive();
            SumPositiveNumbers();
        }
        /// <summary>
        /// Инициализирует массив.
        /// </summary>
        private void MassiveInit() 
        {
            Random rand = new Random();
            for (int i = 0; i < ElementNumber; i++)
                massive[i] = rand.Next(MinRand, MaxRand); 
        }
        /// <summary>
        /// Выводит массив в консоль.
        /// </summary>
        private void DisplayMassive()
        {
            for (int i = 0; i < ElementNumber; i++)
                Write("{0} ", massive[i]);
            WriteLine();
        }
        /// <summary>
        /// Считает сумму неотрицательных элементов массива.
        /// </summary>
        private void SumPositiveNumbers()
        {
            sum = 0;
            foreach (var item in massive)
                if (item >= 0)
                    sum += item;
        }

    }
}

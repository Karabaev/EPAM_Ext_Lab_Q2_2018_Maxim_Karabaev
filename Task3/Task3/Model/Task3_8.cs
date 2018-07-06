namespace Task3.Model
{
    using System;
    using static System.Console;
    class Task3_8 : Task
    {
        int[,,] massive = new int[ElementNumber, ElementNumber, ElementNumber];
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_8()
        {
            Switcher = Switcher.Task3_8;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("Display modified massive? {0} / {1}", YesDialog, NotDialog);
            /*Я решил не обрабатывать случаи, когда пользователь вводит не y или n.
            А просто считать за n все вводимые варианты, кроме y.*/
            if (ReadLine().ToLower() == YesDialog)
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
            if (ReadLine().ToLower() == YesDialog)
                DisplayMassive();
            ReplacePositiveNumbers();
        }
        /// <summary>
        /// Инициализирует массив.
        /// </summary>
        private void MassiveInit()
        {
            Random rand = new Random();
            for (int i = 0; i < ElementNumber; i++)
            {
                for (int j = 0; j < ElementNumber; j++)
                {
                    for (int z = 0; z < ElementNumber; z++)
                        massive[i, j, z] = rand.Next(MinRand, MaxRand);
                }
            }
        }
        /// <summary>
        /// Выводит массив в консоль.
        /// </summary>
        private void DisplayMassive()
        {
            for (int i = 0; i < ElementNumber; i++)
            {
                for (int j = 0; j < ElementNumber; j++)
                {
                    for (int z = 0; z < ElementNumber; z++)
                        Write("{0} ", massive[i, j, z]);
                    WriteLine();
                }
                WriteLine();
            }
        }
        /// <summary>
        /// Заменяет положительные числа нулями.
        /// </summary>
        private void ReplacePositiveNumbers()
        {
            for (int i = 0; i < ElementNumber; i++)
                for (int j = 0; j < ElementNumber; j++)
                    for (int z = 0; z < ElementNumber; z++)
                        if (massive[i, j, z] > 0)
                            massive[i, j, z] = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task3.Model
{
    class Task3_10 : Task
    {
        int[,] massive = new int[ElementNumber, ElementNumber];
        int sum = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_10()
        {
            Switcher = Switcher.Task3_10;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("Sum of elements on even positions = {0}", sum);
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
            SumEvenPosElems();
        }
        /// <summary>
        /// Инициализирует массив.
        /// </summary>
        private void MassiveInit()
        {
            Random rand = new Random();
            for (int i = 0; i < ElementNumber; i++)
                for (int j = 0; j < ElementNumber; j++)
                    massive[i, j] = rand.Next(MinRand, MaxRand);
        }
        /// <summary>
        /// Выводит массив в консоль.
        /// </summary>
        private void DisplayMassive()
        {
            for (int i = 0; i < ElementNumber; i++)
            {
                for (int j = 0; j < ElementNumber; j++)
                    Write("{0} ", massive[i, j]);
                WriteLine();
            }
        }
        /// <summary>
        /// Считает сумму элементов, находящихся на четных позициях.
        /// </summary>
        private void SumEvenPosElems()
        {
            sum = 0;
            for (int i = 0; i < ElementNumber; i++)
                for (int j = 0; j < ElementNumber; j++)
                    if (i == j)
                        sum += massive[i, j];
        }

    }
}

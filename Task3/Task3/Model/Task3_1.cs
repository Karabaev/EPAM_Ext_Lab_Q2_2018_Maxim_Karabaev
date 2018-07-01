
namespace Task3.Model
{
    using System;
    using static System.Console;

    internal class Task3_1 : Task
    {
        private int x, y;
        private float result;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_1()
        {
            Switcher = Switcher.Task3_1;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            result = 0;
            while (true)
            {
                WriteLine("Enter x:");
                try
                {
                    x = 0;
                    x = int.Parse(ReadLine());
                }
                /*По заданию сказано игнорировать ввод строк и нецелых чисел, я решил обработать такие случаи*/
                catch (FormatException) 
                {
                    WriteLine("Sorry, value is invalid. Enter unsigned integer.");
                    continue;
                }
                
                if (x > 0) break;
                WriteLine("Sorry, value is invalid. Enter 0 or better.");
            }
            while (true)
            {
                WriteLine("Enter y:");
                try
                {
                    y = 0;
                    y = int.Parse(ReadLine());
                }
                catch (FormatException)
                {
                    WriteLine("Sorry, value is invalid. Enter unsigned integer.");
                    continue;
                }
                if (y > 0) break;
                WriteLine("Sorry, value is invalid. Enter 0 or better.");
            }
            result = GetTriangleSquare();
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("Square of triangle = {0}", result);
        }
        /// <summary>
        /// Вычисляет площать треугольника.
        /// </summary>
        /// <returns>Возвращает площадь треугольника.</returns>
        private float GetTriangleSquare()
        {
            if (x < 0 || y < 0)
                return ErrorCode;
            return (x * y) / 2;
        }


        
        
    }
}

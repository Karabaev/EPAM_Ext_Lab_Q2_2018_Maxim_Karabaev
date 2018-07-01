using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    using static System.Console;
    class Task3_4 : Task
    {
        const uint startCountLines = 2;
        uint countLines;
        uint countTriangles;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_4()
        {
            Switcher = Switcher.Task3_4;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            for (uint i = 0; i < countTriangles; i++)
            {
                countLines = startCountLines + i;
                WriteLine(DisplayTriangle());
            }
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            countTriangles = 0;
            while (true)
            {
                WriteLine("Enter count of triangles:");
                if (uint.TryParse(ReadLine(), out countTriangles))
                    return;
                else
                {
                    WriteLine("Sorry, value is invalid. Enter, please, unsigned integer.");
                    continue;
                }
            }
            
        }

        /// <summary>
        /// Собирает треугольник из отдельных строк.
        /// </summary>
        /// <returns>Возвращает строку для вывода в консоль.</returns>
        private string DisplayTriangle()
        {
            string output = string.Empty;
            for (uint i = 1; i <= countLines; i++)
                output += DisplayLine(((countTriangles + 1) - i), i * 2 - 1)
                       + (i == countLines ? "" : "\n"); // немного магии
            return output;
        }
        /// <summary>
        /// Записывает одну строку рисунка, на основе входных параметров.
        /// </summary>
        /// <param name="countSpaces">Количество пробелов в строке.</param>
        /// <param name="countStars">Количество звездочек в строке.</param>
        /// <returns>Возвращает одну строку рисунка.</returns>
        private string DisplayLine(uint countSpaces, uint countStars)
        {
            string output = string.Empty;
            for (int i = 0; i < countSpaces; i++) // количество пробелов
                output += " ";
            for (int i = 0; i < countStars; i++) // количество звездочек
                output += "*";
            //output += "\n";
            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task3.Model
{
    class Task3_3 : Task
    {
        uint countLines = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_3()
        {
            Switcher = Switcher.Task3_3;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine(GetDisplayString());
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            countLines = 0;
            while (countLines <= 0)
            {
                WriteLine("Enter count of lines:");
                if (!uint.TryParse(ReadLine(), out countLines))
                    WriteLine("Sorry, value is invalid. Enter, please, unsigned integer.");
            }
        }
        /// <summary>
        /// Собирает треугольник из отдельных строк.
        /// </summary>
        /// <returns>Возвращает строку для вывода в консоль.</returns>
        private string GetDisplayString()
        {
            string output = string.Empty;
            for (uint i = 1; i <= countLines; i++)
                output += OutputLine(countLines - i, i * 2 - 1);
            return output;
        }
        /// <summary>
        /// Записывает одну строку рисунка, на основе входных параметров.
        /// </summary>
        /// <param name="countSpaces">Количество пробелов в строке.</param>
        /// <param name="countStars">Количество звездочек в строке.</param>
        /// <returns>Возвращает одну строку рисунка.</returns>
        private string OutputLine(uint countSpaces, uint countStars)
        {
            string output = string.Empty;
            for (int i = 0; i < countSpaces; i++) // количество пробелов
                output += " ";
            for (int i = 0; i < countStars; i++) // количество звездочек
                output += "*";
            output += "\n";
            return output;
        }

        
    }
}

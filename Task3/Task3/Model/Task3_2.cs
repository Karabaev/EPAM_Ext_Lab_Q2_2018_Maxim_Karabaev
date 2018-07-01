using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task3.Model
{
    class Task3_2 : Task
    {
        uint countLines = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_2()
        {
            Switcher = Switcher.Task3_2;
            Name = Repository.TaskNames[Switcher];
        }

        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            string output = string.Empty;
            for (int i = 0; i < countLines; i++)
            {
                for (int j = 0; j < i + 1; j++)
                    output += "*";
                output += "\n";
            }
            WriteLine(output);
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            countLines = 0;
            while(countLines <= 0)
            {
                WriteLine("Enter count of lines:");
                if (!uint.TryParse(ReadLine(), out countLines))
                    WriteLine("Sorry, value is invalid. Enter unsigned integer.");
            }
        }

        
    }
}

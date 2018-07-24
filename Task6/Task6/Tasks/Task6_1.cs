using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task6.Tasks
{
    class Task6_1
    {
        private string[] stringArray =
        {
            "Hello, world!",
            "My name is Maxim",
            "I am from Russia",
            "a dramatically different tomorrow",
            "A tiny change today brings",
            "EFGH",
            "ABCD"
        };
        public delegate void SortMethod();
        /// <summary>
        /// Вспомагательный метод для сортировки пузырьком
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
		private void SwapStrings(ref string str1,ref string str2)
        {
            string buffer = string.Empty;
            buffer = str1;
            str1 = str2;
            str2 = buffer;
        }
        /// <summary>
        /// Сортировка пузыьком
        /// </summary>
		private void Bubble()
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                for (int j = i + 1; j < stringArray.Length; j++)
                {
                    if (stringArray[i].Length > stringArray[j].Length)
                    {
                        SwapStrings(ref stringArray[i], ref stringArray[j]);
                    }
                    else
                    {
                        if (stringArray[i].Length == stringArray[j].Length)
                        {
                            if (stringArray[i][0] > stringArray[j][0])
                                SwapStrings(ref stringArray[i], ref stringArray[j]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Сортировка
        /// </summary>
        /// <param name="method">Метод сортировки</param>
		private void Sort(SortMethod method)
        {
            method();
        }

        /// <summary>
        /// Сортировка и вывод результат на экран
        /// </summary>
		public void DisplayResult()
        {
            WriteLine("Task 6.1.");
            WriteLine("/////////////////////////////////");
            WriteLine("Original array:\n");
            foreach (var item in stringArray)
                WriteLine(item);
            Sort(Bubble);
            WriteLine("\nSorted array:\n");
            foreach (var item in stringArray)
                WriteLine(item);
        }

    }
}

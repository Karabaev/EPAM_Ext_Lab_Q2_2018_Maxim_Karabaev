using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task3.Model
{
    class Task3_11 : Task
    {
        readonly char[] separators = new char[] { ' ' };
        const string defaultStr = "Hello, world! Maxim is the developer of such a great application.";
        string str;
        int letterCount;
        float average;
        List<string> words;
        public Task3_11()
        {
            Switcher = Switcher.Task3_11;
            Name = Repository.TaskNames[Switcher];
        }
        public override void GetResult()
        {
            WriteLine("Word count:{0}, letter count: {1}, average length: {2}", words.Count(), letterCount, average);
        }

        public override void MenuItem()
        {
            letterCount = 0;
            average = 0;
            words = new List<string>();
            while (true)
            {
                WriteLine("Use default text: {0} / {1}", YesDialog, NotDialog);
                /*Я решил не обрабатывать случаи, когда пользователь вводит не y или n.
                     А просто считать за n все вводимые варианты, кроме y и Y.*/
                if (ReadLine().ToLower() == YesDialog)
                {
                    str = defaultStr;
                    WriteLine("Using default text: {0}", str);
                    break;
                }
                else
                {
                    WriteLine("Enter some text:");
                    str = ReadLine();
                    break;
                }
            }
            words = str.Split(separators).ToList();
            for (int i = 0; i < words.Count; i++) // удаление не букв и не цифр из слов
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if (!char.IsLetterOrDigit(words[i][j]))
                    {
                        words[i] = words[i].Remove(j, 1);
                        j--;
                    }
                }
                letterCount += words[i].Length;
            }
            for (int i = 0; i < words.Count; i++) // удаление пустых слов 
            {
                if(words[i].Length <= 0)
                {
                    words.RemoveAt(i);
                    i--;
                }
            }
            average = (float)letterCount / words.Count;
        }
    }
}

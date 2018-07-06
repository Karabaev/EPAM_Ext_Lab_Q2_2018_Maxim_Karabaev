using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task3.Model
{
    class Task3_12 : Task
    {
        const string errorMessage = "Enter, please, correct string.";
        string str1;
        string str2;
        public Task3_12()
        {
            Switcher = Switcher.Task3_12;
            Name = Repository.TaskNames[Switcher];
        }
        public override void GetResult()
        {
            str2 = str2.ToLower();
            StringBuilder displayString = new StringBuilder();
            foreach (var item in str1)
            {
                if (!str2.Contains(char.ToLower(item)))
                    displayString.Append(item);
                else
                    displayString.AppendFormat("{0}{1}", item, item);
            }
            WriteLine("Result: {0}", displayString);
        }

        public override void MenuItem()
        {
            while(true)
            {
                WriteLine("Enter first string:");
                str1 = ReadLine();
                if (str1.Length > 0) break;
                else
                    WriteLine(errorMessage);
            }
            while (true)
            {
                WriteLine("Enter second string:");
                str2 = ReadLine();
                if (str2.Length > 0) break;
                else
                    WriteLine(errorMessage);
            }    
        }
    }
}

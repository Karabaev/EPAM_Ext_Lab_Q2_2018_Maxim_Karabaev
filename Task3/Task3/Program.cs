﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Model;
using static System.Console;
namespace Task3
{
    class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            while (true)
            {
                WriteLine(repository.Dialogs[DialogKeys.FirstDialog]);
                WriteLine(repository.Dialogs[DialogKeys.MainMenu]);
                Switcher switcher = Switcher.Default;
                int intSwitcher;
                bool exit = false;
                if (!int.TryParse(ReadLine(), out intSwitcher))
                {
                    switcher = Switcher.Error;
                }
                switcher = (Switcher)intSwitcher;
                Switch(switcher, out exit);
                if (exit)
                    break;
            }
        }
        /// <summary>
        /// Обработка выбора вв
        /// </summary>
        /// <param name="switcher"></param>
        /// <param name="exit"></param>
        static void Switch(Switcher switcher, out bool exit)
        {
            exit = false;
            if (switcher == Switcher.Exit)
            {
                exit = true;
                return;
            }
            // найти задачу по ее коду.
            Model.Task task = Repository.AllTasks.Where(at => at.Switcher == switcher).FirstOrDefault();  
            if (task == null)
            {
                WriteLine("Incorrect action.");
                return;
            }
            task.MenuItem();
            task.GetResult();

        }
    }
}

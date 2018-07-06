using System;
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
            while (true)
            {
                WriteLine(Repository.Dialogs[DialogKeys.Separator]);
                WriteLine(Repository.Dialogs[DialogKeys.FirstDialog]);
                WriteLine(Repository.Dialogs[DialogKeys.MainMenu]);
                Switcher switcher = Switcher.Default;
                bool exit = false;
                if (!int.TryParse(ReadLine(), out int intSwitcher))
                    switcher = Switcher.Error;
                switcher = (Switcher)intSwitcher;
                Switch(switcher, out exit);
                if (exit)
                    break;
            }
        }
        /// <summary>
        /// Обработка выбора пункта меню.
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

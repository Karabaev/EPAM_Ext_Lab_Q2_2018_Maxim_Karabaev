using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    enum DialogKeys
    {
        Separator,
        FirstDialog,
        MainMenu
    }
    enum Switcher
    {
        Error = -1,
        Default = 0,
        Task3_1 = 1,
        Task3_2 = 2,
        Task3_3 = 3,
        Task3_4 = 4,
        Task3_5 = 5,
        Task3_6 = 6,
        Task3_7 = 7,
        Task3_8 = 8,
        Task3_9 = 9,
        Task3_10 = 10,
        Task3_11 = 11,
        Task3_12 = 12,
        Task3_13 = 13,
        Exit = 14
    }
    static class Repository
    {
        static Dictionary<DialogKeys, string> dialogs;
        static Dictionary<Switcher, string> taskNames;
        static List<Task> allTasks;


        public static List<Task> AllTasks
        {
            get
            {
                if (allTasks != null)
                    return allTasks;
                else
                {
                    allTasks = new List<Task>();
                    allTasks.Add(new Task3_1());
                    allTasks.Add(new Task3_2());
                    allTasks.Add(new Task3_3());
                    allTasks.Add(new Task3_4());
                    allTasks.Add(new Task3_5());
                    allTasks.Add(new Task3_6());
                    allTasks.Add(new Task3_7());
                    allTasks.Add(new Task3_8());
                    allTasks.Add(new Task3_9());
                    allTasks.Add(new Task3_10());
                    allTasks.Add(new Task3_11());
                    allTasks.Add(new Task3_12());
                    allTasks.Add(new Task3_13());
                    return allTasks;
                }
            }
        }

        /*Справочники строк нужно загружать из файла, но не хватит на это времени.*/
         static public Dictionary<DialogKeys, string> Dialogs
        {
            get
            {
                if (dialogs != null)
                    return dialogs;
                else
                {
                    dialogs = new Dictionary<DialogKeys, string>();
                    dialogs.Add(DialogKeys.Separator, "------------------------------------");
                    dialogs.Add(DialogKeys.FirstDialog, "Choose your action:");
                    
                    string actionString = string.Empty;
                    foreach (var item in AllTasks)
                        actionString += (int)item.Switcher + " - " + item.Name + "\n";
                    actionString += (int)Switcher.Exit + " - " + Switcher.Exit.ToString();
                    dialogs.Add(DialogKeys.MainMenu, actionString);
                    return dialogs;
                }
            }
        }
        public static Dictionary<Switcher, string> TaskNames
        {
            get
            {
                if(taskNames != null)
                    return taskNames;
                else
                {
                    taskNames = new Dictionary<Switcher, string>();
                    taskNames.Add(Switcher.Task3_1, "Triangle square task");
                    taskNames.Add(Switcher.Task3_2, "Display triangle");
                    taskNames.Add(Switcher.Task3_3, "Critmas tree");
                    taskNames.Add(Switcher.Task3_4, "Big critmas tree");
                    taskNames.Add(Switcher.Task3_5, "The sum of the numbers");
                    taskNames.Add(Switcher.Task3_6, "Text selection");
                    taskNames.Add(Switcher.Task3_7, "Integer massive");
                    taskNames.Add(Switcher.Task3_8, "Three-dimensional array");
                    taskNames.Add(Switcher.Task3_9, "Sum positive elements");
                    taskNames.Add(Switcher.Task3_10, "Even positions");
                    taskNames.Add(Switcher.Task3_11, "Average lenght ot whe words");
                    taskNames.Add(Switcher.Task3_12, "Some task by strings");
                    taskNames.Add(Switcher.Task3_13, "Operations with strings");
                    return taskNames;
                }
            }
        }
    }
}

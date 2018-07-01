

namespace Task3.Model
{
    using System.Linq;
    using System.Collections.Generic;
    using static System.Console;
    internal class Task3_6 : Task
    {
        enum SelectionTypes
        {
            None = 0,
            Bold = 1,
            Italic = 2,
            Underline = 3,
        }
        const string ExitString = "exit";
        Dictionary<SelectionTypes, string> selectionTypes = new Dictionary<SelectionTypes, string>();
        List<SelectionTypes> activeSelections = new List<SelectionTypes>();
        uint switcher = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_6()
        {
            Switcher = Switcher.Task3_6;
            Name = Repository.TaskNames[Switcher];
            selectionTypes.Add(SelectionTypes.Bold, SelectionTypes.Bold.ToString());
            selectionTypes.Add(SelectionTypes.Italic, SelectionTypes.Italic.ToString());
            selectionTypes.Add(SelectionTypes.Underline, SelectionTypes.Underline.ToString());
        }
        /// <summary>
        /// Реализация абстрактного метода. В данном случае просто заглушка.
        /// </summary>
        public override void GetResult(){}
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя. в данном случае также выводит результат выполнения
        /// задачи в консоль.
        /// </summary>
        public override void MenuItem()
        {
            activeSelections.Clear();
            switcher = 0;
            while (true)
            {
                if (activeSelections.Count == 0)
                    activeSelections.Add(SelectionTypes.None);
                DisplayActiveSelectionTypes();
                DisplayAllSelectionTypes();
                WriteLine("Enter exit to {0} to main menu.", ExitString);
                string enteredValue = ReadLine();
                if (enteredValue.ToLower() == ExitString)
                    break;
                if (!uint.TryParse(enteredValue, out switcher))
                {

                    WriteLine("Sorry, value is invalid. Enter unsigned integer.");
                    continue;
                }
                else
                {
                    SelectionTypes selected = selectionTypes.Where(st => (uint)st.Key == switcher).FirstOrDefault().Key;
                    if (activeSelections.Contains(selected))
                        activeSelections.Remove(selected);
                    else
                        activeSelections.Add(selected);
                    if(activeSelections.Contains(SelectionTypes.None))
                        activeSelections.Remove(SelectionTypes.None);

                }
            }
            
        }
        /// <summary>
        /// Выведит в консоль все варианты выделения строк.
        /// </summary>
        private void DisplayAllSelectionTypes()
        {
            WriteLine("Enter:");
            foreach (var item in selectionTypes)
                WriteLine("{0}: {1}", (int)item.Key, item.Value);
        }
        /// <summary>
        /// Выводит активные варианты выделения строк.
        /// </summary>
        private void DisplayActiveSelectionTypes()
        {
            Write("Text selection: ");
            foreach (var item in activeSelections)
            {
                if(item == activeSelections.Last())
                    Write(item);
                else
                    Write("{0}, ", item);
            }
            WriteLine();
        }
    }
}

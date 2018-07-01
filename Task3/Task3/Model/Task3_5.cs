namespace Task3.Model
{
    using static System.Console;
    class Task3_5 : Task
    {
        const int maxNumber = 1000, multA = 3, multB = 5;
        int summ = 0;
        /// <summary>
        /// Конструктор. Инициализирует код задачи и понятное название задачи.
        /// </summary>
        public Task3_5()
        {
            Switcher = Switcher.Task3_5;
            Name = Repository.TaskNames[Switcher];
        }
        /// <summary>
        /// Реализация абстрактного метода. Выводит результат вычислений в консоль.
        /// </summary>
        public override void GetResult()
        {
            WriteLine("The sum of the numbers is a multiple of 3 or 5 and less 1000 = {0}", summ);
        }
        /// <summary>
        /// Реализация абстрактного метода. Производит вычисления, 
        /// а также принимает входные параметры от пользователя.
        /// </summary>
        public override void MenuItem()
        {
            summ = 0;
            WriteLine("Display operands? {0} / {1}", YesDialog, NotDialog);
            /*Я решил не обрабатывать случаи, когда пользователь вводит не y или n.
            А просто считать за n все вводимые строки, кроме y.*/
            bool isDisplayOpers = ReadLine() == YesDialog ? true : false;
            for (int i = 0; i < maxNumber; i++)
            {
                if (i % 3 == 0 | i % 5 == 0)
                {
                    if (isDisplayOpers)
                        Write("{0} ", i);
                    summ += i;
                }
            }
            if(isDisplayOpers)
                WriteLine();
        }
    }
}

namespace Task3.Model
{
    internal abstract class Task
    {
        protected const string YesDialog = "y", NotDialog = "n";
        protected const int ElementNumber = 10, MaxRand = 10, MinRand = -10;
        protected const int ErrorCode = -1;

        private string name;
        private Switcher switcher;
        

        /// <summary>
        /// Название задачи.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                name = value;
            }
        }
        /// <summary>
        /// Код задачи.
        /// </summary>
        public Switcher Switcher
        {
            get
            {
                return switcher;
            }
            protected set
            {
                switcher = value;
            }
        }

        /// <summary>
        /// Абстрактный метод, в нем производятся все вычисления задачи. 
        /// Должен вызываться перед методом GetResult().
        /// </summary>
        public abstract void MenuItem();
        /// <summary>
        /// Абстрактный метод, необходим для вывода результата задач.
        /// </summary>
        public abstract void GetResult();
    }
}

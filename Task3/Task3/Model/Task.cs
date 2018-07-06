namespace Task3.Model
{
    using System;
    using System.Text;
    using System.IO;
    using static System.Console;
    internal abstract class Task
    {
         
        protected const string YesDialog = "y", NotDialog = "n";
        protected const int ElementNumber = 10, MaxRand = 10, MinRand = -10;
        protected const int ErrorCode = -1;
        private FileStream fileStream;
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

        /// <summary>
        /// Открывает файл для записи, добавляет в конец файла записи и закрывает файл. Подразумевается, что файл уже существует.
        /// </summary>
        /// <param name="pathToFile">Путь к файлу.</param>
        /// <param name="content">Строка для записи.</param>
        private void WriteToFile(string pathToFile, StringBuilder content)
        {
            try
            {
                using (fileStream = new FileStream(pathToFile, FileMode.Append, FileAccess.Write)) 
                {
                    byte[] buffContent = Encoding.Default.GetBytes(content.ToString());
                    fileStream.Write(buffContent, 0, buffContent.Length);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            catch (IOException ex)
            {
                WriteLine("Error writing to file: {0}", pathToFile);
                WriteLine("Additional info: {0}", ex.Message);
            }
        }
        /// <summary>
        /// Записывает лог в конец файла.
        /// </summary>
        /// <param name="pathToFile">Путь к файлу.</param>
        /// <param name="content">Строка для записи.</param>
        protected void WriteLog(string pathToFile, string content)
        {
            StringBuilder log = new StringBuilder();
            log.Append(String.Format("{0}: {1}{2}", DateTime.Now.ToString(), content, "\n"));
            WriteToFile(pathToFile, log);
        }

    }
}

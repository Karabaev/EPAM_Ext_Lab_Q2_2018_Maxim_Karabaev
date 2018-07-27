namespace Task7.Tasks.Task7_3
{
    using System;
    using System.IO;
    using System.Text;

    internal class Logger
    {
        private string defaultLogFileName = "Log.log";
        private string currentFileName = string.Empty;

        public Logger(string fileName)
        {
            this.currentFileName = fileName;
        }

        public Logger()
        {
            this.currentFileName = this.defaultLogFileName;
        }

        public void Write(string format, params object[] args)
        {
            string log = string.Format(format, args);
            this.WriteToFile(log);
        }

        public void Write(string log)
        {
            this.WriteToFile(log);
        }

        private void WriteToFile(string log)
        {
            try
            {
                using (FileStream stream = new FileStream(this.currentFileName, FileMode.Append, FileAccess.Write))
                {
                    byte[] byteLog = Encoding.Default.GetBytes(string.Format("{0}: {1}{2}", DateTime.Now, log, "\n"));
                    stream.Write(byteLog, 0, byteLog.Length);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing log to file. {0}", ex.Message);
            }
        }
    }
}

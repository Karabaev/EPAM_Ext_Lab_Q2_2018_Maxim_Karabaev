using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Task7.Tasks.Task7_3
{
    class Logger
    {
        private string defaultLogFileName = "Log.txt";
        private FileInfo file;
        public Logger(string pathToFile)
        {
            file = new FileInfo(pathToFile);
        }
        public Logger()
        {
            file = new FileInfo(defaultLogFileName);
        }

        public void Write(string format, params object[] args)
        {
            string log = string.Format(format, args);
            WriteToFile(log);
        }
        public void Write(string log)
        {
            WriteToFile(log);
        }
        private void WriteToFile(string log)
        {
            try
            {
                using (FileStream stream = file.OpenWrite())
                {
                    byte[] byteLog = Encoding.Default.GetBytes(log);
                    stream.Write(byteLog, 0, byteLog.Length);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Error writing log to file. {0}", ex.Message);
            }
        }
    }
}

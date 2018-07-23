using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
namespace Task6.Tasks.Model
{
    public enum DayPart
    {
        Morning,
        Day,
        Evening
    }
    public class Data
    {
        public Dictionary<DayPart, string> GreetingStrings = new Dictionary<DayPart, string>();
        public DayPart CurrentDayPart;
        private Time currentTime;
        private Thread timerThread;
        public Data()
        {
            GreetingStrings.Add(DayPart.Morning, "Good morning.");
            GreetingStrings.Add(DayPart.Day, "Good day.");
            GreetingStrings.Add(DayPart.Evening, "Good evening.");
        }
        public void StartTimer()
        {
            timerThread = new Thread(Timer);
            timerThread.Start();
        }
        public void StopTimer()
        {
            timerThread.Abort();
            timerThread = null;
        }

        private void Timer()
        {
            currentTime = new Time();
            while(true)
            {
                currentTime.Seconds++;
                if (currentTime.Seconds > 59)
                {
                    currentTime.Seconds = 0;
                    currentTime.Minutes++;
                    if(currentTime.Minutes > 59)
                    {
                        currentTime.Minutes = 0;
                        currentTime.Hours++;
                        if (currentTime.Hours > 23)
                            currentTime.Hours = 0;
                    }
                }
                WriteLine(currentTime);
            }
        }

    }
}

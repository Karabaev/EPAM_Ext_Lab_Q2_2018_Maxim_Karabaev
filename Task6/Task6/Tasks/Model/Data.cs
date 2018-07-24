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
        private const int SecondStep = 20;
        private const int DelayBetweenTimerTicks = 20;
        private const int ProbabilityOccurrence = 80;

        public Dictionary<DayPart, string> GreetingStrings = new Dictionary<DayPart, string>();
        public DayPart currentDayPart;
        
        private Time currentTime, timeMorning, timeEvening, startTime = new Time(9, 0, 0);
        private Thread timerThread;

        List<Person> people = new List<Person>();

        private Office office;
        public delegate void Coming(Person person);
        public delegate void Left(Person person);

        public event Coming OnComing;
        public event Left OnLeft;

        private Random rand = new Random();
        public Data()
        {
            GreetingStrings.Add(DayPart.Morning, "Good morning.");
            GreetingStrings.Add(DayPart.Day, "Good day.");
            GreetingStrings.Add(DayPart.Evening, "Good evening.");
            
            timeMorning = new Time(12, 0, 0);
            timeEvening = new Time(17, 0, 0);

            if (startTime < timeMorning)
                CurrentDayPart = DayPart.Morning;
            else
            {
                if (startTime > timeEvening)
                    CurrentDayPart = DayPart.Evening;
                else
                    CurrentDayPart = DayPart.Day;
            }

            //в конструкторах идет подпись к событиям, потому важен порядок инициализации ссылок, офис должен стоять раньше
            office = new Office(this);
            people.Add(new Person(this, "Maxim"));
            people.Add(new Person(this, "Roman"));
            people.Add(new Person(this, "Georgiy"));
            people.Add(new Person(this, "Artem"));
            people.Add(new Person(this, "Evgeniy"));

            
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
            currentTime = new Time(startTime);
            while(true)
            {
                currentTime.Seconds += SecondStep;
                if (currentTime.Seconds > 59)
                {
                    currentTime.Seconds = 0;
                    currentTime.Minutes++;
                    if(currentTime.Minutes > 59)
                    {
                        currentTime.Minutes = 0;
                        currentTime.Hours++;
                        if (currentTime < timeMorning)
                            CurrentDayPart = DayPart.Morning;
                        else
                        {
                            if (currentTime > timeEvening)
                                CurrentDayPart = DayPart.Evening;
                            else
                                CurrentDayPart = DayPart.Day;
                        }
                        if (currentTime.Hours > 23)
                            currentTime.Hours = 0;

                        int value = rand.Next(0, 100);
                        if(value <= ProbabilityOccurrence) // кто-то пришел или ушел
                        {
                            value = rand.Next(0, people.Count - 1);
                            if(!office.peopleInTheOffice.Contains(people[value]))
                            {
                                OnComing(people[value]);
                            }
                            else
                            {
                                OnLeft(people[value]);
                            }
                        }
                    }
                }
                Thread.Sleep(DelayBetweenTimerTicks);
            }
        }
        public DayPart CurrentDayPart
        {
            get
            {
                return currentDayPart;
            }
            set
            {
                if (currentDayPart != value)
                {
                    currentDayPart = value;
                    WriteLine("Now is {0}", currentDayPart.ToString());
                }
            }
        }
    }
}

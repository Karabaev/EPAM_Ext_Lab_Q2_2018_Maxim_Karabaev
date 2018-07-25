using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
namespace Task6.Tasks.Model
{
    /// <summary>
    /// Времена суток.
    /// </summary>
    public enum DayPart
    {
        Default,
        Morning,
        Day,
        Evening
    }
    public class Data
    {

        private const int SecondStep = 20; // сколько секунд проходит за одн тик таймера
        private const int DelayBetweenTimerTicks = 20; // сколько мс реального времени проходит между тиками таймера
        private const int ProbabilityOccurrence = 80; // шанс возникновения события

        public Dictionary<DayPart, string> GreetingStrings = new Dictionary<DayPart, string>(); // строки приветсвтвия, в зависимости от времени суток
        public DayPart currentDayPart; // актуальное время суток
        
        private Time currentTime, timeMorning, timeEvening, startTime = new Time(9, 0, 0); // структуры, актуальное время, утреннее время, вечернее, и начальное
        private Thread timerThread; // поток для таймера

        List<Person> people = new List<Person>(); // список работников офиса

        private Office office; // офис
        public delegate void Coming(Person person); 
        public delegate void Left(Person person);

        public event Coming OnComing;
        public event Left OnLeft;

        private Random rand = new Random();
        public Data()
        {
            GreetingStrings.Add(DayPart.Morning, "Good morning");
            GreetingStrings.Add(DayPart.Day, "Good day");
            GreetingStrings.Add(DayPart.Evening, "Good evening");
            
            timeMorning = new Time(12, 0, 0); // время утра
            timeEvening = new Time(17, 0, 0); // время вечера

            CurrentDayPart = DayPart.Default; // время суток установить в дефолтное значение
            // выбор времени суток в зависимости от времени
            if (startTime < timeMorning)
                CurrentDayPart = DayPart.Morning;
            else
            {
                if (startTime > timeEvening)
                    CurrentDayPart = DayPart.Evening;
                else
                    CurrentDayPart = DayPart.Day;
            }

            //в конструкторах идет подпись на события, потому важен порядок инициализации ссылок, офис должен стоять раньше
            office = new Office(this);
            people.Add(new Person(this, "Maxim"));
            people.Add(new Person(this, "Roman"));
            people.Add(new Person(this, "Georgiy"));
            people.Add(new Person(this, "Artem"));
            people.Add(new Person(this, "Evgeniy"));
        }
        /// <summary>
        /// Запуск таймера
        /// </summary>
        public void StartTimer()
        {
            timerThread = new Thread(Timer);
            timerThread.Start();
        }
        /// <summary>
        /// Остановка таймера
        /// </summary>
        public void StopTimer()
        {
            timerThread.Abort();
            timerThread = null;
        }

        /// <summary>
        /// Метод таймера
        /// </summary>
        private void Timer()
        {
            currentTime = new Time(startTime);
            while(true)
            {
                currentTime.Seconds += SecondStep;
                if (currentTime.Seconds > 59)//todo pn hardcode
                {
                    currentTime.Seconds = 0;
                    currentTime.Minutes++;
                    if(currentTime.Minutes > 59)//todo pn hardcode
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
                        if (currentTime.Hours > 23)//todo pn hardcode
							currentTime.Hours = 0;

                        // а произойдет ли событие?
                        int value = rand.Next(0, 100); // шанс возникновения события указан в ProbabilityOccurrence //todo pn hardcode
						if (value <= ProbabilityOccurrence) // кто-то пришел или ушел
                        {
                            value = rand.Next(0, people.Count - 1); // а кто пришел/ушел?
                            if(!office.peopleInTheOffice.Contains(people[value]))
                                OnComing(people[value]);
                            else
                                OnLeft(people[value]);
                        }
                    }
                }
                Thread.Sleep(DelayBetweenTimerTicks); // задержка между тиками
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

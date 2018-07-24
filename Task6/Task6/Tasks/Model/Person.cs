using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task6.Tasks.Model
{
    public class Person
    {
        public string Name { get; set; }
        private Data data;

        public Person(Data data, string name)
        {
            this.data = data;
            Name = name;
        }
        /// <summary>
        /// Приветствие пришедших в офис.
        /// </summary>
        /// <param name="other">Кто пришел</param>
        public void Greetings(Person other)
        {
            //Сам себя не приветствует
            if(other != this)
                WriteLine("{0}, {1}! - said {2} ", data.GreetingStrings[data.CurrentDayPart], other, Name);
        }
        /// <summary>
        /// Прощание с уходящими.
        /// </summary>
        /// <param name="other">Кто ушел</param>
        public void Goodbye(Person other)
        {
            // сам с собой не прощается
            if (other != this)
                WriteLine("Goodbye, {0}! - said {1}", other, Name);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

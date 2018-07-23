using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task6.Tasks.Model
{
    
    class Person
    {
        public string Name { get; set; }
        private Data data;

        public Person(Data data, string name)
        {
            this.data = data;
            Name = name;
        }

        public void Greetings(Person other)
        {
            WriteLine("{0}, {1} ", data.GreetingStrings[data.CurrentDayPart], other);
        }
        public void Goodbye(Person other)
        {
            WriteLine("Goodbye, {0}", other);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

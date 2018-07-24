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
            data.OnComing += Greetings;
            data.OnLeft += Goodbye;
        }
        public void Greetings(Person other)
        {
            if(other != this)
                WriteLine("{0}, {1}! - said {2} ", data.GreetingStrings[data.CurrentDayPart], other, Name);
        }
        public void Goodbye(Person other)
        {
            if (other != this)
                WriteLine("Goodbye, {0}! - said {1}", other, Name);
        }
        public override string ToString()
        {
            return Name;
        }

        public void SomeEvent()
        {

        }
    }
}

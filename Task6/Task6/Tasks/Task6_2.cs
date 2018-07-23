using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Tasks.Model;
namespace Task6.Tasks
{
    class Task6_2
    {
        Data data = new Data();
        List<Person> people = new List<Person>();

        public Task6_2()
        {
            people.Add(new Person(data, "Maxim"));
            people.Add(new Person(data, "Roman"));
            people.Add(new Person(data, "Georgiy"));
            people.Add(new Person(data, "Artem"));
            people.Add(new Person(data, "Evgeniy"));
        }

        public void Start()
        {
            data.StartTimer();
        }
        public void Stop()
        {
            data.StopTimer();
        }
    }
}

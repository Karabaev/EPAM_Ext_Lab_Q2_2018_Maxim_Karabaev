using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Task6.Tasks.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Office
    {
        /// <summary>
        /// Конструктор для подписи методов на события.
        /// </summary>
        /// <param name="data">Ссылка на объект, который хранит события.</param>
        public Office(Data data)
        {
            data.OnComing += IncomingPerson;
            data.OnLeft += LeftPerson;
        }
        /// <summary>
        /// Люди, находящиеся в офисе
        /// </summary>
        public List<Person> peopleInTheOffice = new List<Person>();
        public void IncomingPerson(Person person)
        {
            if (!peopleInTheOffice.Contains(person))
                peopleInTheOffice.Add(person);
            WriteLine("{0} came to the office", person);
        }
        public void LeftPerson(Person person)
        {
            if(peopleInTheOffice.Contains(person))
                peopleInTheOffice.Remove(person);
            WriteLine("{0} left the office", person);
        }
    }
}

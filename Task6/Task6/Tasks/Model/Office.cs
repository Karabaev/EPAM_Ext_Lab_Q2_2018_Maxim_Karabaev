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
        Data data;
        /// <summary>
        /// Конструктор для подписи методов на события.
        /// </summary>
        /// <param name="data">Ссылка на объект, который хранит события.</param>
        public Office(Data data)
        {
            this.data = data;
            this.data.OnComing += IncomingPerson;
            this.data.OnLeft += LeftPerson;
        }
        /// <summary>
        /// Люди, находящиеся в офисе.
        /// </summary>
        public List<Person> peopleInTheOffice = new List<Person>();
        /// <summary>
        /// Вызывается, когда кто-то приходим в офис. 
        /// </summary>
        /// <param name="person">Тот кто пришел.</param>
        public void IncomingPerson(Person person)
        {
            if (!peopleInTheOffice.Contains(person))
            {
                peopleInTheOffice.Add(person);
                data.OnComing += person.Greetings;
                data.OnLeft += person.Goodbye;
            }
            WriteLine("{0} came to the office", person);
        }
        /// <summary>
        /// Вызывается когда кто-то ушел из офиса.
        /// </summary>
        /// <param name="person">Кто ушел.</param>
        public void LeftPerson(Person person)
        {
            if (peopleInTheOffice.Contains(person))
            {
                peopleInTheOffice.Remove(person);
                data.OnComing -= person.Greetings;
                data.OnLeft -= person.Goodbye;
            }
            WriteLine("{0} left the office", person);
        }
    }
}

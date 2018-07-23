using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    /// <summary>
    /// Сообщение в теме
    /// </summary>
    public class Message : Entity
    {
        /// <summary>
        /// Кто создал 
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        /// Когда создана
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Content { get; set; }

        public ExtraContent Extra { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Creator: {1}, Date/time of create: {2}", ID, Creator, CreationDate);
        }
        public override bool Equals(object obj)
        {
            Message other = obj as Message;
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Creator.GetHashCode() + CreationDate.GetHashCode() + Content.GetHashCode() + Extra.GetHashCode();
            }
            catch(StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }
}

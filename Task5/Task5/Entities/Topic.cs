using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    /// <summary>
    /// Тема на форуме
    /// </summary>
    public class Topic : Entity
    {
        /// <summary>
        /// заголовок темы
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Сообщения в теме
        /// </summary>
        public List<Message> Messages { get; set; }
        /// <summary>
        /// Кем опубликована тема
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        /// Когда опубликована тема
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Ссылка на тему
        /// </summary>
        public string Link { get; set; }

        public override bool Equals(object obj)
        {
            Topic other = obj as Topic;
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Caption.GetHashCode() + Messages.GetHashCode() + Creator.GetHashCode() + CreationDate.GetHashCode() + Link.GetHashCode();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}

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
            return (ID == other.ID) &&
                (Caption == other.Caption) &&
                Messages.Equals(other.Messages) &&
                Creator.Equals(other.Creator) &&
                (CreationDate == other.CreationDate) &&
                (Link == other.Link);
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

        public override void Reinitialization(Entity other)
        {
            Topic newTopic = other as Topic;
            if (newTopic == null) return;
            Caption = newTopic.Caption;
            Messages = newTopic.Messages;
            Creator = newTopic.Creator;
            CreationDate = newTopic.CreationDate;
            Link = newTopic.Link;
        }
    }
}

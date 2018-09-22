using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Entities
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

        public MessageAttacment Extra { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Creator: {1}, Date/time of create: {2}", ID, Creator, CreationDate);
        }
        public override bool Equals(object obj)
        {
            Message other = obj as Message;
            if (other == null) return false;
            return (ID == other.ID) && 
                (Creator == other.Creator) && 
                (CreationDate == other.CreationDate) && 
                (Content == other.Content) && 
                (Extra == other.Extra);
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

        public override void Reinitialization(Entity other)
        {
            Message message = other as Message;
            if (message == null) return;
            Creator = message.Creator;
            CreationDate = message.CreationDate;
            Content = message.Content;
            Extra = message.Extra;
        }

        public override bool LikeAs(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}

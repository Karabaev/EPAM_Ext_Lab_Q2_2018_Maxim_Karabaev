using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    public class MessageRepository : Repository
    {
        public List<Message> Messages { get; set; }

        public MessageRepository()
        {
            UserRepository userRepos = new UserRepository();
            ExtraContentRepository extraRepos = new ExtraContentRepository();
            Messages = new List<Message>();
            Messages.Add(new Message { ID = 0, Content = "m1", CreationDate = DateTime.Now, Creator = userRepos.Users[0], Extra = extraRepos.ExtraContents[0] });
            Messages.Add(new Message { ID = 1, Content = "m2", CreationDate = DateTime.Now, Creator = userRepos.Users[1], Extra = extraRepos.ExtraContents[1] });
            Messages.Add(new Message { ID = 2, Content = "m3", CreationDate = DateTime.Now, Creator = userRepos.Users[1], Extra = extraRepos.ExtraContents[2] });
            Messages.Add(new Message { ID = 3, Content = "m4", CreationDate = DateTime.Now, Creator = userRepos.Users[0], Extra = extraRepos.ExtraContents[3] });
        }

        public override List<T> GetAllEntities<T>()
        {
            return Messages as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Messages.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return Messages.Remove(Messages.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            Message newMessage = entity as Message;
            if (newMessage == null) return false;
            if (Messages.Contains(newMessage))
                return false;
            // если запись с таким ID уже есть в базе, то изменить ее поля
            Message message = Messages.Where(u => u.ID == newMessage.ID).FirstOrDefault();
            if (message != null)
            {
                message.Reinitialization(newMessage);
                return true;
            }
            Messages.Add(newMessage);
            return true;
        }
    }
}

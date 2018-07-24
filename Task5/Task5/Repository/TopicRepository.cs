using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    public class TopicRepository : Repository
    {
        public List<Topic> Topics { get; set; }

        public TopicRepository()
        {
            Topics = new List<Topic>();
            UserRepository userRepos = new UserRepository();
            MessageRepository messageRepos = new MessageRepository();
            Topics.Add(new Topic { ID = 0, Caption = "t1", Link = "t1", CreationDate = DateTime.Now, Creator = userRepos.Users[0], Messages = new List<Message>() });
            Topics.Add(new Topic { ID = 1, Caption = "t2", Link = "t2", CreationDate = DateTime.Now, Creator = userRepos.Users[1], Messages = new List<Message>() });
            Topics.Add(new Topic { ID = 2, Caption = "t3", Link = "t3", CreationDate = DateTime.Now, Creator = userRepos.Users[2], Messages = new List<Message>() });
            Topics.Add(new Topic { ID = 3, Caption = "t4", Link = "t4", CreationDate = DateTime.Now, Creator = userRepos.Users[3], Messages = new List<Message>() });

        }

        public override List<T> GetAllEntities<T>()
        {
            return Topics as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Topics.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return Topics.Remove(Topics.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            Topic newTopic = entity as Topic;
            if (newTopic == null) return false;
            if (Topics.Contains(newTopic))
                return false;
            // если запись с таким ID уже есть в базе, то изменить ее поля
            Topic topic = Topics.Where(u => u.ID == newTopic.ID).FirstOrDefault();
            if (topic != null)
            {
                topic.Reinitialization(newTopic);
                return true;
            }

            Topics.Add(newTopic);
            return true;
        }
    }
}

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
            int startCount = Messages.Count;
            Messages.Add(entity as Message);
            return Messages.Count > startCount ? true : false;
        }
    }
}

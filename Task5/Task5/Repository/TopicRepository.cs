using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    class TopicRepository : Repository
    {
        public List<Topic> Topics { get; set; }

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
            int startCount = Topics.Count;
            Topics.Add(entity as Topic);
            return Topics.Count > startCount ? true : false;
        }
    }
}

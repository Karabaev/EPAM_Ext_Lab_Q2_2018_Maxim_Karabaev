using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task5.Repository
{
    public class ExtraContentRepository : Repository
    {
        public List<ExtraContent> ExtraContents { get; set; }

        public ExtraContentRepository()
        {
            ExtraContents = new List<ExtraContent>();
            ExtraContents.Add(new ExtraContent { ID = 0, Content = new object() });
            ExtraContents.Add(new ExtraContent { ID = 1, Content = new object() });
            ExtraContents.Add(new ExtraContent { ID = 2, Content = new object() });
            ExtraContents.Add(new ExtraContent { ID = 3, Content = new object() });
            ExtraContents.Add(new ExtraContent { ID = 4, Content = new object() });
        }

        public override List<T> GetAllEntities<T>()
        {
            return ExtraContents as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return ExtraContents.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return ExtraContents.Remove(ExtraContents.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            ExtraContent newExtra = entity as ExtraContent;
            if (newExtra == null) return false;
            if (ExtraContents.Contains(newExtra))
                return false;
            // если запись с таким ID уже есть в базе, то изменить ее поля
            ExtraContent extra = ExtraContents.Where(u => u.ID == newExtra.ID).FirstOrDefault();
            if (extra != null)
            {
                extra.Reinitialization(newExtra);
                return true;
            }
            ExtraContents.Add(newExtra);
            return true;
        }
        
    }
}

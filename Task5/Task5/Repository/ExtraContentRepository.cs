using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task5.Repository
{
    public class ExtraContentRepository : Repository
    {
        public List<ExtraContent> ExtraContents { get; set; }

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
            int startCount = ExtraContents.Count;
            ExtraContents.Add(entity as ExtraContent);
            return ExtraContents.Count > startCount ? true : false;
        }
        
    }
}

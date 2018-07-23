using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    class SectionRepository : Repository
    {
        public List<Section> Sections { get; set; }

        public override List<T> GetAllEntities<T>()
        {
            return Sections as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Sections.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return Sections.Remove(Sections.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            int startCount = Sections.Count;
            Sections.Add(entity as Section);
            return Sections.Count > startCount ? true : false;
        }
    }
}

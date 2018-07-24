using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    public class SectionRepository : Repository
    {
        public List<Section> Sections { get; set; }

        public SectionRepository()
        {
            Sections = new List<Section>();
            Sections.Add(new Section { ID = 0, Name = "s1", Description = "s1", Link = "s1", TopicList = new List<Topic>() });
            Sections.Add(new Section { ID = 1, Name = "s2", Description = "s2", Link = "s2", TopicList = new List<Topic>() });
            Sections.Add(new Section { ID = 2, Name = "s3", Description = "s3", Link = "s3", TopicList = new List<Topic>() });
            Sections.Add(new Section { ID = 3, Name = "s4", Description = "s4", Link = "s4", TopicList = new List<Topic>() });
        }


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
            Section newSection = entity as Section;
            if (newSection == null) return false;
            if (Sections.Contains(newSection))
                return false;
            // если запись с таким ID уже есть в базе, то изменить ее поля
            Section section = Sections.Where(u => u.ID == newSection.ID).FirstOrDefault();
            if (section != null)
            {
                section.Reinitialization(newSection);
                return true;
            }
            Sections.Add(newSection);
            return true;
        }
    }
}

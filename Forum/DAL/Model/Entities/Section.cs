using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model.Entities
{
    /// <summary>
    /// Раздел форума
    /// </summary>
    public class Section : Entity
    {
        /// <summary>
        /// Название раздела
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ссылка на раздел
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Список тем в разделе
        /// </summary>
        public List<Topic> TopicList { get; set; }

        public override bool Equals(object obj)
        {
            Section other = obj as Section;
            if (other == null) return false;
            return (ID == other.ID) &&
                (Name == other.Name) &&
                (Description == other.Description) &&
                (Link == other.Link) &&
                TopicList.Equals(other.TopicList);
        }
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Name.GetHashCode() + Description.GetHashCode() + Link.GetHashCode() + TopicList.GetHashCode();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public override void Reinitialization(Entity other)
        {
            Section newSection = other as Section;
            if (newSection == null) return;
            Name = newSection.Name;
            Description = newSection.Description;
            Link = newSection.Link;
            TopicList = newSection.TopicList;
        }
    }
}

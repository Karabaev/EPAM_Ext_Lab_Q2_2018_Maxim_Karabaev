using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    /// <summary>
    /// Привилегии (редактирование записей, пользователей, блокировка пользователей, доступ к разделам форума)
    /// </summary>
    public class Permission : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            Permission other = obj as Permission;
            if (other == null) return false;
            return (ID == other.ID) && (Name == other.Name) && (Description == other.Description);
        }
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Name.GetHashCode() + Description.GetHashCode();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public override void Reinitialization(Entity other)
        {
            Permission newPerm = other as Permission;
            if (newPerm == null) return;
            Name = newPerm.Name;
            Description = newPerm.Description;
        }
    }
}

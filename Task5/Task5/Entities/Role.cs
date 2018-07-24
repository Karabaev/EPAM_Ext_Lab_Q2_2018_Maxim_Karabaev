using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    /// <summary>
    /// Уровень доступа (админ, модератор, пользователь, гость)
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список прав
        /// </summary>
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        /// <summary>
        /// Список пользователей, относящихся к роли
        /// </summary>
      //  public List<User> Users { get; set; } = new List<User>();

        public override bool Equals(object obj)
        {
            Role other = obj as Role;
            if (other == null) return false;
            return (Name == other.Name) && Permissions.Equals(other.Permissions);
        }
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Name.GetHashCode() + Permissions.GetHashCode();// + Users.GetHashCode();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public override void Reinitialization(Entity other)
        {
            Role newRole = other as Role;
            if (newRole == null) return;
            Name = newRole.Name;
            Permissions = newRole.Permissions;
        }
    }
}

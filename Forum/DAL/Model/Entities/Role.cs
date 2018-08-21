namespace DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Уровень доступа (админ, модератор, пользователь, гость...).
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Название роли.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список прав.
        /// </summary>
        public List<Permission> Permissions { get; set; }

        public Role(uint? id, string name, List<Permission> permissions)
        {
            base.ID = id;
            this.Name = name;
            this.Permissions = permissions;
        }

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

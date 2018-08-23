namespace DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public Role(int? id, string name, List<Permission> permissions)
        {
            base.ID = id;
            this.Name = name;
            this.Permissions = permissions;
        }

        public override bool Equals(object obj)
        {
            Role other = obj as Role;

            if (other == null)

            {
                return false;
            }

            return (base.ID == other.ID) && this.LikeAs(other);
        }

        public override bool LikeAs(Entity entity)
        {
            Role other = entity as Role;

            if (other == null)
            {
                return false;
            }

            return (this.Name == other.Name) && this.Permissions.All(other.Permissions.Contains);
        }

        public override int GetHashCode()
        {
            int result = 0;

            try
            {
                result = base.ID.GetHashCode() + this.Name.GetHashCode() + this.Permissions.GetHashCode();
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
            this.Name = newRole.Name;
            this.Permissions = newRole.Permissions;
        }
    }
}

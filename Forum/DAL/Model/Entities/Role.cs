namespace DAL.Model.Entities
{
    using System;
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
        /// Уровень доступа.
        /// </summary>
        public int AccessLevel { get; set; }

        public Role(int id, string name, int level)
        {
            base.ID = id;
            this.Name = name;
            this.AccessLevel = level;
        }

        public Role() { }

        public override bool Equals(object obj)
        {
            if (!(obj is Role other))
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

            return this.Name == other.Name && this.AccessLevel == other.AccessLevel;
        }

        public override int GetHashCode()
        {
            int result = 0;

            try
            {
                result = base.ID.GetHashCode() + this.Name.GetHashCode() + this.AccessLevel;
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public override void Reinitialization(Entity other)
        {
            if (!(other is Role newRole))
            {
                return;
            }

            this.Name = newRole.Name;
            this.AccessLevel = newRole.AccessLevel;
        }

        public override string ToString()
        {
            return string.Format("Role ID:{0}, Name: {1}, AccessLevel: {2}", this.ID, this.Name, this.AccessLevel);
        }
    }
}

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

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если все свойства сопадают, иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Role other))
            {
                return false;
            }

            return (base.ID == other.ID) && this.LikeAs(other);
        }

        /// <summary>
        /// Сравнивает объект с другим (кроме идентификатора). 
        /// </summary>
        /// <param name="entity">Другой объект.</param>
        /// <returns>true если свойства, кроме идентификатора сопадают, иначе false.</returns>
        public override bool LikeAs(Entity entity)
        {
            Role other = entity as Role;

            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name && this.AccessLevel == other.AccessLevel;
        }

        /// <summary>
        /// Вычисляет хэш код объекта.
        /// </summary>
        /// <returns>Хэш код объекта.</returns>
        public override int GetHashCode()
        {
            int result = 0;

            try
            {
                result = base.ID.GetHashCode() + this.Name.GetHashCode() + this.AccessLevel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Переинициализация свойств объекта свойств другого объекта.
        /// </summary>
        /// <param name="other"></param>
        public override void Reinitialization(Entity other)
        {
            if (!(other is Role newRole))
            {
                return;
            }

            this.Name = newRole.Name;
            this.AccessLevel = newRole.AccessLevel;
        }

        /// <summary>
        /// Возвращает строкое представление объекта.
        /// </summary>
        /// <returns>Строковое представление.</returns>
        public override string ToString()
        {
            return string.Format("Role ID:{0}, Name: {1}, AccessLevel: {2}", this.ID, this.Name, this.AccessLevel);
        }
    }
}

namespace DAL.Model.Entities
{
    using System;

    /// <summary>
    /// Раздел форума.
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
        /// Инициализирует объект в памяти.
        /// </summary>
        /// /// <param name="id">Идентификатор раздела.</param>
        /// <param name="name">Название раздела.</param>
        /// <param name="desc">Описание раздела.</param>
        /// <param name="link">Ссылка на раздел.</param>
        public Section(int id, string name, string desc, string link)
        {
            base.ID = id;
            this.Name = name;
            this.Description = desc;
            this.Link = link;
        }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если все свойства сопадают, иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Section other))
            {
                return false;
            }

            return (ID == other.ID) && this.LikeAs(other);
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
                result =    this.ID.GetHashCode() +
                            this.Name.GetHashCode() +
                            this.Description.GetHashCode() +
                            this.Link.GetHashCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Сравнивает объект с другим (кроме идентификатора). 
        /// </summary>
        /// <param name="entity">Другой объект.</param>
        /// <returns>true если свойства, кроме идентификатора сопадают, иначе false.</returns>
        public override bool LikeAs(Entity entity)
        {
            if (!(entity is Section other))
            {
                return false;
            }

            return  this.Name == other.Name &&
                    this.Description == other.Description &&
                    this.Link == other.Link;
        }

        /// <summary>
        /// Переинициализация свойств объекта свойств другого объекта.
        /// </summary>
        /// <param name="other"></param>
        public override void Reinitialization(Entity other)
        {
            if (!(other is Section newSection))
            {
                return;
            }

            this.Name = newSection.Name;
            this.Description = newSection.Description;
            this.Link = newSection.Link;
        }
    }
}

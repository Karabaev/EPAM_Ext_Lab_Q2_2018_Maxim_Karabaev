namespace DAL.Model.Entities
{
    using System;

    /// <summary>
    /// Тема на форуме
    /// </summary>
    public class Topic : Entity
    {
        /// <summary>
        /// заголовок темы
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Кем опубликована тема
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// Когда опубликована тема
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Ссылка на тему
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если все свойства сопадают, иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Topic other))
            {
                return false;
            }

            return (ID == other.ID) && this.LikeAs(other);
        }

        /// <summary>
        /// Сравнивает объект с другим (кроме идентификатора). 
        /// </summary>
        /// <param name="entity">Другой объект.</param>
        /// <returns>true если свойства, кроме идентификатора сопадают, иначе false.</returns>
        public override bool LikeAs(Entity entity)
        {
            if (!(entity is Topic other))
            {
                return false;
            }

            return  this.Caption == other.Caption &&
                    this.Author.Equals(other.Author) &&
                    this.CreationDate == other.CreationDate &&
                    this.Link == other.Link;
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
                result =    base.ID.GetHashCode() + 
                            this.Caption.GetHashCode() + 
                            this.Author.GetHashCode() + 
                            this.CreationDate.GetHashCode() + 
                            this.Link.GetHashCode();
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
            if (!(other is Topic newTopic))
            {
                return;
            }

            this.Caption = newTopic.Caption;
            this.Author = newTopic.Author;
            this.CreationDate = newTopic.CreationDate;
            this.Link = newTopic.Link;
        }
    }
}

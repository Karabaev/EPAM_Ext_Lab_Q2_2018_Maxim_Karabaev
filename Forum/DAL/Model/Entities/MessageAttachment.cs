namespace DAL.Model.Entities
{
    using System;

    /// <summary>
    /// Приложение к сообщению.
    /// </summary>
    public class MessageAttachment : Entity
    {
        /// <summary>
        /// Ссылка на приложени.
        /// </summary>
        public string ContentLink { get; set; }
        /// <summary>
        /// Сообщение, к которому относится вложение.
        /// </summary>
        public Message Message { get; set; }

        public MessageAttachment(int id, string link, Message message)
        {
            base.ID = id;
            this.ContentLink = link;
            this.Message = message;
        }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если все свойства сопадают, иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is MessageAttachment other))
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
                result = this.ID.GetHashCode() + this.ContentLink.GetHashCode();
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
            if (!(entity is MessageAttachment other))
            {
                return false;
            }

            return this.ContentLink == other.ContentLink && this.ID == other.ID;
        }

        /// <summary>
        /// Переинициализация свойств объекта свойств другого объекта.
        /// </summary>
        /// <param name="other"></param>
        public override void Reinitialization(Entity other)
        {
            if (!(other is MessageAttachment newAttach))
            {
                return;
            }

            this.ContentLink = newAttach.ContentLink;
            this.Message.Equals(newAttach.Message);
        }
    }
}

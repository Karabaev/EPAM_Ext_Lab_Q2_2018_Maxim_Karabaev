namespace DAL.Model.Entities
{
    using System;

    /// <summary>
    /// Сообщение в теме
    /// </summary>
    public class Message : Entity
    {
        /// <summary>
        /// Кто создал 
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// Когда создана
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Тема.
        /// </summary>
        public Topic Topic { get; set; }

        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        /// <param name="id">Идентификатор сообщения.</param>
        /// <param name="author">Автор сообщения.</param>
        /// <param name="dateTime">Дата и время публикации сообщения.</param>
        /// <param name="content">Содержание сообщения.</param>
        public Message(int id, User author, DateTime dateTime, string content, Topic topic)
        {
            base.ID = id;
            this.Author = author;
            this.CreationDate = dateTime;
            this.Content = content;
            this.Topic = topic;
        }

        /// <summary>
        /// Возвращает строкое представление объекта.
        /// </summary>
        /// <returns>Строковое представление.</returns>
        public override string ToString()
        {
            return string.Format("ID: {0}, Creator: {1}, Date/time of create: {2}", this.ID, this.Author, this.CreationDate);
        }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true если все свойства сопадают, иначе false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Message other))
            {
                return false;
            }

            return ID == other.ID && this.LikeAs(other);
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
                result = this.ID.GetHashCode() +
                    this.Author.GetHashCode() +
                    this.CreationDate.GetHashCode() +
                    this.Content.GetHashCode() +
                    this.Topic.GetHashCode();
            }
            catch(Exception ex)
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
            if (!(other is Message message))
            {
                return;
            }

            this.Author.Equals(message.Author);
            this.CreationDate = message.CreationDate;
            this.Content = message.Content;
            this.Topic = message.Topic;
        }

        /// <summary>
        /// Сравнивает объект с другим (кроме идентификатора). 
        /// </summary>
        /// <param name="entity">Другой объект.</param>
        /// <returns>true если свойства, кроме идентификатора сопадают, иначе false.</returns>
        public override bool LikeAs(Entity entity)
        {
            if (!(entity is Message other))
            {
                return false;
            }

            return this.Author.Equals(other.Author) &&
                this.CreationDate == other.CreationDate &&
                this.Content == other.Content &&
                this.Topic.Equals(other.Topic);
        }
    }
}

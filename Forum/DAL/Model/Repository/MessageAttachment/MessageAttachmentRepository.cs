namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Entities;
    using Core;

    public class MessageAttachmentRepository : BaseRepository<MessageAttachment>, IMessageAttachmentRepository
    {
        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        public MessageAttachmentRepository()
        {
            this.messageRepository = new MessageRepository();
        }

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public override string TableName { get; protected set; } = "MessageAttachments";
        /// <summary>
        /// Репозиторий сообщений.
        /// </summary>
        private readonly MessageRepository messageRepository;

        /// <summary>
        /// Получить все записи сущности.
        /// </summary>
        /// <returns>Контейнер с объектами сущности.</returns>
        public override List<MessageAttachment> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<MessageAttachment> result = new List<MessageAttachment>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new MessageAttachment(   (int)reader["ID"],
                                                            (string)reader["ContentLink"],
                                                            this.messageRepository.GetEntity((int)reader["MessageID"])));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает объект из базы по его ID.
        /// </summary>
        /// <param name="id">ID объекта.</param>
        /// <returns>Объект с указанным ID.</returns>
        public override MessageAttachment GetEntity(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = base.connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName,
                                                                            "*",
                                                                            "ID = {0}", id.ToString());
                MessageAttachment result = null;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    result = new MessageAttachment( (int)reader["ID"],
                                                    (string)reader["ContentLink"],
                                                    this.messageRepository.GetEntity((int)reader["MessageID"]));
                }

                return result;
            }
        }

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>true в случае успеха, иначе false.</returns>
        public override bool SaveEntity(MessageAttachment entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string fieldValues = QueryBuilder.GetValueList(entity.ContentLink, entity.Message.ID);
                base.command.CommandText = QueryBuilder.GetAddRecordCommand(this.TableName, fieldValues);
                base.command.CommandType = CommandType.Text;

                try
                {
                    return base.command.ExecuteNonQuery() > 0;
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Изменение свойств сущности.
        /// </summary>
        /// <param name="entity">Изменяемая сущность.ы</param>
        /// <returns>true в случае успеха, иначе false.</returns>
        public override bool UpdateEntity(MessageAttachment entity)
        {
            if (entity == null || this.GetEntity(entity.ID) == null)
            {
                return false;
            }

            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetUpdateRecordCommand(this.TableName,
                                                                                string.Format("ID = {0}", entity.ID),
                                                                                string.Format("ContentLink = '{0}', MessageID = {1}",
                                                                                entity.ContentLink, entity.Message.ID));

                try
                {
                    return base.command.ExecuteNonQuery() > 0;
                }
                catch (DbException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Не реализовано.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public override List<Section> GetAllEntities(int count)
        {
            throw new NotImplementedException();
        }
    }
}

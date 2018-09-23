namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Entities;
    using Core;

    /// <summary>
    /// Репозиторий сообщений.
    /// </summary>
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        public MessageRepository()
        {
            this.userRepository = new UserRepository();
            this.topicRepository = new TopicRepository();
        }

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public override string TableName { get; protected set; } = "Messages";
        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        private readonly UserRepository userRepository;
        /// <summary>
        /// Репозиторий топиков.
        /// </summary>
        private readonly TopicRepository topicRepository;

        /// <summary>
        /// Получить все записи сущности.
        /// </summary>
        /// <returns>Контейнер с объектами сущности.</returns>
        public override List<Message> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<Message> result = new List<Message>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Message( (int)reader["ID"],
                                                this.userRepository.GetEntity((int)reader["AuthorID"]),
                                                (DateTime)reader["CreationDate"],
                                                (string)reader["Content"],
                                                this.topicRepository.GetEntity((int)reader["TopicID"])));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает сообщение из базы по его ID.
        /// </summary>
        /// <param name="id">ID сообщения.</param>
        /// <returns>Сообщение с указанным ID.</returns>
        public override Message GetEntity(int? id)
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
                Message result = null;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    result = new Message(   (int)reader["ID"],
                                            this.userRepository.GetEntity((int)reader["AuthorID"]),
                                            (DateTime)reader["CreationDate"],
                                            (string)reader["Content"],
                                            this.topicRepository.GetEntity((int)reader["TopicID"]));
                }

                return result;
            }
        }

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>true в случае успеха, иначе false.</returns>
        public override bool SaveEntity(Message entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string fieldValues = QueryBuilder.GetValueList(entity.Author.ID, entity.CreationDate, entity.Content, entity.Topic.ID);
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
        public override bool UpdateEntity(Message entity)
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
                                                                                string.Format("AuthorID = {0}, CreationDate = '{1}', Content = '{2}', TopicID = {3}",
                                                                                entity.Author.ID, entity.CreationDate, entity.Content, entity.Topic.ID));

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
        public override List<Message> GetAllEntities(int count)
        {
            throw new NotImplementedException();
        }
    }
}

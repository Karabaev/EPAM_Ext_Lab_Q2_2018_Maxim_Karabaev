namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Entities;
    using Core;

    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        public TopicRepository()
        {
            this.userRepository = new UserRepository();
            this.sectionRepository = new SectionRepository();
        }

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public override string TableName { get; protected set; } = "Topics";
        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        private readonly UserRepository userRepository;
        /// <summary>
        /// Репозиторий разделов.
        /// </summary>
        private readonly SectionRepository sectionRepository;

        /// <summary>
        /// Получить все записи сущности.
        /// </summary>
        /// <returns>Контейнер с объектами сущности.</returns>
        public override List<Topic> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<Topic> result = new List<Topic>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Topic((int)reader["ID"],
                                                (string)reader["Caption"],
                                                this.userRepository.GetEntity((int)reader["AuthorID"]),
                                                (DateTime)reader["CreationDate"],
                                                (string)reader["Link"],
                                                this.sectionRepository.GetEntity((int)reader["SectionID"])));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает топик из базы по его ID.
        /// </summary>
        /// <param name="id">ID топик.</param>
        /// <returns>Топик с указанным ID.</returns>
        public override Topic GetEntity(int? id)
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
                Topic result = null;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    result = new Topic( (int)reader["ID"],
                                        (string)reader["Caption"],
                                        this.userRepository.GetEntity((int)reader["AuthorID"]),
                                        (DateTime)reader["CreationDate"],
                                        (string)reader["Link"],
                                        this.sectionRepository.GetEntity((int)reader["SectionID"]));
                }

                return result;
            }
        }

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>true в случае успеха, иначе false.</returns>
        public override bool SaveEntity(Topic entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string fieldValues = QueryBuilder.GetValueList(entity.Caption, entity.Author.ID, entity.CreationDate, entity.Link, entity.Section.ID);
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
        public override bool UpdateEntity(Topic entity)
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
                                                                                string.Format("Caption = '{0}', AuthorID = {1}, CreationDate = '{2}', Link = '{3}, SectionID = {4}",
                                                                                entity.Caption, entity.Author.ID, entity.CreationDate, entity.Link, entity.Section.ID));

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
        public override List<Topic> GetAllEntities(int count)
        {
            throw new NotImplementedException();
        }
    }
}

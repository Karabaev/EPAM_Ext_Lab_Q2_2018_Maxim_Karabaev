namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Entities;
    using Core;

    /// <summary>
    /// Репозиторий разделов форума.
    /// </summary>
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        public SectionRepository() { }

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public override string TableName { get; protected set; } = "Sections";

        /// <summary>
        /// Получить все записи сущности.
        /// </summary>
        /// <returns>Контейнер с объектами сущности.</returns>
        public override List<Section> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<Section> result = new List<Section>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Section((int)reader["ID"],
                                                (string)reader["Name"],
                                                (string)reader["Description"],
                                                (string)reader["Link"]));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает раздел из базы по его ID.
        /// </summary>
        /// <param name="id">ID раздела.</param>
        /// <returns>Раздел с указанным ID.</returns>
        public override Section GetEntity(int? id)
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
                Section result = null;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    result = new Section(   (int)reader["ID"],
                                            (string)reader["Name"],
                                            (string)reader["Description"],
                                            (string)reader["Link"]);
                }

                return result;
            }
        }

        /// <summary>
        /// Добавление сущности.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>true в случае успеха, иначе false.</returns>
        public override bool SaveEntity(Section entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string fieldValues = QueryBuilder.GetValueList(entity.Name, entity.Description, entity.Link);
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
        public override bool UpdateEntity(Section entity)
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
                                                                                string.Format("Name = '{0}', Description = '{1}', Link = '{2}'", 
                                                                                entity.Name, entity.Description, entity.Link));

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

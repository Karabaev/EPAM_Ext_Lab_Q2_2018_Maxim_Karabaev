namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using DAL.Model.Entities;
    using System.Linq;
    using Core;

    /// <summary>
    /// Репозиторий ролей.
    /// </summary>
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository() { }

        /// <summary>
        /// Инициализирует объект в памяти.
        /// </summary>
        /// <param name="connString">Строка подключения к базе.</param>
        /// <param name="factory">Поставщик классов для источника данных.</param>
        public RoleRepository(string connString, DbProviderFactory factory) : base(connString, factory)
        {
        }

        /// <summary>
        /// Название таблицы.
        /// </summary>
        public override string TableName { get; protected set; } = "Roles";

        /// <summary>
        /// Возвращает роль из базы по ее ID.
        /// </summary>
        /// <param name="id">ID роли.</param>
        /// <returns>Роль с указанным ID.</returns>
        public override Role GetEntity(int? id)
        {
            if(!id.HasValue)
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
                Role result = null;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    result = new Role((int)reader["ID"], (string)reader["Name"], (int)reader["AccessLevel"]);
                }

                return result;
            }
        }

        public override List<Role> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<Role> result = new List<Role>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Role((int)reader["ID"], (string)reader["Name"], (int)reader["AccessLevel"]));
                    }
                }

                return result;
            }
        }

        public override List<Role> GetAllEntities(int count)
        {
            throw new NotImplementedException();
        }

        public override bool SaveEntity(Role entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string fieldValues = QueryBuilder.GetValueList(entity.Name, entity.AccessLevel);
                base.command.CommandText = QueryBuilder.GetAddRecordCommand(this.TableName, fieldValues);
                base.command.CommandType = CommandType.Text;
                try
                {
                    return base.command.ExecuteNonQuery() > 0;
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public override bool UpdateEntity(Role entity)
        {
            if(entity == null || this.GetEntity(entity.ID) == null)
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
                                                                                string.Format("Name = '{0}', AccessLevel = {1}", entity.Name, entity.AccessLevel));

                try
                {
                    return base.command.ExecuteNonQuery() > 0;
                }
                catch (DbException)
                {
                    throw new Exception();
                }
            }  
        }
    }
}

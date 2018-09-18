namespace DAL.Model.Repository
{
    using System.Collections.Generic;
    using DAL.Model.Entities;
    using System.Data.Common;
    using DAL.Core;
    using System.Data;
    using System;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public override string TableName { get; protected set; } = "Users"; 
        private readonly RoleRepository roleRepository;

        public UserRepository()
        {
            this.roleRepository = new RoleRepository();
        }

        /// <summary>
        /// Инициализирует объект репозитория пользователей в памяти.
        /// </summary>
        /// <param name="connString">Строка подключения.</param>
        /// <param name="provider">Поставщик классов для работы с бд. б</param>
        /// <param name="roleRepos">Репозиторий ролей.</param>
        public UserRepository(string connString, DbProviderFactory provider, RoleRepository roleRepos) : base(connString, provider)
        {
            this.roleRepository = roleRepos;
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        public override List<User> GetAllEntities()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                base.command.CommandType = CommandType.Text;
                DbDataReader reader = command.ExecuteReader();
                List<User> result = new List<User>();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        bool b = (bool)reader["IsBanned"];
                        result.Add(new User((int)reader["ID"], 
                                            (string)reader["Login"], 
                                            (string)reader["PasswordHash"],
                                            (string)reader["PublicName"], 
                                            this.roleRepository.GetEntity((int?)reader["UserRole"]),
                                            (bool)reader["IsBanned"], 
                                            (DateTime)reader["RegistrationDate"],
                                            (string)reader["Email"]));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Получить первых count пользователей из таблицы.
        /// </summary>
        /// <param name="count">Количество получаемых пользвателей.</param>
        /// <returns>Список пользователей.</returns>
        public override List<User> GetAllEntities(int count)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = base.connectionString;
                base.connection.Open();

                DbParameter countParameter = factory.CreateParameter();
                countParameter.ParameterName = "@Count";
                countParameter.Value = count;

                DbDataReader reader = QueryBuilder.GetStoredProcedureDataReader(connection, "GetAllUsers", countParameter);
                List<User> result = new List<User>();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        result.Add(new User((int)reader["ID"],
                                            (string)reader["Login"],
                                            (string)reader["PasswordHash"],
                                            (string)reader["PublicName"],
                                            roleRepository.GetEntity((int)reader["UserRole"]),
                                            (bool)reader["IsBanned"],
                                            (DateTime)reader["RegistrationDate"],
                                            (string)reader["Email"]));
                    }
                }
                return result;
            }
        }

        public override User GetEntity(int? id)
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
                                                                            QueryBuilder.GetEntityProperties(typeof(User)),
                                                                            "ID = {0}", id.ToString());
                User result = null;
                DbDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    if(reader.Read())
                    {
                        result = new User(  (int)reader["ID"],
                                            (string)reader["Login"],
                                            (string)reader["PasswordHash"],
                                            (string)reader["PublicName"],
                                            roleRepository.GetEntity((int)reader["UserRole"]),
                                            (bool)reader["IsBanned"],
                                            (DateTime)reader["RegistrationDate"],
                                            (string)reader["Email"]);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Добавить пользователя в базу.
        /// </summary>
        /// <param name="entity">Сущность для добавления в базу.</param>
        /// <returns>true - успех, иначе false.</returns>
        public override bool SaveEntity(User entity)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                string tableFields = "Login, PasswordHash, PublicName, UserRole, IsBanned, RegistrationDate, Email";
                string fieldValues = string.Format("'{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}'", entity.Login, entity.PasswordHash,
                    entity.PublicName, entity.UserRole.ID, entity.IsBanned, entity.RegistrationDate, entity.Email);
                base.command.CommandText = QueryBuilder.GetAddRecordCommand(this.TableName, tableFields, fieldValues);
                base.command.CommandType = CommandType.Text;
                try
                {
                    return base.command.ExecuteNonQuery() > 0;
                }
                catch(DbException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public override bool UpdateEntity(User entity)
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
                string fieldsAndValues = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", 
                                                            string.Format("Login = '{0}'", entity.Login),
                                                            string.Format("PasswordHash = '{0}'", entity.PasswordHash),
                                                            string.Format("PublicName = '{0}'", entity.PublicName),
                                                            string.Format("UserRole = {0}", entity.UserRole.ID),
                                                            string.Format("IsBanned = '{0}'", entity.IsBanned),
                                                            string.Format("Email = '{0}'", entity.Email));
                base.command.CommandText = QueryBuilder.GetUpdateRecordCommand(this.TableName,
                                                                                string.Format("ID = {0}", entity.ID),
                                                                                fieldsAndValues);

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

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
        public override string TableName { get; } = "Users";
        private readonly RoleRepository roleRepository;


        public UserRepository(string connString, DbProviderFactory provider, RoleRepository roleRepos) : base(connString, provider)
        {
            this.roleRepository = roleRepos;
        }

        public override List<T> GetAllEntities<T>()
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName, "*");
                DbDataReader reader = command.ExecuteReader();
                List<User> result = new List<User>();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        result.Add(new User((uint?)reader["UserID"], (string)reader["Login"], (string)reader["PasswordHash"],
                            (string)reader["PublicName"], roleRepository.GetEntity<Role>((uint)reader["RoleID"]),
                            (bool)reader["IsBanned"], (FormattedDate)reader["RegistrationDate"]));
                    }
                }
                return result as List<T>;
            }
        }

        public override T GetEntity<T>(uint id)
        {
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

                        result = new User((uint?)reader["UserID"], (string)reader["Login"], (string)reader["PasswordHash"], 
                            (string)reader["PublicName"], roleRepository.GetEntity<Role>((uint)reader["RoleID"]), 
                            (bool)reader["IsBanned"], (FormattedDate)reader["RegistrationDate"]);
                    }
                }

                return result as T;
            }
        }

        public override int RemoveEntity(uint id)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = base.connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetDeleteRecordCommand(this.TableName, "ID = {0}", id.ToString());
                base.command.CommandType = CommandType.Text;
                return base.command.ExecuteNonQuery();
            }
        }

        public override bool SaveEntity<T>(T entity)
        {
            User newUser = entity as User;

            string tableFields = string.Empty, fieldValues = string.Empty;
            QueryBuilder.GetEntityPropertiesAndValues(newUser, out tableFields, out fieldValues);
            Console.WriteLine("entityID " + entity.ID);
            Console.WriteLine("newUserID " + newUser.ID);
            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetAddRecordCommand(this.TableName, tableFields, fieldValues);
                base.command.CommandType = CommandType.Text;
                return base.command.ExecuteNonQuery() > 0;
            }
        }
    }
}

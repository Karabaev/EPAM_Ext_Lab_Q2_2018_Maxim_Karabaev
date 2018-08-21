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
                List<T> entities = new List<T>();
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                //var command = new SqlC
                return entities;
            }
        }

        public override T GetEntity<T>(uint id)
        {
            using (base.connection = base.factory.CreateConnection())
            {
                User result = null;
                base.connection.ConnectionString = base.connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName,
                                                                            QueryBuilder.GetEntiyProperties(typeof(User)),
                                                                            "ID = {0}", id.ToString());
                DbDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    if(reader.Read())
                    {

                        result = new User((uint?)reader["id"], (string)reader["Login"], (string)reader["PasswordHash"], 
                            (string)reader["PublicName"], roleRepository.GetEntity<Role>((uint)reader["RoleID"]), 
                            (bool)reader["IsBanned"], (DateTime)reader["RegistrationDate"]);
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

            //Dictionary<string, object> propValues = new Dictionary<string, object>();

            //foreach (var item in newUser.GetType().GetProperties())
            //{
            //    propValues.Add(item.Name, item.PropertyType.IsSubclassOf(typeof(Entity)) ?  base.IDProperty.GetValue(newUser) :  
            //                                                                                item.GetValue(newUser));
            //}

            //StringBuilder tableFields = new StringBuilder();
            //StringBuilder fieldValues = new StringBuilder();
            //string delimiter = ",";
            //int index = 1;
            //foreach (var item in propValues)
            //{
            //    tableFields.Append(item.Key);
            //    fieldValues.Append(item.Value);
            //    if(index < propValues.Count)
            //    {
            //        tableFields.Append(delimiter);
            //        fieldValues.Append(delimiter);
            //    }

            //    index++;
            //}
            User newUser = entity as User;
            string tableFields = string.Empty, fieldValues = string.Empty;
            QueryBuilder.GetEntityPropertiesAndValues(newUser, out tableFields, out fieldValues);

            using (base.connection = base.factory.CreateConnection())
            {
                base.connection.ConnectionString = connectionString;
                base.connection.Open();
                base.command = base.connection.CreateCommand();
                base.command.CommandText = QueryBuilder.GetAddRecordCommand(this.TableName, tableFields.ToString(), fieldValues.ToString());
                base.command.CommandType = CommandType.Text;
                return base.command.ExecuteNonQuery() > 0;
            }
        }
    }
}

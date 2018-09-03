namespace DAL.Core.DatabaseToModelMapper.Repository
{
    using DAL.Core.DatabaseToModelMapper.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Common;
    using System.Reflection;

    public class UserRepository
    {
        public UserRepository(string connString, DbProviderFactory factory)
        {
            this.connectionString = connString;
            this.factory = factory;
            User.Remap("Name", "UserName");
            User.Remap("RegDate", "Date");
        }



        public User GetUser(int id)
        {
            using (this.connection = this.factory.CreateConnection())
            {
                this.connection.ConnectionString = this.connectionString;
                this.connection.Open();
                this.command = this.connection.CreateCommand();
                this.command.CommandText = "SELECT * FROM " + User.Mapper.TableName + " WHERE ID = " + id;
                User result = null;
                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    result = new User();
                    foreach (var item in typeof(User).GetProperties())
                    {
                        MapTrio map = User.Mapper.PropertyMapTrios.Where(pmt => pmt.Property == item).FirstOrDefault();
                      //  item.SetValue(result, Converter.ConvertFromDBValue(reader[map.TableFieldName]));
                        item.SetValue(result, reader[map.TableFieldName]);
                        //object obj = reader[map.TableFieldName];
                        //if (item.Name != "RegDate")
                        //    item.SetValue(result, reader[map.TableFieldName]);
                        //else
                        //    result.RegDate = (DateTime)reader[map.TableFieldName];
                    }
                }
                return result;
            }
        }

        protected string connectionString;
        protected DbProviderFactory factory;
        protected DbCommand command;
        protected DbConnection connection;
        //public User GetEntity(int? id)
        //{
        //    if (!id.HasValue)
        //    {
        //        return null;
        //    }

        //    using (base.connection = base.factory.CreateConnection())
        //    {
        //        base.connection.ConnectionString = base.connectionString;
        //        base.connection.Open();
        //        base.command = base.connection.CreateCommand();
        //        base.command.CommandText = QueryBuilder.GetSelectRecordCommand(this.TableName,
        //                                                                    QueryBuilder.GetEntityProperties(typeof(User)),
        //                                                                    "ID = {0}", id.ToString());
        //        User result = null;
        //        DbDataReader reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            if (reader.Read())
        //            {
        //                result = new User((int?)reader["ID"],
        //                                    (string)reader["Login"],
        //                                    (string)reader["PasswordHash"],
        //                                    (string)reader["PublicName"],
        //                                    roleRepository.GetEntity<Role>((int)reader["UserRole"]),
        //                                    (bool)reader["IsBanned"],
        //                                    (DateTime)reader["RegistrationDate"],
        //                                    (string)reader["Email"]);
        //            }
        //        }

        //        return result;
        //    }
        //}
    }
}

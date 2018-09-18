namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using Entities;
    using System.Data;
    using Core;

    public abstract class BaseRepository<T> : IBaseRepository<T> where T: Entity
    {
        protected string connectionString;
        protected DbProviderFactory factory;
        protected DbCommand command;
        protected DbConnection connection;

        public BaseRepository()
        {
            this.connectionString = "Data Source=(local);Initial Catalog=Forum;Integrated Security=True";
            this.factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        public BaseRepository(string connString, DbProviderFactory factory)
        {
            this.connectionString = connString;
            this.factory = factory;
        }

        public int RemoveEntity(int? id)
        {
            if (!id.HasValue)
            {
                return 0;
            }

            using (this.connection = this.factory.CreateConnection())
            {
                this.connection.ConnectionString = this.connectionString;
                this.connection.Open();
                this.command = this.connection.CreateCommand();
                this.command.CommandText = QueryBuilder.GetDeleteRecordCommand(this.TableName, "ID = {0}", id.ToString());
                this.command.CommandType = CommandType.Text;
                return this.command.ExecuteNonQuery();
            }
        }

        public int RemoveAllEntities()
        {
            using (this.connection = this.factory.CreateConnection())
            {
                this.connection.ConnectionString = connectionString;
                this.connection.Open();
                this.command = this.connection.CreateCommand();
                this.command.CommandText = QueryBuilder.GetDeleteAllRecordsCommand(this.TableName);
                this.command.CommandType = CommandType.Text;
                return this.command.ExecuteNonQuery();
            }
        }

        public abstract string TableName { get; protected set; }

        public abstract List<T> GetAllEntities();
        public abstract List<T> GetAllEntities(int count);
        public abstract T GetEntity(int? id);
        public abstract bool SaveEntity(T entity);
        public abstract bool UpdateEntity(T entity);
    }
}

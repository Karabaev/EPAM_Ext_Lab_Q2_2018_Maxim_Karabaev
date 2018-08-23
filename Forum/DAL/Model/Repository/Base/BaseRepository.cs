namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using DAL.Model.Entities;

    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        protected string connectionString;
        protected DbProviderFactory factory;
        protected DbCommand command;
        protected DbConnection connection;

        public BaseRepository(string connString, DbProviderFactory factory)
        {
            this.connectionString = connString;
            this.factory = factory;
        }

        public abstract string TableName { get; }

        public abstract List<T> GetAllEntities<T>() where T : Entity;
        public abstract List<T> GetAllEntities<T>(int count) where T : Entity;
        public abstract T GetEntity<T>(int? id) where T : Entity;
        public abstract int RemoveAllEntities();
        public abstract int RemoveEntity(int? id);
        public abstract bool SaveEntity<T>(T entity) where T : Entity;
    }
}

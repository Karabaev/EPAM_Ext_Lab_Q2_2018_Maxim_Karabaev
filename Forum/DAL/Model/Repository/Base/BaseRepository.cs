namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using DAL.Model.Entities;

    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected string connectionString;
        protected DbProviderFactory factory;
        protected DbCommand command;
        protected DbConnection connection;
        //protected PropertyInfo IDProperty;

        //private const string IDPropName = "ID";

        public BaseRepository(string connString, DbProviderFactory factory)
        {
            this.connectionString = connString;
            this.factory = factory;
            //this.IDProperty = typeof(Entity).GetProperty(IDPropName);
        }

        public virtual List<T> GetAllEntities<T>() where T : Entity
        {       
            throw new NotImplementedException();
        }

        public virtual T GetEntity<T>(uint id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public virtual int RemoveEntity(uint id) 
        {
            throw new NotImplementedException();
        }

        public virtual bool SaveEntity<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public virtual string TableName { get; } = "Default";
    }
}

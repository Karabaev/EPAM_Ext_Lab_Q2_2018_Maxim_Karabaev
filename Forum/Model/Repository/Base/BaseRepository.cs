namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Model.Entities;
    using System.Data;
    using System.Data.Common;
    using System.Configuration;


    public class BaseRepository<T> : IBaseRepository<T>
    {
        protected string connectionString;
        protected DbProviderFactory factory;

        public BaseRepository(string connString, DbProviderFactory factory)
        {
            this.connectionString = connString;
            this.factory = factory;
        }

        public virtual List<T> GetAllEntities<T>() where T : Entity, new()
        {       
            throw new NotImplementedException();
        }

        public virtual T GetEntity<T>(uint id) where T : Entity, new()
        {
            throw new NotImplementedException();
        }

        public virtual bool RemoveEntity(uint id)
        {
            throw new NotImplementedException();
        }

        public virtual bool SaveEntity<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}

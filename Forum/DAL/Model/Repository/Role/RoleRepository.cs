namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using DAL.Model.Entities;

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public override string TableName { get; } = "Roles";

        public RoleRepository(string connString, DbProviderFactory factory) : base(connString, factory){}

        

        public override List<T> GetAllEntities<T>()
        {
            return base.GetAllEntities<T>();
        }

        public override T GetEntity<T>(uint id)
        {
            return base.GetEntity<T>(id);
        }

        public override int RemoveEntity(uint id)
        {
            return base.RemoveEntity(id);
        }

        public override bool SaveEntity<T>(T entity)
        {
            return base.SaveEntity(entity);
        }
    }
}

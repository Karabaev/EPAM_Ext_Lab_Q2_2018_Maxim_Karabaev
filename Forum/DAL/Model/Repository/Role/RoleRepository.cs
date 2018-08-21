namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using DAL.Model.Entities;

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public override string TableName { get; } = "Roles";
        /// <summary>
        /// Хранилище ролей для тестов.
        /// </summary>
        private readonly List<Role> roles = new List<Role>();

        public RoleRepository(string connString, DbProviderFactory factory) : base(connString, factory)
        {
            roles = new List<Role>();
            roles.Add(new Role(0, "r0", new List<Permission>()));
            roles.Add(new Role(1, "r1", new List<Permission>()));
            roles.Add(new Role(2, "r2", new List<Permission>()));
        }

        

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

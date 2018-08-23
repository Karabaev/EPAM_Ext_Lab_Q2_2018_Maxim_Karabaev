namespace DAL.Model.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using DAL.Model.Entities;
    using System.Linq;

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

        public override T GetEntity<T>(int? id)
        {
            if(!id.HasValue)
            {
                return null;
            }

            return roles.Where(r => r.ID == id).FirstOrDefault() as T;
        }

        public override List<T> GetAllEntities<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> GetAllEntities<T>(int count)
        {
            throw new NotImplementedException();
        }

        public override int RemoveAllEntities()
        {
            throw new NotImplementedException();
        }

        public override int RemoveEntity(int? id)
        {
            throw new NotImplementedException();
        }

        public override bool SaveEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

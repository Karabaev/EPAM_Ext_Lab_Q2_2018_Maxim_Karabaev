using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    class PermissionRepository : Repository
    {
        public List<Permission> Permissions { get; set; }

        public PermissionRepository()
        {
            Permissions = new List<Permission>();
            Permissions.Add(new Permission { ID = 0, Name = "Edit all messages", Description = string.Empty });
            Permissions.Add(new Permission { ID = 1, Name = "Edit topics", Description = string.Empty });
            Permissions.Add(new Permission { ID = 2, Name = "Edit users", Description = string.Empty });
            Permissions.Add(new Permission { ID = 3, Name = "Edit sections", Description = string.Empty });
            Permissions.Add(new Permission { ID = 4, Name = "Access to admin panel", Description = string.Empty });
            Permissions.Add(new Permission { ID = 5, Name = "Edit own messages", Description = string.Empty });
            Permissions.Add(new Permission { ID = 6, Name = "View messages in all sections", Description = string.Empty });
        }

        public override List<T> GetAllEntities<T>()
        {
            return Permissions as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Permissions.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return Permissions.Remove(Permissions.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            int startCount = Permissions.Count;
            Permissions.Add(entity as Permission);
            return Permissions.Count > startCount ? true : false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task5.Repository
{
    class RoleRepository : Repository
    {
        public List<Role> Roles { get; set; }

        public RoleRepository()
        {
            PermissionRepository permissionRepos = new PermissionRepository();
            Roles = new List<Role>();
            Roles.Add(new Role
            {
                ID = 0,
                Name = "Admin",
                Permissions = new List<Permission> {   permissionRepos.Permissions[0],
                                                       permissionRepos.Permissions[1],
                                                       permissionRepos.Permissions[2],
                                                       permissionRepos.Permissions[3],
                                                       permissionRepos.Permissions[4],
                                                       permissionRepos.Permissions[5],
                                                       permissionRepos.Permissions[6]}
            });
            Roles.Add(new Role
            {
                ID = 1,
                Name = "Authorised user",
                Permissions = new List<Permission> {permissionRepos.Permissions[5],
                                                    permissionRepos.Permissions[6]}
            });
            Roles.Add(new Role
            {
                ID = 2,
                Name = "Unauthorised user",
                Permissions = new List<Permission> { permissionRepos.Permissions[6] }
            });
        }

        public override List<T> GetAllEntities<T>()
        {
            return Roles as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Roles.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            return Roles.Remove(Roles.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            int startCount = Roles.Count;
            Roles.Add(entity as Role);
            return Roles.Count > startCount ? true : false;
        }
    }
}

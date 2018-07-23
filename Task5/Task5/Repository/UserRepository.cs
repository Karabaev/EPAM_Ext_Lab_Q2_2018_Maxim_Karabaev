using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{
    public class UserRepository : Repository
    {
        public List<User> Users { get; set; }

        public UserRepository()
        {
            RoleRepository roleRepository = new RoleRepository();
            Users = new List<User>();
            Users.Add(new User { ID = 0, IsBanned = false, Login = "u1", PasswordHash = "u1", PublicName = "u1", UserRole = roleRepository.Roles[0] });
            Users.Add(new User { ID = 1, IsBanned = false, Login = "u2", PasswordHash = "u2", PublicName = "u2", UserRole = roleRepository.Roles[1] });
            Users.Add(new User { ID = 2, IsBanned = false, Login = "u3", PasswordHash = "u3", PublicName = "u3", UserRole = roleRepository.Roles[1] });
            Users.Add(new User { ID = 3, IsBanned = false, Login = "u4", PasswordHash = "u4", PublicName = "u4", UserRole = roleRepository.Roles[1] });
            Users.Add(new User { ID = 4, IsBanned = false, Login = "u5", PasswordHash = "u5", PublicName = "u5", UserRole = roleRepository.Roles[2] });
            Users.Add(new User { ID = 5, IsBanned = false, Login = "u6", PasswordHash = "u6", PublicName = "u6", UserRole = roleRepository.Roles[2] });
            Users.Add(new User { ID = 6, IsBanned = false, Login = "u7", PasswordHash = "u7", PublicName = "u7", UserRole = roleRepository.Roles[2] });
        }

        public override List<T> GetAllEntities<T>()
        {
            return Users as List<T>;
        }

        public override T GetEntity<T>(uint id)
        {
            return Users.Where(m => m.ID == id).FirstOrDefault() as T;
        }

        public override bool Remove(uint id)
        {
            //User user = Users.Where(m => m.ID == id).FirstOrDefault();
            //if (user == null) return false;
            //return Users.Remove(user);
            return Users.Remove(Users.Where(m => m.ID == id).FirstOrDefault());
        }

        public override bool SaveEntity<T>(T entity)
        {
            int startCount = Users.Count;
            Users.Add(entity as User);
            return Users.Count > startCount ? true : false;
        }
    }
}

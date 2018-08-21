namespace DAL.Model.Service
{
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using System.Collections.Generic;
    public class UserService : BaseService<User>
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository repos)
        {
            this.userRepository = repos;
        }

        public List<User> GetAllEntities()
        {
            return this.userRepository.GetAllEntities<User>();
        }

        public User GetEntity(uint id)
        {
            return this.userRepository.GetEntity<User>(id);
        }

        public override bool RemoveEntity(uint id)
        {
            return this.userRepository.RemoveEntity(id);
        }

        public bool SaveEntity(User entity)
        {
            return this.userRepository.SaveEntity(entity);
        }
    }
}

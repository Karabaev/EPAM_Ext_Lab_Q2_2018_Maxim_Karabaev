namespace DAL.Model.Service
{
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using System.Collections.Generic;

    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository repos)
        {
            this.userRepository = repos;
        }

        public override List<User> GetAllEntities()
        {
            return this.userRepository.GetAllEntities();
        }

        public override User GetEntity(int id)
        {
            return this.userRepository.GetEntity(id);
        }

        public override int RemoveEntity(int id)
        {
            return this.userRepository.RemoveEntity(id);
        }

        public override bool SaveEntity(User entity)
        {
            return this.userRepository.SaveEntity(entity);
        }
    }
}

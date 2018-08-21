namespace DAL.Model.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Model.Entities;
    using System.Data.Common;
    using System.Data.SqlTypes;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(string connString, DbProviderFactory provider) : base(connString, provider){}


        public override List<T> GetAllEntities<T>()
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = new SqlC
            }
        }

        public override T GetEntity<T>(uint id)
        {
            throw new System.NotImplementedException();
        }

        public override bool RemoveEntity(uint id)
        {
            throw new System.NotImplementedException();
        }

        public override bool SaveEntity<T>(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

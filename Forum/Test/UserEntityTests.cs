namespace Test
{
    using System;
    using DAL;
    using DAL.Model;
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using DAL.Model.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Тесты сущности, репозитория, сервиса User.
    /// </summary>
    [TestClass]
    public class UserEntityTests
    {
        private const string connString = "Data Source=(local);Initial Catalog=Forum;Integrated Security=True";
        private const string factoryString = "System.Data.SqlClient";

        /// <summary>
        /// Тест метода UserRepository.SaveEntity.
        /// </summary>
        public void ReposSaveEntityTest()
        {
            UserRepository userRepos = new UserRepository(  connString, 
                                                            DbProviderFactories.GetFactory(factoryString), 
                                                            new RoleRepository( connString, 
                                                                                DbProviderFactories.GetFactory(factoryString)));
            //User newUser = new User(0, "")
            //userRepos.SaveEntity<User>()

        }
    }
}

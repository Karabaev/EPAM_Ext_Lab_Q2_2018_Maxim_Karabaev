namespace Test
{
    using System;
    using DAL;
    using DAL.Model;
    using DAL.Core;
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using DAL.Model.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data;
    using System.Data.Common;
    using System.Collections.Generic;

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
        [TestMethod]
        public void ReposSaveEntityTest()
        {
            UserRepository userRepos = new UserRepository(  connString, 
                                                            DbProviderFactories.GetFactory(factoryString), 
                                                            new RoleRepository( connString, 
                                                                                DbProviderFactories.GetFactory(factoryString)));
            Role testRole = new Role(1, "r1", new List<Permission>());
            User testUser = new User(4, "u1", "u1", "u1", testRole, false, DateTime.Now);


            Assert.IsTrue(userRepos.SaveEntity(testUser));

        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления несуществующей строки.
        /// </summary>
        [TestMethod]
        public void ReposRemoveRemoveNotExistEntity()
        {
            UserRepository userRepos = new UserRepository(connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository(connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            Assert.IsFalse(userRepos.RemoveEntity(0) != 0);
        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления существующей строки.
        /// </summary>
        [TestMethod]
        public void ReposRemoveRemoveOneExistEntity()
        {
            UserRepository userRepos = new UserRepository(connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository(connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            Assert.IsFalse(userRepos.RemoveEntity(1) != 1);
        }
    }
}

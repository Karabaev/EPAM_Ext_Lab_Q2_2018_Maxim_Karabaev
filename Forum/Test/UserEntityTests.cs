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
    using System.Linq;

    /// <summary>
    /// Тесты сущности, репозитория, сервиса User.
    /// </summary>
    [TestClass]
    public class UserEntityTests
    {
        private const string connString = "Data Source=(local);Initial Catalog=Forum;Integrated Security=True";
        private const string factoryString = "System.Data.SqlClient";
        private const int ExistID = 18;
        private const int NotExistID = 1000;
        private const int DisplayUsersCount = 1;

        /// <summary>
        /// Тест метода User.Equals. При сравнении используются разные инстансы ролей и списков разрешений.
        /// </summary>
        [TestMethod]
        public void UserEqualsDifferentRoleInstancesTest()
        {
            User user1 = new User(1, "u1", "u1", "u1", new Role(1, "r1", new List<Permission>()), false, DateTime.Now.Date, "u1@ya.ru");
            User user2 = new User(1, "u1", "u1", "u1", new Role(1, "r1", new List<Permission>()), false, DateTime.Now.Date, "u1@ya.ru");
            Assert.IsTrue(user1.Equals(user2));
        }

        /// <summary>
        /// Тест метода User.Equals. При сравнении используется один инстанс роли и списка разрешений.
        /// </summary>
        [TestMethod]
        public void UserEqualsSingleRoleInstanceTest()
        {
            Role role = new Role(1, "r1", new List<Permission>());
            User user1 = new User(1, "u1", "u1", "u1", role, false, DateTime.Now.Date, "u1@ya.ru");
            User user2 = new User(1, "u1", "u1", "u1", role, false, DateTime.Now.Date, "u1@ya.ru");
            Assert.IsTrue(user1.Equals(user2));
        }

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
            User testUser = new User(4, "u1", "u1", "u1", testRole, false, DateTime.Now, "u1@ya.ru");
            Assert.IsTrue(userRepos.SaveEntity(testUser));

        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления несуществующей строки.
        /// </summary>
        [TestMethod]
        public void ReposRemoveNotExistEntity()
        {
            UserRepository userRepos = new UserRepository(connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository(connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            Assert.IsTrue(userRepos.RemoveEntity(NotExistID) == 0);
        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления существующей строки. Тест одноразовый.
        /// </summary>
        [TestMethod]
        public void ReposRemoveOneExistEntity()
        {
            UserRepository userRepos = new UserRepository(connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository(connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            User newUser = new User(null, "u", "u", "u", new Role(1, "r", new List<Permission>()), false, DateTime.Now, "u");

            if(!userRepos.SaveEntity(newUser))
            {
                Assert.Fail("Не удалось добавить запись в базу.");
            }

            List<User> users = userRepos.GetAllEntities<User>();
            int? id = 0;

            if(users == null || users.Count == 0 || users[0] == null)
            {
                Assert.Fail("Не удалось загрузить записи из базы.");
            }
            id = users[0].ID;
            Assert.IsFalse(userRepos.RemoveEntity(id) != 1);
        }

        /// <summary>
        /// Тест метода UserRepository.GetAllEntities
        /// </summary>
        [TestMethod]
        public void ReposGetAllEntitiesTest()
        {
            UserRepository userRepos = new UserRepository(  connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository( connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            
            List<User> expectedUsers = new List<User>();
            Role role = new Role(1, "r1", new List<Permission>());
            FormattedDate date = new DateTime(2018, 08, 22);
            expectedUsers.Add(new User(18, "u1", "u1", "u1", role, false, date, "u1@ya.ru"));
            expectedUsers.Add(new User(18, "u2", "u2", "u2", role, false, date, "u2@ya.ru"));
            userRepos.RemoveAllEntities();

            foreach (var item in expectedUsers)
            {
                userRepos.SaveEntity(item);
            }

            List<User> users = userRepos.GetAllEntities<User>();

            if (expectedUsers.Count != users.Count)
            {
                Assert.Fail("Количество записей, полученных из базы не совпадает с ожидаемым количеством.");
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (!users[i].LikeAs(expectedUsers[i]))
                {
                    Assert.Fail("Запись из базы {0} не подобна\n ожидаемой {1}", users[i], expectedUsers[i]);
                }
            }

            int count = 0;

            foreach (var item in users)
            {
                count += expectedUsers.Where(eu => eu.LikeAs(item)).Count();
            }

            Assert.IsTrue((count == users.Count) && (count == expectedUsers.Count));
        }

        /// <summary>
        /// Тест перегрузки метода UserRepository.GetAllEntities с указанием количества выводимых записей.
        /// </summary>
        [TestMethod]
        public void ReposGetTopNEntitiesTest()
        {
            UserRepository userRepos = new UserRepository(  connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository( connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            List<User> addedUsers = new List<User>();
            Role role = new Role(1, "r1", new List<Permission>());
            FormattedDate date = new DateTime(2018, 08, 22);
            addedUsers.Add(new User(18, "u1", "u1", "u1", role, false, date, "u1@ya.ru"));
            addedUsers.Add(new User(18, "u2", "u2", "u2", role, false, date, "u2@ya.ru"));
            userRepos.RemoveAllEntities();

            foreach (var item in addedUsers)
            {
                userRepos.SaveEntity(item);
            }

            List<User> users = userRepos.GetAllEntities<User>(DisplayUsersCount);

            if (DisplayUsersCount != users.Count)
            {
                Assert.Fail("Количество записей, полученных из базы не совпадает с ожидаемым количеством.");
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (!users[i].LikeAs(addedUsers[i]))
                {
                    Assert.Fail("Запись из базы {0} не подобна\n ожидаемой {1}", users[i], addedUsers[i]);
                }
            }
        }

        /// <summary>
        /// Тест метода UserRepository.GetEntity. В метод передается существующий ID.
        /// </summary>
        [TestMethod]
        public void ReposGetEntityWithExpectedIDTest()
        {
            UserRepository userRepos = new UserRepository(  connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository( connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            User user = userRepos.GetEntity<User>(18);
            User expectedUser = new User(18, "u1", "u1", "u1", new Role(1, "r1", new List<Permission>()), false, new DateTime(2018, 08, 22), "u1@ya.ru");
            Assert.IsTrue(user.LikeAs(expectedUser));
        }

        /// <summary>
        /// Тест метода UserRepository.GetEntity. В метод передается несуществующий ID.
        /// </summary>
        [TestMethod]
        public void ReposGetEntityWithNotExistedTest()
        {
            UserRepository userRepos = new UserRepository(connString,
                                                            DbProviderFactories.GetFactory(factoryString),
                                                            new RoleRepository(connString,
                                                                                DbProviderFactories.GetFactory(factoryString)));
            User user = userRepos.GetEntity<User>(1000);
            Assert.IsNull(user);
        }
    }
}

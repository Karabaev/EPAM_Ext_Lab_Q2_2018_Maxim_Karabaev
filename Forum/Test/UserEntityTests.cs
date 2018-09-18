namespace Test
{
    using System;
    using DAL.Core;
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Configuration;

    /// <summary>
    /// Тесты сущности, репозитория, сервиса User.
    /// </summary>
    [TestClass]
    public class UserEntityTests
    {
        private const string ConectionSringName = "ForumSqlServerConnection";
        private const int ExistID = 18;
        private const int NotExistID = 1000;
        private const int DisplayUsersCount = 1;

        private UserRepository GetUserRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[ConectionSringName];
            var connString = connectionStringItem.ConnectionString;
            var factory = DbProviderFactories.GetFactory(connectionStringItem.ProviderName);
            return new UserRepository(connString, factory, new RoleRepository(connString, factory));
        }
        
        private RoleRepository GetRoleRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[ConectionSringName];
            var connString = connectionStringItem.ConnectionString;
            var factory = DbProviderFactories.GetFactory(connectionStringItem.ProviderName);
            return new RoleRepository(connString, factory);
        }
        /// <summary>
        /// Тест метода User.Equals. При сравнении используются разные инстансы ролей и списков разрешений.
        /// </summary>
        [TestMethod]
        public void UserEqualsDifferentRoleInstancesTest()
        {
            User user1 = new User(1, "u1", "u1", "u1", new Role(1, "r1", 1), false, DateTime.Now.Date, "u1@ya.ru");
            User user2 = new User(1, "u1", "u1", "u1", new Role(1, "r1", 1), false, DateTime.Now.Date, "u1@ya.ru");
            Assert.IsTrue(user1.Equals(user2));
        }

        /// <summary>
        /// Тест метода User.Equals. При сравнении используется один инстанс роли и списка разрешений.
        /// </summary>
        [TestMethod]
        public void UserEqualsSingleRoleInstanceTest()
        {
            Role role = new Role(1, "r1", 1);
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
            UserRepository userRepos = this.GetUserRepository();
            Role testRole = new Role(1, "r1", 1);
            User testUser = new User(4, "u1", "u1", "u1", testRole, false, DateTime.Now, "u1@ya.ru");
            Assert.IsTrue(userRepos.SaveEntity(testUser));

        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления несуществующей строки.
        /// </summary>
        [TestMethod]
        public void ReposRemoveNotExistEntity()
        {
            UserRepository userRepos = this.GetUserRepository();
            Assert.IsTrue(userRepos.RemoveEntity(NotExistID) == 0);
        }

        /// <summary>
        /// Тест метода UserRepository.Remove. Попытка удаления существующей строки. Тест одноразовый.
        /// </summary>
        [TestMethod]
        public void ReposRemoveOneExistEntity()
        {
            UserRepository userRepos = this.GetUserRepository();
            User newUser = new User(1, "u", "u", "u", new Role(1, "r", 1), false, DateTime.Now, "u");

            if(!userRepos.SaveEntity(newUser))
            {
                Assert.Fail("Не удалось добавить запись в базу.");
            }

            List<User> users = userRepos.GetAllEntities();
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
            UserRepository userRepos = this.GetUserRepository();
            RoleRepository roleRepos = this.GetRoleRepository();
            Role addedRole = new Role(1, "r1", 1);
            roleRepos.RemoveAllEntities();
            roleRepos.SaveEntity(addedRole);
            List<Role> roles = roleRepos.GetAllEntities();
            List<User> expectedUsers = new List<User>();   
            FormattedDate date = new DateTime(2018, 08, 22);
            expectedUsers.Add(new User(18, "u1", "u1", "u1", roles?[0], false, date, "u1@ya.ru"));
            expectedUsers.Add(new User(18, "u2", "u2", "u2", roles?[0], false, date, "u2@ya.ru"));
            userRepos.RemoveAllEntities();

            foreach (var item in expectedUsers)
            {
                userRepos.SaveEntity(item);
            }

            List<User> users = userRepos.GetAllEntities();

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
            UserRepository userRepos = this.GetUserRepository();
            List<User> addedUsers = new List<User>();
            Role role = new Role(1, "r1", 1);
            FormattedDate date = new DateTime(2018, 08, 22);
            addedUsers.Add(new User(18, "u1", "u1", "u1", role, false, date, "u1@ya.ru"));
            addedUsers.Add(new User(18, "u2", "u2", "u2", role, false, date, "u2@ya.ru"));
            userRepos.RemoveAllEntities();

            foreach (var item in addedUsers)
            {
                userRepos.SaveEntity(item);
            }

            List<User> users = userRepos.GetAllEntities(DisplayUsersCount);

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
            UserRepository userRepos = this.GetUserRepository();
            List<User> users = userRepos.GetAllEntities();
            
            if (users == null || !users.Any() || users[0] == null)
            {
                Assert.Fail("Не удалось загрузить пользователей из базы.");
            }
            User expectedUser = users[0];
            User user = userRepos.GetEntity(expectedUser.ID);
            Assert.IsTrue(user.Equals(expectedUser));
        }

        /// <summary>
        /// Тест метода UserRepository.GetEntity. В метод передается несуществующий ID.
        /// </summary>
        [TestMethod]
        public void ReposGetEntityWithNotExistedTest()
        {
            UserRepository userRepos = this.GetUserRepository();
            User user = userRepos.GetEntity(NotExistID);
            Assert.IsNull(user);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReposTest
{
    [TestClass]
    public class UserEntityAndRepositoryTest
    {
        private const string EntriesNotMutchErrorText = "Collection entries don't match.";
        private const uint ExistingID = 1;
        private const uint NotExistindID = 1000;
        /// <summary>
        /// Верка количества записей в исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesCount()
        {
            UserRepository repository = new UserRepository();
            var users = repository.GetAllEntities<User>();
            Assert.AreEqual(repository.Users.Count, users.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            UserRepository repository = new UserRepository();
            var users = repository.GetAllEntities<User>();

            bool successFlag = false;
            foreach (var obtainedItem in users)
            {
                successFlag = false;
                foreach (var originalItem in repository.Users)
                {
                    if (obtainedItem.Equals(originalItem))
                    {
                        successFlag = true;
                        break;
                    }
                }
                if(!successFlag)
                    Assert.Fail(EntriesNotMutchErrorText);
            }
        }
        /// <summary>
        /// Сравнение элемента коллекции, полученного с помощью метода GetEntity с исходным элементом.
        /// </summary>
        [TestMethod]
        public void GetEntityEqualsToOriginal()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.Users;
            User resultUser = repository.GetEntity<User>(ExistingID);
            Assert.AreEqual(repository.Users.Where(u => u.ID == ExistingID).FirstOrDefault(), resultUser);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            UserRepository repository = new UserRepository();
            User resultUser = repository.GetEntity<User>(NotExistindID);
            Assert.IsNull(resultUser);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            UserRepository repository = new UserRepository();
            List<User> startList = new List<User>(repository.Users);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            User deletedUser = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedUser == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.Users.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedUser) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Users)
                {
                    if (startItem.Equals(resultItem))
                    {
                        successFlag = true; // Успех. Соответствующая запись найдена
                        break;
                    }
                }
                // Если для одной из записей исходной коллекции не найдена соответствующая запись в результирующей коллекции
                // за ислючением удаленной записи.
                if (!successFlag) 
                    Assert.Fail(EntriesNotMutchErrorText);
            }
        }
        /// <summary>
        /// Удаление Пользователя с несуществующимв базе ID
        /// </summary>
        [TestMethod]
        public void RemoveUserWithNotExistingID()
        {
            UserRepository repository = new UserRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            UserRepository repository = new UserRepository();
            Assert.IsFalse(repository.SaveEntity<User>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            UserRepository repository = new UserRepository();
            User user = repository.Users[0];
            User newUser = new User();

            newUser.ID = user.ID;
            newUser.Reinitialization(user);
            Assert.IsFalse(repository.SaveEntity(newUser));
        }
        /// <summary>
        /// Проверка метода Equals класса User
        /// </summary>
        [TestMethod]
        public void UserNotEquals()
        {
            User u1 = new User
            {
                ID = 50,
                IsBanned = false,
                Login = "u1",
                PasswordHash = "u1",
                PublicName = "u1",
                UserRole = new Role()
            };
            User u2 = new User
            {
                ID = 51,
                IsBanned = false,
                Login = "u2",
                PasswordHash = "u2",
                PublicName = "u2",
                UserRole = new Role()
            };
            Assert.IsFalse(u1.Equals(u2));
        }
        
        /// <summary>
        /// List.Contains ищет по ссылке или по Equals ? По перегрузке Equals.
        /// </summary>
        [TestMethod]
        public void ListContains()
        {
            List<Permission> perms = new List<Permission>();
            User u1 = new User
            {
                ID = 50,
                IsBanned = false,
                Login = "u1",
                PasswordHash = "u1",
                PublicName = "u1",
                UserRole = new Role { ID = 0, Name = "ur1", Permissions = perms }
            };
            List<User> users = new List<User>();
            users.Add(u1);
            User u2 = new User
            {
                ID = 50,
                IsBanned = false,
                Login = "u1",
                PasswordHash = "u1",
                PublicName = "u1",
                UserRole = new Role { ID = 0, Name = "ur1", Permissions = perms }
            };

            Assert.IsTrue(users.Contains(u2));
        }
    }
}

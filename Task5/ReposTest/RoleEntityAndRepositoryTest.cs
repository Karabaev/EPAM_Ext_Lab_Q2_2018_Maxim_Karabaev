using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReposTest
{
    [TestClass]
    public class RoleEntityAndRepositoryTest
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
            RoleRepository repository = new RoleRepository();
            var roles = repository.GetAllEntities<Role>();
            Assert.AreEqual(repository.Roles.Count, roles.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            RoleRepository repository = new RoleRepository();
            var roles = repository.GetAllEntities<Role>();

            bool successFlag = false;
            foreach (var obtainedItem in roles)
            {
                successFlag = false;
                foreach (var originalItem in repository.Roles)
                {
                    if (obtainedItem.Equals(originalItem))
                    {
                        successFlag = true;
                        break;
                    }
                }
                if (!successFlag)
                    Assert.Fail(EntriesNotMutchErrorText);
            }
        }
        /// <summary>
        /// Сравнение элемента коллекции, полученного с помощью метода GetEntity с исходным элементом.
        /// </summary>
        [TestMethod]
        public void GetEntityEqualsToOriginal()
        {
            RoleRepository repository = new RoleRepository();
            List<Role> roles = repository.Roles;
            Role resultRole = repository.GetEntity<Role>(ExistingID);
            Assert.AreEqual(repository.Roles.Where(u => u.ID == ExistingID).FirstOrDefault(), resultRole);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            RoleRepository repository = new RoleRepository();
            Role resultRole = repository.GetEntity<Role>(NotExistindID);
            Assert.IsNull(resultRole);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            RoleRepository repository = new RoleRepository();
            List<Role> startList = new List<Role>(repository.Roles);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            Role deletedRole = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedRole == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.Roles.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedRole) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Roles)
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
            RoleRepository repository = new RoleRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            RoleRepository repository = new RoleRepository();
            Assert.IsFalse(repository.SaveEntity<Role>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            RoleRepository repository = new RoleRepository();
            Role role = repository.Roles[0];
            Role newRole = new Role();

            newRole.ID = role.ID;
            newRole.Reinitialization(role);
            Assert.IsFalse(repository.SaveEntity(newRole));
        }
    }
}

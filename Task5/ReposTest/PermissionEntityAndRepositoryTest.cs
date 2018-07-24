using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReposTest
{
    [TestClass]
    public class PermissionEntityAndRepositoryTest
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
            PermissionRepository repository = new PermissionRepository();
            var perms = repository.GetAllEntities<Permission>();
            Assert.AreEqual(repository.Permissions.Count, perms.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            PermissionRepository repository = new PermissionRepository();
            var perms = repository.GetAllEntities<Permission>();

            bool successFlag = false;
            foreach (var obtainedItem in perms)
            {
                successFlag = false;
                foreach (var originalItem in repository.Permissions)
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
            PermissionRepository repository = new PermissionRepository();
            List<Permission> perms = repository.Permissions;
            Permission resultPerm = repository.GetEntity<Permission>(ExistingID);
            Assert.AreEqual(repository.Permissions.Where(u => u.ID == ExistingID).FirstOrDefault(), resultPerm);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            PermissionRepository repository = new PermissionRepository();
            Permission resultPerm = repository.GetEntity<Permission>(NotExistindID);
            Assert.IsNull(resultPerm);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            PermissionRepository repository = new PermissionRepository();
            List<Permission> startList = new List<Permission>(repository.Permissions);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            Permission deletedPermission = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedPermission == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.Permissions.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedPermission) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Permissions)
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
            PermissionRepository repository = new PermissionRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            PermissionRepository repository = new PermissionRepository();
            Assert.IsFalse(repository.SaveEntity<Role>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            PermissionRepository repository = new PermissionRepository();
            Permission perm = repository.Permissions[0];
            Permission newPerm = new Permission();

            newPerm.ID = perm.ID;
            newPerm.Reinitialization(perm);
            Assert.IsFalse(repository.SaveEntity(newPerm));
        }
    }
}

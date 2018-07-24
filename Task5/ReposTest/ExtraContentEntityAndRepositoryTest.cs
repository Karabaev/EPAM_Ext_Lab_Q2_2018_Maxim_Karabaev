using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ReposTest
{
    [TestClass]
    public class ExtraContentEntityAndRepositoryTest
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
            ExtraContentRepository repository = new ExtraContentRepository();
            var extras = repository.GetAllEntities<ExtraContent>();
            Assert.AreEqual(repository.ExtraContents.Count, extras.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            ExtraContentRepository repository = new ExtraContentRepository();
            var extras = repository.GetAllEntities<ExtraContent>();

            bool successFlag = false;
            foreach (var obtainedItem in extras)
            {
                successFlag = false;
                foreach (var originalItem in repository.ExtraContents)
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
            ExtraContentRepository repository = new ExtraContentRepository();
            List<ExtraContent> extras = repository.ExtraContents;
            ExtraContent resultExtra = repository.GetEntity<ExtraContent>(ExistingID);
            Assert.AreEqual(repository.ExtraContents.Where(u => u.ID == ExistingID).FirstOrDefault(), resultExtra);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            ExtraContentRepository repository = new ExtraContentRepository();
            ExtraContent resultExtra = repository.GetEntity<ExtraContent>(NotExistindID);
            Assert.IsNull(resultExtra);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            ExtraContentRepository repository = new ExtraContentRepository();
            List<ExtraContent> startList = new List<ExtraContent>(repository.ExtraContents);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            ExtraContent deletedExtra = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedExtra == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.ExtraContents.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedExtra) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.ExtraContents)
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
            ExtraContentRepository repository = new ExtraContentRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            ExtraContentRepository repository = new ExtraContentRepository();
            Assert.IsFalse(repository.SaveEntity<ExtraContent>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            ExtraContentRepository repository = new ExtraContentRepository();
            ExtraContent extra = repository.ExtraContents[0];
            ExtraContent newExtra = new ExtraContent();

            newExtra.ID = extra.ID;
            newExtra.Reinitialization(extra);
            Assert.IsFalse(repository.SaveEntity(newExtra));
        }
    }
}

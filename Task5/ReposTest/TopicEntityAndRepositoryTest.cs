using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ReposTest
{
    [TestClass]
    public class TopicEntityAndRepositoryTest
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
            TopicRepository repository = new TopicRepository();
            var topics = repository.GetAllEntities<Topic>();
            Assert.AreEqual(repository.Topics.Count, topics.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            TopicRepository repository = new TopicRepository();
            var topics = repository.GetAllEntities<Topic>();

            bool successFlag = false;
            foreach (var obtainedItem in topics)
            {
                successFlag = false;
                foreach (var originalItem in repository.Topics)
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
            TopicRepository repository = new TopicRepository();
            List<Topic> topics = repository.Topics;
            Topic resultRole = repository.GetEntity<Topic>(ExistingID);
            Assert.AreEqual(repository.Topics.Where(u => u.ID == ExistingID).FirstOrDefault(), resultRole);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            TopicRepository repository = new TopicRepository();
            Topic resultTopic = repository.GetEntity<Topic>(NotExistindID);
            Assert.IsNull(resultTopic);
        }
        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            TopicRepository repository = new TopicRepository();
            List<Topic> startList = new List<Topic>(repository.Topics);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            Topic deletedTopic = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedTopic == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.Topics.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedTopic) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Topics)
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
            TopicRepository repository = new TopicRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            TopicRepository repository = new TopicRepository();
            Assert.IsFalse(repository.SaveEntity<Topic>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            TopicRepository repository = new TopicRepository();
            Topic topic = repository.Topics[0];
            Topic newTopic = new Topic();

            newTopic.ID = topic.ID;
            newTopic.Reinitialization(topic);
            Assert.IsFalse(repository.SaveEntity(newTopic));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReposTest
{
    [TestClass]
    public class MessageEntityAndRepositoryTest
    {
        private const string EntriesNotMutchErrorText = "Collection entries don't match.";
        private const uint ExistingID = 1;
        private const uint NotExistindID = 1000;
        /// <summary>
        /// Сверка количества записей в исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesCount()
        {
            MessageRepository repository = new MessageRepository();
            var messages = repository.GetAllEntities<Message>();
            Assert.AreEqual(repository.Messages.Count, messages.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            MessageRepository repository = new MessageRepository();
            var messages = repository.GetAllEntities<Message>();

            bool successFlag = false;
            foreach (var obtainedItem in messages)
            {
                successFlag = false;
                foreach (var originalItem in repository.Messages)
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
            MessageRepository repository = new MessageRepository();
            List<Message> messages = repository.Messages;
            Message resultMessage = repository.GetEntity<Message>(ExistingID);
            Assert.AreEqual(repository.Messages.Where(u => u.ID == ExistingID).FirstOrDefault(), resultMessage);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            MessageRepository repository = new MessageRepository();
            Message resultMessage = repository.GetEntity<Message>(NotExistindID);
            Assert.IsNull(resultMessage);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            MessageRepository repository = new MessageRepository();
            List<Message> startList = new List<Message>(repository.Messages);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            Message deletedMessage = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedMessage == null) Assert.Fail("Error. Deleted entry not found.");


            if (startList.Count != repository.Messages.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedMessage) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Messages)
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
            MessageRepository repository = new MessageRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            MessageRepository repository = new MessageRepository();
            Assert.IsFalse(repository.SaveEntity<Message>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            MessageRepository repository = new MessageRepository();
            Message message = repository.Messages[0];
            Message newMessage = new Message();

            newMessage.ID = message.ID;
            newMessage.Reinitialization(message);
            Assert.IsFalse(repository.SaveEntity(newMessage));
        }
    }
}

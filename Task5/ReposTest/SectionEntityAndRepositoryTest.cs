using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ReposTest
{
    [TestClass]
    public class SectionEntityAndRepositoryTest
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
            SectionRepository repository = new SectionRepository();
            var sections = repository.GetAllEntities<Section>();
            Assert.AreEqual(repository.Sections.Count, sections.Count);
        }
        /// <summary>
        /// Сравнение элементов исходной коллекции и полученной методом GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesEntries()
        {
            SectionRepository repository = new SectionRepository();
            var sections = repository.GetAllEntities<Section>();

            bool successFlag = false;
            foreach (var obtainedItem in sections)
            {
                successFlag = false;
                foreach (var originalItem in repository.Sections)
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
            SectionRepository repository = new SectionRepository();
            List<Section> sections = repository.Sections;
            Section resultSection = repository.GetEntity<Section>(ExistingID);
            Assert.AreEqual(repository.Sections.Where(u => u.ID == ExistingID).FirstOrDefault(), resultSection);
        }
        /// <summary>
        /// Проверка результата метода GetEntity на null, в случае передачи ID, которого нет в базе
        /// </summary>
        [TestMethod]
        public void GetEntityWithNotExistingID()
        {
            SectionRepository repository = new SectionRepository();
            Section resultSection = repository.GetEntity<Section>(NotExistindID);
            Assert.IsNull(resultSection);
        }

        /// <summary>
        /// Проверка результата удаления Пользователя из базы.
        /// </summary>
        [TestMethod]
        public void RemoveUserWithExistingID()
        {
            SectionRepository repository = new SectionRepository();
            List<Section> startList = new List<Section>(repository.Sections);

            if (!repository.Remove(ExistingID))
                Assert.Fail("Error deleting entry.");
            Section deletedSection = startList.Where(u => u.ID == ExistingID).FirstOrDefault();
            if (deletedSection == null) Assert.Fail("Error. Deleted entry not found.");

            if (startList.Count != repository.Sections.Count + 1)
                Assert.Fail("After deleting the entry, the collection size did not decrease by 1.");
            bool successFlag = false;
            foreach (var startItem in startList)
            {
                successFlag = false;
                if (startItem == deletedSection) continue;  // Удаленную запись после удаления уже не сравнивать.
                foreach (var resultItem in repository.Sections)
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
            SectionRepository repository = new SectionRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }

        /// <summary>
        /// Проверка метода Save при передачи null сущности для сохранения
        /// </summary>
        [TestMethod]
        public void SaveNullEntity()
        {
            SectionRepository repository = new SectionRepository();
            Assert.IsFalse(repository.SaveEntity<Section>(null));
        }
        /// <summary>
        /// Проверка Save при передачи записи, которая уже есть в базе
        /// </summary>
        [TestMethod]
        public void SaveContainsEntity()
        {
            SectionRepository repository = new SectionRepository();
            Section section = repository.Sections[0];
            Section newSection = new Section();

            newSection.ID = section.ID;
            newSection.Reinitialization(section);
            Assert.IsFalse(repository.SaveEntity(newSection));
        }
    }
}

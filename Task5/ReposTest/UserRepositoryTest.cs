using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ReposTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        private const string EntriesNotMutchErrorText = "Collection entries don't match.";
        private const uint ExistingID = 1;
        private const uint NotExistindID = 1000;
        /// <summary>
        /// ����� ���������� ������� � �������� ��������� � ���������� ������� GetAllEntities
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesCount()
        {
            UserRepository repository = new UserRepository();
            var users = repository.GetAllEntities<User>();
            Assert.AreEqual(repository.Users.Count, users.Count);
        }
        /// <summary>
        /// ��������� ��������� �������� ��������� � ���������� ������� GetAllEntities
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

        [TestMethod]
        public void GetEntityEqualsToOriginal()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.Users;

            User resultUser = repository.GetEntity<User>(ExistingID);

        }





        /// <summary>
        /// �������� ���������� �������� ������������ �� ����.
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
                if (startItem == deletedUser) continue;  // ��������� ������ ����� �������� ��� �� ����������.
                foreach (var resultItem in repository.Users)
                {
                    if (startItem.Equals(resultItem))
                    {
                        successFlag = true; // �����. ��������������� ������ �������
                        break;
                    }
                }
                // ���� ��� ����� �� ������� �������� ��������� �� ������� ��������������� ������ � �������������� ���������
                // �� ���������� ��������� ������.
                if (!successFlag) 
                    Assert.Fail(EntriesNotMutchErrorText);
            }
        }
        /// <summary>
        /// �������� ������������ � ��������������� ���� ID
        /// </summary>
        [TestMethod]
        public void RemoveUserWithNotExistingID()
        {
            UserRepository repository = new UserRepository();
            Assert.IsFalse(repository.Remove(NotExistindID));
        }
    }
}

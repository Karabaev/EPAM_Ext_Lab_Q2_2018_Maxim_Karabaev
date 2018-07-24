using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task5.Repository;
using Task5;
using System;
using System.Collections.Generic;
using static System.Console;
namespace RepositoryTest
{
    [TestClass]
    public class MessageRepositoryTest
    {
        [TestMethod]
        public void GetAllEntities()
        {
            MessageRepository repos = new MessageRepository();
            List<Message> messages = repos.GetAllEntities<Message>();
            foreach (var item in messages)
                WriteLine(item.ToString());
        }
        [TestMethod]
        public void GetEntityWithNonExistentID()
        {
            MessageRepository repos = new MessageRepository();
            Message message = repos.GetEntity<Message>(2132);
            try
            {
                WriteLine(message.ToString());
            }
            catch (NullReferenceException ex)
            {
                WriteLine(ex.Message);
            }


        }
    }
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public bool RemoveUserWithExistingID()
        {
            UserRepository repository = new UserRepository();
            List<User> startList = new List<User>(repository.Users);

            repository.Remove(1);

            if (startList.Count != repository.Users.Count + 1)
                return false;
            foreach (var startItem in startList)
            {
                foreach (var resultItem in repository.Users)
                {
                    if (startItem.Equals(resultItem)) // если поля объектов идентичны
                    {
                        if (startItem.GetHashCode() != resultItem.GetHashCode())
                            return false;
                    }
                }
            }
            return true;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task5;
using Task5.Repository;
namespace RepositoryTest
{
    //[TestClass]
    //public class UserRepositoryTest
    //{
    //    [TestMethod]
    //    public bool RemoveUserWithExistingID()
    //    {
    //        UserRepository repository = new UserRepository();
    //        List<User> startList = new List<User>(repository.Users);

    //        repository.Remove(1);

    //        if (startList.Count != repository.Users.Count + 1)
    //            return false;
    //        foreach (var startItem in startList)
    //        {
    //            foreach (var resultItem in repository.Users)
    //            {
    //                if(startItem.Equals(resultItem)) // если поля объектов идентичны
    //                {
    //                    if (startItem.GetHashCode() != resultItem.GetHashCode())
    //                        return false;
    //                }
    //            }
    //        }
    //        return true;
    //    }
    //}
}

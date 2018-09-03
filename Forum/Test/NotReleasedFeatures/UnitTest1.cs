

namespace Test.NotReleasedFeatures
{
    using System.Configuration;
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data;
    using System.Data.Common;
    using DAL.Core.DatabaseToModelMapper.Entities;
    using DAL.Core.DatabaseToModelMapper.Repository;

    [TestClass]
    public class UserTest
    {
        private const string ConectionSringName = "ForumSqlServerConnection";
        private readonly string ConnString;
        private readonly DbProviderFactory Factory;

        public UserTest()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[ConectionSringName];
            ConnString = connectionStringItem.ConnectionString;
            Factory = DbProviderFactories.GetFactory(connectionStringItem.ProviderName);
        }

        [TestMethod]
        public void TestMethod1()
        {
            UserRepository userRepos = new UserRepository(ConnString, Factory);
            //Console.WriteLine(User.Mapper.ToString());
            User user = userRepos.GetUser(6);
            if (user != null)
                Console.WriteLine(user.ID.ToString() + " " + user.Name);
            else
                Console.WriteLine("user = null");
        }
    }
}

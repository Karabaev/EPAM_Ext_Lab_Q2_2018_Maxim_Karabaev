namespace Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DAL.Core;
    using DAL.Model.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Тесты хелпера QueryBuilder.
    /// </summary>
    [TestClass]
    public class QueryBuilderTest
    {
        /// <summary>
        /// Тест метода QueryBuilder.GetDeleteRecordCommand.
        /// </summary>
        [TestMethod]
        public void GetDeleteRecordCommandTest()
        {
            string result = QueryBuilder.GetDeleteRecordCommand("Users", "ID = {0} AND Name = {1}", 0.ToString(), "Steve");
            Console.WriteLine(result);
            result = QueryBuilder.GetDeleteRecordCommand("Roles", "ID = {0} AND Name = {1}", 15.ToString(), "Admin");
            Console.WriteLine(result);
        }

        /// <summary>
        /// Тест метода QueryBuilder.GetAddRecordCommand.
        /// </summary>
        [TestMethod]
        public void GetAddRecordCommandTest()
        {
            string result = QueryBuilder.GetAddRecordCommand("Users", "Name, RoleID", "Steve, 1");
            Console.WriteLine(result);
            result = QueryBuilder.GetAddRecordCommand("Roles", "Name, Description", "Admin, Top users");
            Console.WriteLine(result);
        }

        /// <summary>
        /// Тест метода QueryBuilder.GetSelectRecordCommand.
        /// </summary>
        [TestMethod]
        public void GetSelectRecordCommandTest()
        {
            string result = QueryBuilder.GetSelectRecordCommand("Users", "Name, RoleID", "ID = {0}", 1.ToString());
            Console.WriteLine(result);
            result = QueryBuilder.GetSelectRecordCommand("Roles", "*", "ID = {0}", 1.ToString());
            Console.WriteLine(result);
        }

        /// <summary>
        /// Тест метода QueryBuilder.GetEntityProperties.
        /// </summary>
        [TestMethod]
        public void GetEntityPropertiesTest()
        {
            string result = QueryBuilder.GetEntityProperties(typeof(User));
            Console.WriteLine(result);
            result = QueryBuilder.GetEntityProperties(typeof(string));
            Console.WriteLine(result);
        }

        /// <summary>
        /// Тест метода QueryBuilder.GetEntityPropertiesAndValues.
        /// </summary>
        [TestMethod]
        public void GetEntityPropertiesAndValuesTest()
        {
            string result1 = "";
            string result2 = "";
            Role testRole = new Role(0, "r1", new List<Permission>());
            User testUser = new User(0, "u1", "u1", "u1", testRole, false, DateTime.Now);
            QueryBuilder.GetEntityPropertiesAndValues(testUser, out result1, out result2);
            Console.WriteLine(result1 + "\n" + result2);
            QueryBuilder.GetEntityPropertiesAndValues(testRole, out result1, out result2);
            Console.WriteLine(result1 + "\n" + result2);
        }
    }
}

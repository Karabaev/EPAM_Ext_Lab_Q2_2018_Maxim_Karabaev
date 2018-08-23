namespace Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DAL.Core;
    using DAL.Model.Entities;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Configuration;

    /// <summary>
    /// Тесты хелпера QueryBuilder. В качестве тестов в основном используется вывод возвращаемых комманд на экран.
    /// </summary>
    [TestClass]
    public class QueryBuilderTest
    {
        private readonly string ConnString;
        private readonly DbProviderFactory Factory;
        private const string ConectionSringName = "ForumSqlServerConnection";

        public QueryBuilderTest()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[ConectionSringName];
            ConnString = connectionStringItem.ConnectionString;
            Factory = DbProviderFactories.GetFactory(connectionStringItem.ProviderName);
        }

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
        /// Тест метода QueryBuilder.GetDeleteRecordCommand.
        /// </summary>
        [TestMethod]
        public void GetDeleteAllRecordsCommandTest()
        {
            string result = QueryBuilder.GetDeleteAllRecordsCommand("Users");
            Console.WriteLine(result);
            result = QueryBuilder.GetDeleteAllRecordsCommand("Roles");
            Console.WriteLine(result);
        }

        /// <summary>
        /// Тест метода QueryBuilder.GetAddRecordCommand.
        /// </summary>
        [TestMethod]
        public void GetAddRecordCommandTest()
        {
            string result = QueryBuilder.GetAddRecordCommand("Users", "Name, RoleID", "'Steve', 1");
            Console.WriteLine(result);
            result = QueryBuilder.GetAddRecordCommand("Roles", "Name, Description", "'Admin', 'Top user'");
            Console.WriteLine(result);
            result = QueryBuilder.GetAddRecordCommand("Roles", "SELECT * FROM Users --", "'Admin', 'Top user'");
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
            result = QueryBuilder.GetSelectRecordCommand("Roles", "*", "");
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
            Role testRole = new Role(3, "r1", new List<Permission>());
            User testUser = new User(0, "u1", "u1", "u1", testRole, false, new FormattedDate(DateTime.Now), "u1@ya.ru");
            QueryBuilder.GetEntityPropertiesAndValues(testUser, out result1, out result2);
            Console.WriteLine(result1 + "\n" + result2);
            QueryBuilder.GetEntityPropertiesAndValues(testRole, out result1, out result2);
            Console.WriteLine(result1 + "\n" + result2);
            QueryBuilder.GetEntityPropertiesAndValues(testRole, out result1, out result2, true);
            Console.WriteLine(result1 + "\n" + result2);
        }

        /// <summary>
        /// Тест QueryBuilder.GetStoredProcedureDataReader с несуществующей процедурой.
        /// </summary>
        [TestMethod]
        public void GetNotExistStoredProcedureDataReaderTest()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnString;
            connection.Open();
            DbParameter parameter = Factory.CreateParameter();
            parameter.ParameterName = "";
            parameter.Value = 15;
            Assert.IsNull(QueryBuilder.GetStoredProcedureDataReader(connection, "11", parameter));
        }

        /// <summary>
        /// Тест QueryBuilder.GetStoredProcedureDataReader с передачей неверного количества параметров.
        /// </summary>
        [TestMethod]
        public void GetStoredProcedureInvalidParamsDataReaderTest()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = ConnString;
            connection.Open();
            DbParameter parameter1 = Factory.CreateParameter();
            parameter1.ParameterName = "@SomeVal";
            parameter1.Value = 143;
            DbParameter parameter2 = Factory.CreateParameter();
            parameter2.ParameterName = "@SomeVal2";
            parameter2.Value = "Hello world!";
            Assert.IsNull(QueryBuilder.GetStoredProcedureDataReader(connection, "GetAllUsers", parameter1, parameter2));
        }
    }
}

namespace Test
{
    using System;
    using DAL.Core;
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Configuration;

    [TestClass]
    public class RoleEntityTests
    {
        private const string ConectionSringName = "ForumSqlServerConnection";

        private RoleRepository GetRoleRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[RoleEntityTests.ConectionSringName];
            var connString = connectionStringItem.ConnectionString;
            var factory = DbProviderFactories.GetFactory(connectionStringItem.ProviderName);
            return new RoleRepository(connString, factory);
        }

        /// <summary>
        /// Тест метода RoleRepository.SaveEntity, когда ему передается корректный объект.
        /// </summary>
        [TestMethod]
        public void SaveEntityTest()
        {
            RoleRepository repos = this.GetRoleRepository();
            Role newRole = new Role(0, "Admin", 5);
            Assert.IsTrue(repos.SaveEntity(newRole));
        }

        /// <summary>
        /// Тест метода RoleRepository.GetEntity, когда ему передается существующий ID.
        /// </summary>
        [TestMethod]
        public void GetEntityTest()
        {
            RoleRepository repos = this.GetRoleRepository();
            Role expected = new Role(0, "Admin", 5);
            repos.SaveEntity(expected);
            repos.GetEntity(3);

            //Assert.IsTrue(repos.SaveEntity(newRole));
        }

        /// <summary>
        /// Тест метода RoleRepository.GetAllEntities.
        /// </summary>
        [TestMethod]
        public void GetAllEntitiesTest()
        {
            RoleRepository repos = this.GetRoleRepository();
            repos.RemoveAllEntities();
            List<Role> expectedRoles = new List<Role>();
            expectedRoles.Add(new Role(0, "Admin", 5));
            expectedRoles.Add(new Role(1, "User", 1));
            expectedRoles.Add(new Role(2, "SA", 8));

            foreach (var item in expectedRoles)
            {
                repos.SaveEntity(item);
            }

            List<Role> resultRoles = repos.GetAllEntities();

            if(expectedRoles.Count != resultRoles.Count)
            {
                Assert.Fail("Размеры листов не совпадают.");
            }
            int index = 0;
            foreach (var item in resultRoles)
            {
                if(!item.LikeAs(expectedRoles[index]))
                {
                    Assert.Fail(string.Format("Роль {0} не подобна роли {1}", item, expectedRoles[index]));
                }
                index++;
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateEntityTest()
        {
            RoleRepository repos = this.GetRoleRepository();
            Role expectedRole = new Role(1, "Admin", 10);
            Role role = new Role(1, "User", 1);
            repos.RemoveAllEntities();
            if (repos.SaveEntity(role))
            {
                Role gettedRole = repos.GetAllEntities().Last();
                role.Reinitialization(expectedRole);
                repos.UpdateEntity(new Role(gettedRole.ID, role.Name, role.AccessLevel));
            }
            Assert.IsTrue(expectedRole.LikeAs(repos.GetAllEntities().Last()));
        }
    }
}

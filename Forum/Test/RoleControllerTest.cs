using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    using System.Web;
    using Forum.Controllers.Admin;
    using Forum.Models;
    using Forum.App_Start;
    using DAL.Model.Repository;
    [TestClass]
    public class RoleControllerTest
    {
        [TestMethod]
        public void EditCanSaveValidChanges()
        {
            RoleController controller = new RoleController(new RoleRepository());
            Mapping mapping = new Mapping();
            controller.Edit(new RoleEditViewModel { ID = 5, AccessLevel = 10, Name = "Admin" });
        }
    }
}

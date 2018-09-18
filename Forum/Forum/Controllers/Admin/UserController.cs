namespace Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using DAL.Model.Entities;
    using DAL.Model.Repository;
    using Models;
    using AutoMapper;

    public class UserController : Controller
    {
        private readonly IUserRepository Repository;

        public UserController(IUserRepository repo)
        {
            this.Repository = repo;
        }

        public ActionResult Index()
        {
            List<UserListViewModel> users = Mapper.Map<List<User>, List<UserListViewModel>>(this.Repository.GetAllEntities());
            return View(users);
        }

        public ActionResult Edit(int? id)
        {
            UserEditViewModel user = Mapper.Map<User, UserEditViewModel>(this.Repository.GetEntity(id));

            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //[HttpPost]
        public ActionResult Delete(int? id)
        {
            this.Repository.RemoveEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            User user = this.Repository.GetEntity(id);
            return View(Mapper.Map<User, UserEditViewModel>(user));
        }
    }
}
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
        private readonly IUserRepository UserRepository;
        private readonly IRoleRepository RoleRepository;

        public UserController(IUserRepository repo, IRoleRepository repo1)
        {
            this.UserRepository = repo;
            this.RoleRepository = repo1;
        }

        public ActionResult Index()
        {
            List<UserListViewModel> users = Mapper.Map<List<User>, List<UserListViewModel>>(this.UserRepository.GetAllEntities());
            return View(users);
        }

        public ActionResult Edit(int? id)
        {
            UserEditViewModel user = Mapper.Map<User, UserEditViewModel>(this.UserRepository.GetEntity(id));

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserRoleList = Mapper.Map<List<Role>, List<RoleDropDownViewModel>>(this.RoleRepository.GetAllEntities().ToList());
            return View(user);
        }

        //[HttpPost]
        public ActionResult Delete(int? id)
        {
            this.UserRepository.RemoveEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            User user = this.UserRepository.GetEntity(id);
            return View(Mapper.Map<User, UserEditViewModel>(user));
        }
    }
}
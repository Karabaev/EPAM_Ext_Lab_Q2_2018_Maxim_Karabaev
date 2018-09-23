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
            User dUSer = this.UserRepository.GetEntity(id);
            UserEditViewModel user = Mapper.Map<User, UserEditViewModel>(dUSer);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserRoleList = this.RoleRepository.GetAllEntities();// Mapper.Map<List<Role>, List<RoleDropDownViewModel>>(this.RoleRepository.GetAllEntities().ToList());
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel userView)
        {
            ViewBag.UserRoleList = this.RoleRepository.GetAllEntities();
            
            if (ModelState.IsValid)
            {
                userView.UserRole = this.RoleRepository.GetEntity(userView.UserRoleID);
                User user = Mapper.Map<UserEditViewModel, User>(userView);
                this.UserRepository.UpdateEntity(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(userView);
            }
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

        public ActionResult Create()
        {
            ViewBag.UserRoleList = this.RoleRepository.GetAllEntities();
            return View(Mapper.Map<User, UserEditViewModel>(new User()));
        }

        [HttpPost]
        public ActionResult Create(UserEditViewModel userView)
        {
            ViewBag.UserRoleList = this.RoleRepository.GetAllEntities();
            if (ModelState.IsValid)
            {
                userView.UserRole = this.RoleRepository.GetEntity(userView.UserRoleID);
                userView.RegistrationDate = DateTime.Now;
                this.UserRepository.SaveEntity(Mapper.Map<UserEditViewModel, User>(userView));
                return RedirectToAction("Index");
            }
            else
            {
                return View(userView);
            }
        }
    }
}
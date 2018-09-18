namespace Forum.Controllers.Admin
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

    public class RoleController : Controller
    {
        private IRoleRepository repository;

        public RoleController(IRoleRepository repo)
        {
            this.repository = repo;
        }

        // GET: Role
        public ActionResult Index()
        {
            List<RoleEditViewModel> roles = Mapper.Map<List<Role>, List<RoleEditViewModel>>(this.repository.GetAllEntities());
            return View(roles);
        }

        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return HttpNotFound();
            }

            RoleEditViewModel role = Mapper.Map<Role, RoleEditViewModel>(this.repository.GetEntity(id));
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(RoleEditViewModel roleView)
        {
            Role role = Mapper.Map<RoleEditViewModel, Role>(roleView);
           
            if (ModelState.IsValid)
            {
                this.repository.UpdateEntity(role);
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }

        public ActionResult Create()
        {
            return View(Mapper.Map<Role, RoleEditViewModel>(new Role()));
        }

        [HttpPost]
        public ActionResult Create(RoleEditViewModel roleView)
        {
            Role role = Mapper.Map<RoleEditViewModel, Role>(roleView);

            if (ModelState.IsValid)
            {
                this.repository.SaveEntity(role);
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            this.repository.RemoveEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            Role role = this.repository.GetEntity(id);
            return View(Mapper.Map<Role, RoleEditViewModel>(role));
        }
    }
}
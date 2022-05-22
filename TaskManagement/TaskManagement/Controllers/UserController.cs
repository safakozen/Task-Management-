using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        UserManager _userManager = new UserManager(new EFUserDAL());
        StatusManager _statusManager = new StatusManager(new EFStatusDAL());

        public ActionResult Index()
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                var uservalues = _userManager.GetUserList().Where(x => x.IsDelete == false).ToList();
                return View(uservalues);
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Create()
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusTypeId == 1), "Id", "StatusName");
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            User LoginUser = Session["LoginUser"] as User;
            ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusTypeId == 1), "Id", "StatusName");
            model.CreatedUserName = LoginUser.Name;
            _userManager.CreateUser(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                var data = _userManager.GetUserById(id);
                if (data != null)
                {
                    ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 1), "Id", "StatusName", data.StatusId);
                    return View(data);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            UserValidator userValidator = new UserValidator();
            ValidationResult results = userValidator.Validate(model);
            var user = _userManager.GetUserById(model.Id);
            if (user != null)
            {
                ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 1), "Id", "StatusName", user.StatusId);
                user.Name = model.Name;
                user.Email = model.Email;
                user.Password = model.Password;
                user.StatusId = model.StatusId;
                _userManager.UpdateUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                var data = _userManager.GetUserById(id);
                if (data != null)
                {
                    ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 1), "Id", "StatusName", data.StatusId);
                    return View(data);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Delete(User model)
        {
            UserValidator userValidator = new UserValidator();
            ValidationResult results = userValidator.Validate(model);
            var user = _userManager.GetUserById(model.Id);
            if (user != null)
            {
                ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 1), "Id", "StatusName", user.StatusId);
                user.IsDelete = true;
                _userManager.UpdateUser(user);
                return RedirectToAction("Index");
            }

            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }



    }
}
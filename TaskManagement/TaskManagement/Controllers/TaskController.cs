using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class TaskController : Controller
    {
        TaskManager _taskManager = new TaskManager(new EFTaskDAL());
        StatusManager _statusManager = new StatusManager(new EFStatusDAL());
        UserManager _userManager = new UserManager(new EFUserDAL());
        UrgencyStatusManager _urgencyStatusManager = new UrgencyStatusManager(new EFUrgencyStatusDAL());
        public ActionResult Index()
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null)
            {
                List<EntityLayer.Concrete.Task> data = null;
                if (LoginUser.StatusId == 1)
                {
                    data = _taskManager.GetAllTask().Where(x => x.IsDelete == false).ToList();
                }
                else
                {
                    data = _taskManager.GetAllTask().Where(x => x.UserId == LoginUser.Id && x.IsDelete == false).ToList();
                }
                return View(data);
            }
            return RedirectToAction("Login", "Login");
        }
        public ActionResult BoardList()
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null)
            {
                TaskStatusVM data = new TaskStatusVM();

                if (LoginUser.StatusId == 1)
                {
                    data.Status = _statusManager.GetStatusList();
                    data.Task = _taskManager.GetAllTask().Where(x => x.IsDelete == false).ToList();
                }
                else
                {
                    data.Status = _statusManager.GetStatusList();
                    data.Task = _taskManager.GetAllTask().Where(x => x.UserId == LoginUser.Id && x.IsDelete == false).ToList();
                }
                return View(data);
            }
            return RedirectToAction("Login", "Login");

        }

        public ActionResult Create()
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 2), "Id", "StatusName");
                ViewBag.UrgencyStatusId = new SelectList(_urgencyStatusManager.GetUrgencyStatusList(), "Id", "Name");
                ViewBag.UserId = new SelectList(_userManager.GetUserList().Where(x => x.StatusId == 3).ToList(), "Id", "Name");
                return View();
            }
            return RedirectToAction("Login", "Login");

        }

        [HttpPost]
        public ActionResult Create(EntityLayer.Concrete.Task model)
        {
            TaskValidator taskValidator = new TaskValidator();
            ValidationResult results = taskValidator.Validate(model);
            if (results.IsValid)
            {
                User LoginUser = Session["LoginUser"] as User;
                model.IsDelete = false;
                model.CreatedUserId = LoginUser.Id;
                _taskManager.CreateTask(model);
                var user = _userManager.GetUserById(model.UserId);
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + user.Name);
                body.AppendLine("E-Mail Adresi: " + user.Email);
                body.AppendLine("Task Adı: " + model.Title);
                body.AppendLine("Task İçerik: " + model.TaskContent);
                body.AppendLine("Taskınız " + LoginUser.Name+ " Tarafından Size Atanmıştır.");
                MailSender.MailSend(user.Email, body.ToString());


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

        public ActionResult Edit(int id)
        {
            User LoginUser = Session["LoginUser"] as User;
            if (LoginUser != null && LoginUser.StatusId == 1)
            {
                var data = _taskManager.GetTaskById(id);
                if (data != null)
                {
                    ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 2), "Id", "StatusName", data.StatusId);
                    ViewBag.UrgencyStatusId = new SelectList(_urgencyStatusManager.GetUrgencyStatusList(), "Id", "Name", data.UrgencyStatusId);
                    ViewBag.UserId = new SelectList(_userManager.GetUserList().Where(x => x.StatusId == 3).ToList(), "Id", "Name", data.UserId);
                    return View(data);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Edit(EntityLayer.Concrete.Task model)
        {
            TaskValidator taskValidator = new TaskValidator();
            ValidationResult results = taskValidator.Validate(model);
            var task = _taskManager.GetTaskById(model.Id);

            if (results.IsValid)
            {
                if (task != null)
                {
                    ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 2), "Id", "StatusName", model.StatusId);
                    ViewBag.UrgencyStatusId = new SelectList(_urgencyStatusManager.GetUrgencyStatusList(), "Id", "Name", model.UrgencyStatusId);
                    ViewBag.UserId = new SelectList(_userManager.GetUserList().Where(x => x.StatusId == 3).ToList(), "Id", "Name", model.UserId);
                    task.Title = model.Title;
                    task.TaskContent = model.TaskContent;
                    task.IsPriority = model.IsPriority;
                    task.EndDate = model.EndDate;
                    task.StatusId = model.StatusId;
                    task.UrgencyStatusId = model.UrgencyStatusId;
                    task.UserId = model.UserId;
                    _taskManager.UpdateTask(task);
                    return RedirectToAction("Index");
                }
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
                var data = _taskManager.GetTaskById(id);
                if (data != null)
                {
                    ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 2), "Id", "StatusName", data.StatusId);
                    ViewBag.UrgencyStatusId = new SelectList(_urgencyStatusManager.GetUrgencyStatusList(), "Id", "Name", data.UrgencyStatusId);
                    ViewBag.UserId = new SelectList(_userManager.GetUserList().Where(x => x.StatusId == 3).ToList(), "Id", "Name", data.UserId);
                    return View(data);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Delete(EntityLayer.Concrete.Task model)
        {
            TaskValidator taskValidator = new TaskValidator();
            ValidationResult results = taskValidator.Validate(model);
            var task = _taskManager.GetTaskById(model.Id);

            if (task != null)
            {
                ViewBag.StatusId = new SelectList(_statusManager.GetStatusList().Where(x => x.StatusType.Id == 2), "Id", "StatusName", task.StatusId);
                ViewBag.UrgencyStatusId = new SelectList(_urgencyStatusManager.GetUrgencyStatusList(), "Id", "Name", task.UrgencyStatusId);
                ViewBag.UserId = new SelectList(_userManager.GetUserList().Where(x => x.StatusId == 3).ToList(), "Id", "Name", task.UserId);
                task.IsDelete = true;
                _taskManager.UpdateTask(task);
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

        public ActionResult ChangeStatus(int id)
        {
            var task = _taskManager.GetTaskById(id);

            if (task != null)
            {
                switch (task.StatusId)
                {
                    case 4:
                        task.StatusId = 5;
                        break;
                    case 5:
                        var user = _userManager.GetUserById(task.UserId);
                        var admin = _userManager.GetUserById(task.CreatedUserId);
                        var body = new StringBuilder();
                        body.AppendLine("Ad & Soyad: " + user.Name);
                        body.AppendLine("E-Mail Adresi: " + user.Email);
                        body.AppendLine("Task Adı: " + task.Title);
                        body.AppendLine("Task İçerik: " + task.TaskContent);
                        body.AppendLine(user.Name + " Kişinin " + task.Title + " Başlıklı Taskı Tamamlandı");
                        MailSender.MailSend(admin.Email, body.ToString());
                        task.StatusId = 6;
                        break;
                    default:
                        break;
                }
                _taskManager.UpdateTask(task);
                return RedirectToAction("BoardList");
            }
            return View();
        }

    }
}
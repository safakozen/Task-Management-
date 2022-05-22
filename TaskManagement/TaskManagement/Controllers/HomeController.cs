using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        TaskManager _taskManager = new TaskManager(new EFTaskDAL());
        UserManager _userManager = new UserManager(new EFUserDAL());
        public ActionResult Index()
        {
            User loginUser = Session["LoginUser"] as User;
            if (loginUser != null)
            {
                ViewBag.TotalPersonel = _userManager.GetUserList().Where(x => x.IsDelete == false).Count();
                ViewBag.TotalTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false).Count();
                ViewBag.TotalContinueTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.StatusId == 5).Count();
                ViewBag.TotalDoneTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.StatusId == 6).Count();
                ViewBag.PersonelTotalTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.UserId==loginUser.Id).Count();
                ViewBag.PersonelTotalWaitingTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.StatusId == 4 && x.UserId == loginUser.Id).Count();
                ViewBag.PersonelTotalContinueTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.StatusId == 5 && x.UserId == loginUser.Id).Count();
                ViewBag.PersonelTotalDoneTask = _taskManager.GetAllTask().Where(x => x.IsDelete == false && x.StatusId == 6 && x.UserId == loginUser.Id).Count();
                return View();
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
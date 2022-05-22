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
        public ActionResult Index()
        {
            if ((Session["LoginUser"] as User) != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
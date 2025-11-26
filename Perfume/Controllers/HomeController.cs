using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perfume.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NewProduct()
        {
            return View();
        }
        public ActionResult Shop()
        {
            return View();
        }
        public ActionResult Collection()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult STTT()
        {
            return View();
        }
        public ActionResult SB()
        {
            return View();
        }
        public ActionResult SCU()
        {
            return View();
        }
        public ActionResult SCA()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
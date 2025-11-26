using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;

namespace Perfume.Controllers
{
    public class LoginRegisterController : Controller
    {
        DBVinamilkEntities db=new DBVinamilkEntities();
        // GET: LoginRegister
        //Tạo form login
        public ActionResult Index()
        {
            return View();
        }
        //Xử lý Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminUser adminUser)
        {
            var checkID = db.AdminUser.Where(s => s.ID.Equals(adminUser.ID)).FirstOrDefault();
            var checkPass = db.AdminUser.Where(s => s.PasswordUser.Equals(adminUser.PasswordUser)).FirstOrDefault();
            var checkName= db.AdminUser.Where(s => s.NameUser.Equals(adminUser.NameUser)).FirstOrDefault();
            if (checkID==null)
            {
                ViewBag.ErrLoginID = "Sai ID";
                return View("Index");
            }
            if(checkPass==null)
            {
                ViewBag.ErrLoginPass = "Sai Mật Khẩu";
                return View("Index");
            }
            if (checkName==null)
            {
                ViewBag.ErrLoginName = "Sai Tên";
                return View("Index");
            }
            if (checkID!=null && checkPass!=null && checkName!=null)
            {
                Session["NameUser"] = adminUser.NameUser;
                Session["RoleUser"] = adminUser.RoleUser;
                if (Session["RoleUser"].ToString() == "Admin")
                    return RedirectToAction("Index", "Product");
                else
                    return RedirectToAction("IndexCustomer", "Product");
            }
            return View("Index","LoginRegister");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","LoginRegister");
        }
        //Tọa form Register\
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AdminUser adminUser)
        {
            var checkID = db.AdminUser.Where(s => s.ID == adminUser.ID).FirstOrDefault();
            if (checkID == null)
            {
                ViewBag.ErrorRegister = "Trùng ID";
                return View("Register");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.AdminUser.Add(adminUser);
                    db.SaveChanges();
                    return RedirectToAction("Index", "LoginRegister");
                }
            }

            return View();
        }
    }
}
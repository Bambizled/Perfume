using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;

namespace Perfume.Controllers
{
    public class CategoryController : Controller
    {
        DBVinamilkEntities db = new DBVinamilkEntities();
        // GET: Category
        public ActionResult danhSachDanhMuc()
        {
            return View(db.Category.ToList());
        }
    }
}
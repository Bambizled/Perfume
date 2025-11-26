    using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;

namespace Perfume.Controllers
{
    public class ProductController : Controller
    {
        DBVinamilkEntities db = new DBVinamilkEntities();
        // GET: Product
        public ActionResult IndexCustomer(decimal? _min, decimal? _max, string _category)
        {
            var proFilter = db.Products.AsQueryable();
            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);
            if (!string.IsNullOrEmpty(_category))
                proFilter = proFilter.Where(s => s.IDCate == _category);

            // Truyền danh sách danh mục cho View
            ViewBag.Categories = new SelectList(db.Category, "IDCate", "NameCate");
            return View(proFilter.ToList());
        }
        public ActionResult Index(decimal?_min, decimal? _max, string _category)
        {
            var proFilter = db.Products.AsQueryable();
            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);
            if (!string.IsNullOrEmpty(_category))
                proFilter = proFilter.Where(s => s.IDCate == _category);

            // Truyền danh sách danh mục cho View
            ViewBag.Categories = new SelectList(db.Category, "IDCate", "NameCate");
            return View(proFilter.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products pro)
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            if (ModelState.IsValid)
            {
                if (pro.ImagePath != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImagePath.FileName);
                    string extention = Path.GetExtension(pro.ImagePath.FileName);
                    fileName = fileName + extention;
                    pro.ImagePath.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    pro.ImagePro = "~/Image/" + fileName;
                }
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            var pro = db.Products.Find(id);
            return View(pro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products pro)
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            if (ModelState.IsValid)
            {
                var proEdit = db.Products.Find(pro.ProductID);
                proEdit.NamePro = pro.NamePro;
                proEdit.DecriptionPro = pro.DecriptionPro;
                proEdit.Price=pro.Price;
                proEdit.IDCate = pro.IDCate;
                if (pro.ImagePath != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImagePath.FileName);
                    string extention = Path.GetExtension(pro.ImagePath.FileName);
                    fileName = fileName + extention;
                    pro.ImagePath.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    proEdit.ImagePro = "~/Image/" + fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            var pro = db.Products.Find(id);
            return View(pro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa(Products pro)
        {
            ViewBag.ListCate = new SelectList(db.Category, "IDCate", "NameCate");
            var proXoa=db.Products.Find(pro.ProductID);
            try
            {
                db.Products.Remove(proXoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.errXoa = "Không Thề Xóa Sản Phẩm Này (Đang được sử dụng)";
            }
            return View(pro);
        }
    }
}
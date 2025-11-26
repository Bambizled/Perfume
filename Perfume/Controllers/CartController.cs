using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;

namespace Perfume.Controllers
{
    public class CartController : Controller
    {
        //Khởi tạo CSDL
        DBVinamilkEntities db=new DBVinamilkEntities();
        //Tạo Session giỏ hàng bằng danh sách các dòng hàng
        public List<CartItem> GetCartItems()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart==null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // GET: giao diện giỏ hàng_cart
        public ActionResult Index()
        {
            var cart = GetCartItems();//khai báo biến giỏ hàng
            ViewBag.Total=cart.Sum(s=>s.total);//tính tổng tất cả các dòng hàng
            ViewBag.Count=cart.Sum(s=>s.quantity);//tồng số lượng hàng
            return View(cart);
        }
        public ActionResult Add(int id)
        {
            var cart= GetCartItems();//Tạo giao diện dòng hàng
            var product = db.Products.Find(id);//Khi layout chọn sp vào giỏ hàng thì chuyền bằng ID
            var item = cart.FirstOrDefault(s => s.idPro == id);//mã sp đối chiếu có trong SCDL, nếu có => gắn vào giỏ hàng(sp đó đóng vai tò item)
            //TH sp đã có trong giỏ hàng thì tăng số lượng lên
            if (item != null)
                item.quantity++;
            else
                cart.Add(new CartItem
                {
                    idPro = id,
                    namePro = product.NamePro,
                    quantity = 1,
                    price=(decimal)product.Price
                    
                });
            return RedirectToAction("Index");
        }
        //Viết hàm xóa trong giỏ hàng
        public ActionResult Remove(int id)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(s => s.idPro == id);
            if (item != null)
                cart.Remove(item);
            return RedirectToAction("Index");
        }
        //Viết chức năng Thanh Toán
        public ActionResult ThanhToan(int IDCus,string PhoneCus, string DCNhanHang, string SDT)
        {
            var cart=GetCartItems();
            if(!cart.Any())
            {
                TempData["Error"] = "Lỗi sản phẩm";
                return RedirectToAction("Index");
            }
            var order = new OrderPro
            {
                DateOrder= DateTime.Now,
                AddressDeliverry= DCNhanHang,
                TotalAmount=cart.Sum(s=>s.total)
            };
            db.OrderPro.Add(order);
            db.SaveChanges();
            //lưu dòng chi tiết vào OrderDetail
            foreach(var item in cart)
            {
                var orderdetail = new OrderDetail
                {
                    IDOrder=order.ID,
                    IDProduct=item.idPro,
                    Quantity=item.quantity,
                    UnitPrice=(double?)item.price
                };
                db.OrderDetail.Add(orderdetail);
            }
            db.SaveChanges();
            //Sau khi lưu dl => giỏ hàng rỗng
            Session["Cart"] = null;
            TempData["Sucess"] = "Thanh Toán Thành Công";
            return RedirectToAction("IndexCustomer", "Product");
        }
    }
}
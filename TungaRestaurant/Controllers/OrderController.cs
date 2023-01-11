using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TungaRestaurant.Models;
using TungaRestaurant.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace TungaRestaurant.Controllers
{
    public class OrderController : Controller, IActionFilter
    {

        private List<OrderDetail> listOrderDetail = new List<OrderDetail>();

        private readonly TungaRestaurantDbContext _context;

        public OrderController(TungaRestaurantDbContext tungaRestaurantDbContext)
        {
            _context = tungaRestaurantDbContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var myOrder = HttpContext.Session.GetString("myOrder");
            if (myOrder != null)
            {
                listOrderDetail = JsonConvert.DeserializeObject<List<OrderDetail>>(myOrder);
            }
            base.OnActionExecuting(context);
        }

        public IActionResult AddToOrder(int foodId, int? quantity)
        {
            var food = listOrderDetail.FirstOrDefault(p => p.FoodId == foodId);
            if (food != null)
            {
                if (quantity == null)
                    listOrderDetail.Where(p => p.FoodId == foodId).First().Quantity += 1;
                else
                    listOrderDetail.Where(p => p.FoodId == foodId).First().Quantity += quantity.Value;
            }
            else
            {
                var f = _context.Foods.FirstOrDefault(f => f.Id == foodId);
                OrderDetail od = new OrderDetail() { Id = p.Id, Name = p.Name, Price = p.Price, Image = p.Image, Quantity = 1 };
                listOrderDetail.Add(od);
            }

            //chuyen du lieu thanh json
            var data = JsonConvert.SerializeObject(listOrderDetail);
            HttpContext.Session.SetString("myOrder", data);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateOrder(int id, int quantity) //update quantity cua food tren trang gio hang
        {
            var product = listOrderDetail.FirstOrDefault(p => p.Id == id);
            if (quantity <= 0)
            {
                var c = listOrderDetail.FirstOrDefault(p => p.Id == id);
                listOrderDetail.Remove(c);
            }
            else
            {
                listOrderDetail.Where(p => p.Id == id).First().Quantity = quantity;
            }            

            //chuyen du lieu thanh json
            var data = JsonConvert.SerializeObject(listOrderDetail);
            HttpContext.Session.SetString("myOrder", data);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoteProdutFromOrder(int id)
        {
            var c = listOrderDetail.FirstOrDefault(p => p.Id == id);
            listOrderDetail.Remove(c);

            //chuyen du lieu thanh json
            var data = JsonConvert.SerializeObject(listOrderDetail);
            HttpContext.Session.SetString("myOrder", data);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("myOrder");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult OrderOrder()
        {
            //xu ly dat hang
            //luu vao bang order truoc
            //xet da dang nhap chua, dang nhap roi thi se lay thong tin account ra.

            //duyet danh sach hang va luu vao bang orderdetail

            //remove Order di
            HttpContext.Session.Remove("myOrder");

            return RedirectToAction(nameof(Index));
        }
    }
}
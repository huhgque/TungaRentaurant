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

        
    }
}
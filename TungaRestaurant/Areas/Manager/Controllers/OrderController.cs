using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,Branch Manager")]
    public class OrderController : Controller
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly TungaRestaurantDbContext _context;
        public OrderController(UserManager<UserInfo> userManager, TungaRestaurantDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.Include(o=>o.OrderDetail).ToList();
            ViewBag.Orders = orders;
            return View();
        }
        public IActionResult Detail(int id)
        {
            List<Order> order = _context.Orders.Include(o => o.OrderDetail).Where(o=>o.Id == id).ToList();
            ViewBag.Order = order;
            return View();
        }
    }
}

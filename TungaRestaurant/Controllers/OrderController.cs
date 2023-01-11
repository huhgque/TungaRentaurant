﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TungaRestaurant.Models;
using TungaRestaurant.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TungaRestaurant.Controllers
{
    public class OrderController : Controller, IActionFilter
    {

        private List<OrderDetail> listOrderDetail = new List<OrderDetail>();

        private readonly TungaRestaurantDbContext _context;
        private readonly UserManager<UserInfo> _userManager;

        public OrderController(TungaRestaurantDbContext context, UserManager<UserInfo> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            //return ve trang checkout
            return View("/Views/Order/CheckOut.cshtml");
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            Order order = new Order();
            if (ModelState.IsValid)
            {
                try
                {
                    order.UserInfoId = _userManager.GetUserId(User);
                    //lay cart
                    List<Cart> carts = _context.Carts.Include(c=>c.Food).Where(c => c.UserInfoId == _userManager.GetUserId(User)).ToList();
                    order.Name = _userManager.GetUserName(User);
                    float totalPrice = 0;
                    foreach (var cart in carts)
                    {
                        totalPrice += cart.Quantity * cart.Food.Price;
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.FoodId = cart.FoodId;
                        orderDetail.Price = cart.Food.Price;
                        orderDetail.Quantity = cart.Quantity;
                        order.OrderDetail.Add(orderDetail);
                    }
                    order.Price = totalPrice;
                    order.CreatedAt = DateTime.Now;


                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ViewBag.error = "This item has exist in your Order. Please add or reduce it by go to Order page!";
                }
            }
            return RedirectToAction(nameof(Index));
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

    }
}
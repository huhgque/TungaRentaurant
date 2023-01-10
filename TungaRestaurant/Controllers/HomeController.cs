using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

namespace TungaRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TungaRestaurantDbContext _context;
        private readonly UserManager<UserInfo> _userManager;

        public HomeController(ILogger<HomeController> logger,TungaRestaurantDbContext tungaRestaurantDbContext,UserManager<UserInfo> userManager)
        {
            _logger = logger;
            _context = tungaRestaurantDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Menu(int? branch,[DefaultValue(false)] bool isVegan)
        {
            UserInfo user = null;
            List<Branch> branches = await _context.Branch.ToListAsync();
            ViewBag.Branches = branches;
            if (User != null && branch == null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
                branch = user.PreferBranchId;
                isVegan = user.IsVegan;
            }
            if (branch == null)
            {
                return View("Views/Home/SelectBranch.cshtml");
            }
            if(user != null)
            {
                user.PreferBranchId = branch;
                await _userManager.UpdateAsync(user);
            }
            // query by food
            //IQueryable<Food> foods = _context.Foods.Include(f => f.Category);
            //if (isVegan)
            //{
            //    foods.Where(f => f.IsVeganDish);
            //}
            //foods.Where(f => f.BranchId == null || f.BranchId == branch);
            //ViewBag.Foods = await foods.ToListAsync();

            //query by category
            IQueryable<Category> categories ;
            if (isVegan)
            {
                ViewBag.Categories = _context.Categories.Include(c => c.Foods.Where(f => f.IsVeganDish && ( f.BranchId == branch || f.BranchId == null ) )).AsQueryable();
            }
            else
            {
                ViewBag.Categories = _context.Categories.Include(c => c.Foods ).Where(categories=>categories.Foods.Where(f=>f.BranchId==branch || f.BranchId == null).FirstOrDefault() != null).ToList();
            }
            
            return View();
        }
    }
}

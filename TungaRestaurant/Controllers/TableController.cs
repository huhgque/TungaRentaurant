using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

namespace TungaRestaurant.Controllers
{
    public class TableController : Controller
    {
        private readonly TungaRestaurantDbContext _context;
        private readonly UserManager<UserInfo> _userManager;

        public TableController(TungaRestaurantDbContext context,UserManager<UserInfo> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Food = await _context.Foods.ToListAsync();
            return View("~/Views/Home/TableReservation.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookATable([Bind("firstName,lastName,date,time,time_to,phone,type,numberOfGuest,message")] TableBookInfor tableBookInfor)
        {
            
            if (ModelState.IsValid)
            {
                DateTime revDate = DateTime.ParseExact(tableBookInfor.date + " " + tableBookInfor.time, "M/d/yyyy h:mmtt", CultureInfo.InvariantCulture);
                DateTime revEnd = DateTime.ParseExact(tableBookInfor.date + " " + tableBookInfor.time_to, "M/d/yyyy h:mmtt", CultureInfo.InvariantCulture);
                
                Table tables = await _context.Table
           
                .Where(t => t.NumberOfGuest >= tableBookInfor.numberOfGuest)
                .Where(t =>
                  _context.Reservations
                        .Where(re => re.ReservationAt.Date == revDate.Date)
                        .Where(re => re.ReservationAt < revEnd && revDate < re.ReservationEnd)
                      .FirstOrDefault(re => re.TableId == t.Id) == null
                 )
                .FirstOrDefaultAsync();
                if (tables == null)
                {
                    TempData["Message"] = "No table available at this time stamp";
                    return RedirectToAction("Index", "Home");
                }
               
                                   
              
                Reservation reservation = new Reservation();
                reservation.CreatedAt = DateTime.Now;
                reservation.NumberOfGuest = tableBookInfor.numberOfGuest;
                reservation.ReservationAt = revDate;
                reservation.ReservationEnd = revEnd;
                reservation.Status = ReservationStatus.END;
                reservation.TableId = tables.Id;
                if (User != null)
                {
                    UserInfo loginUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    reservation.UserId = loginUser.Id;
                }
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Table " + tables.Name + " booked at "+ revDate;
            }
            else
            {
                TempData["Message"] = "Err";
            }
            return RedirectToAction("Index","Home");
        }
    }
}

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

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookATable([Bind("firstName,lastName,date,time,phone,type,numberOfGuest,message")] TableBookInfor tableBookInfor)
        {
         
            Table tables =  await _context.Table
                .Where(t => t.NumberOfGuest>= tableBookInfor.numberOfGuest)
                .Where(t => 
                    _context.Reservations
                        .Where(re => re.ReservationAt.Date != DateTime.Now.Date)
                        .FirstOrDefault(re => re.TableId == t.Id) == null
                    )
                    .FirstOrDefaultAsync();
          
            DateTime revDate = DateTime.ParseExact(tableBookInfor.date + " " + tableBookInfor.time, "M/dd/yyyy h:mmtt", CultureInfo.InvariantCulture);
            UserInfo loginUser = await _userManager.FindByNameAsync(User.Identity.Name);
            Reservation reservation = new Reservation();
            reservation.CreatedAt = DateTime.Now;
            reservation.NumberOfGuest = tableBookInfor.numberOfGuest;
            reservation.UserId = loginUser.Id;
            reservation.ReservationAt = revDate;
            reservation.Status = ReservationStatus.BOOKED;
            reservation.TableId = tables.Id;
            _context.Add(reservation);
            await _context.SaveChangesAsync();

            


            return View("Index");
        }
    }
}

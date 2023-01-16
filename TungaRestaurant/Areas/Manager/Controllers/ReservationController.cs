using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Authorize("Manage")]
    [Authorize(Roles = "Admin,Branch Manager")]
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

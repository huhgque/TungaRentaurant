using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TungaRestaurant.Data;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TestDbController : Controller
    {
        private readonly TungaRestaurantDbContext _dbContext;
        public TestDbController(TungaRestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            
            return View(_dbContext.Categories.ToList());
        }
    }
}

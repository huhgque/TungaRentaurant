using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TungaRestaurant.Models;

namespace TungaRestaurant.Controllers
{
    public class TableBookInfor
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }  
        [Required]
        public string date { get; set; }
        [Required]
        public string time { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public TableType type { get; set; }
        [Required]
        public int numberOfGuest { get; set; }
        [Required]
        public string message { get; set; }

    }
}

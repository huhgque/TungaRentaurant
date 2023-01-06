using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TungaRestaurant.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int CookDuration  {get; set;}

        [ForeignKey("Branch")]
        public int? BranchId {get; set;}
        public int ServeUnit {get; set;}
        public FoodStatus Status { get; set; } = FoodStatus.AVAILABLE;
        public virtual List<CategoryDetail> Categories { get; set; }
        public Branch Branch { get; set; }
        
    }
}

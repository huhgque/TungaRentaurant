using System;
using System.Collections.Generic;
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
        public int BranchId {get; set;}
        public int ServeUnit {get; set;}
        public int Status { get; set; }
        public virtual List<CategoryDetail> Categories { get; set; }
    }
}

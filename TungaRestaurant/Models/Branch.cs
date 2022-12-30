﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TungaRestaurant.Models
{

    public class Branch
    {
        public int Id { get; set; }

        public string Name { get; set; }  
        public string Location { get; set; }

        public int Status { get; set; }
        public virtual List<UserInfo> Users { get; set; }
    }
}

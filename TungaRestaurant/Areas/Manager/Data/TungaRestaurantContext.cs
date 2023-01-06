using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TungaRestaurant.Models;

namespace TungaRestaurant.Data
{
    public class TungaRestaurantContext : DbContext
    {
        public TungaRestaurantContext (DbContextOptions<TungaRestaurantContext> options)
            : base(options)
        {
        }

        public DbSet<TungaRestaurant.Models.Table> Table { get; set; }
    }
}

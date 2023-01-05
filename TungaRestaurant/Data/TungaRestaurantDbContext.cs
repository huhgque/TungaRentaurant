using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TungaRestaurant.Models;

namespace TungaRestaurant.Data
{
    public class TungaRestaurantDbContext : IdentityDbContext<UserInfo>
    {
        public TungaRestaurantDbContext(DbContextOptions<TungaRestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CategoryDetail>().HasKey(detail => new { detail.FoodId, detail.CategoryId });
            builder.Entity<CategoryDetail>().HasOne<Food>(detail => detail.Food).WithMany(food => food.Categories).HasForeignKey(detail => detail.FoodId);
            builder.Entity<CategoryDetail>().HasOne<Category>(detail => detail.Category).WithMany(category => category.Foods).HasForeignKey(detail => detail.CategoryId);

            builder.Entity<OrderDetail>().HasKey(order => new { order.OrderId });
            builder.Entity<ReservationDetail>().HasKey(reserv => reserv.ReservationId);
        }
    }
}

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

[assembly: HostingStartup(typeof(TungaRestaurant.Areas.Identity.IdentityHostingStartup))]
namespace TungaRestaurant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TungaRestaurantDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TungaRestaurantContextConnection")));

                services.AddDefaultIdentity<UserInfo>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TungaRestaurantDbContext>();
            });
        }
    }
}
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TungaRestaurant.Data;

[assembly: HostingStartup(typeof(TungaRestaurant.Areas.Identity.IdentityHostingStartup))]
namespace TungaRestaurant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TungaRestaurantContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TungaRestaurantContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TungaRestaurantContext>();
            });
        }
    }
}
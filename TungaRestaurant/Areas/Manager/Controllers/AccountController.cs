﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TungaRestaurant.Models;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserInfo> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<UserInfo> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            List<UserInfo> users = userManager.Users.ToList();
            return View(users);
        }
        public IActionResult Create()
        {
            List<IdentityRole> roles = roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Email,Password,DisplayName,Phone,Sex,Address")] UserInfo user,[FromForm] string RoleName,[FromForm] string Password)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            IdentityRole role = await roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                return RedirectToAction(nameof(Index));
            }
            user.PasswordHash = Password;
            user.UserName = user.Email;
            var Result = await userManager.CreateAsync(user);
            var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string id)
        {
            UserInfo user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var roles = roleManager.Roles.ToList();
            ViewBag.StatusList = Enum.GetValues( typeof(UserStatus) );
            ViewBag.Roles = roles;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Email,Password,DisplayName,Phone,Sex,Address")] UserInfo user, [FromForm] string RoleName, [FromForm] string Password)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            IdentityRole role = await roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                return RedirectToAction(nameof(Index));
            }
            user.PasswordHash = Password;
            user.UserName = user.Email;
            var currentRoles = userManager.GetRolesAsync(user).Result;
            string currentRole = (currentRoles.Count > 0) ? currentRoles[0] : "";
            UserInfo userFromDb = userManager.Users.Where(u => u.Id.Equals(user.Id)).First();
            user.ConcurrencyStamp = userFromDb.ConcurrencyStamp;
            var updateResl = await userManager.UpdateAsync(user);
            if (!currentRole.Equals(role))
            {
                var removeFromRole = await userManager.RemoveFromRoleAsync(user,role.Name);
                var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
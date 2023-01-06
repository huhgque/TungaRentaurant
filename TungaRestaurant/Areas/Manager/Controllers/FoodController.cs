﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class FoodController:Controller
    {
        private readonly TungaRestaurantDbContext _dbContext;
        public FoodController(TungaRestaurantDbContext context)
        {
            _dbContext = context;
        }
        // GET: Food
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Foods.ToListAsync());
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Food = await _dbContext.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Food == null)
            {
                return NotFound();
            }

            return View(Food);
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Price,CookDuration,BranchId,ServeUnit,Status")] Food Food)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Add(Food);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.error = "This food has existed! Please contact with admin for more infor." + ex;
                    return View(Food);
                }
            }
            return View(Food);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Food = await _dbContext.Foods.FindAsync(id);
            if (Food == null)
            {
                return NotFound();
            }
            return View(Food);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Price,CookDuration,BranchId,ServeUnit,Status")] Food Food)
        {
            if (id != Food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(Food);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(Food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Food);
        }

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Food = await _dbContext.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Food == null)
            {
                return NotFound();
            }

            return View(Food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Food = await _dbContext.Foods.FindAsync(id);
            _dbContext.Foods.Remove(Food);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _dbContext.Foods.Any(e => e.Id == id);
        }
    }
}
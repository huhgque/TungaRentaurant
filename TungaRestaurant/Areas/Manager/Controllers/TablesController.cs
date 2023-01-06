using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TungaRestaurant.Data;
using TungaRestaurant.Models;

namespace TungaRestaurant.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TablesController : Controller
    {
        private readonly TungaRestaurantDbContext _context;

        public TablesController(TungaRestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Manager/Tables
        public async Task<IActionResult> Index(int? branche,String? tableType)
        {
            ViewBag.BranchList = await _context.Branch.ToListAsync();
            IQueryable<Table> tunga ;
            ViewBag.Reservations = await _context.Reservations.Where(r => r.CreatedAt >= DateTime.Now.AddMonths(-1)).Include(r=>r.Table).ToListAsync();
            if (branche==null)
            {
                var id = _context.Table.FirstOrDefault().Id;
                tunga = _context.Table.Where(t => t.BranchId ==(id)).Include(t => t.Branch);
                ViewBag.Branch = id;
            }
            else
            {
                tunga = _context.Table.Include(t => t.Branch).Where(t=>t.BranchId==branche);
                ViewBag.Branch = branche;
            }


            if (String.IsNullOrEmpty(tableType) )
            {
                
                ViewBag.tableType = null;
            }
            else if(tableType=="ALL")
            {
                ViewBag.tableType = "ALL";
            }else
            {
                if (tableType == TableType.PUBLIC.ToString())
                {
                    tunga = tunga.Where(t => t.Type == TableType.PUBLIC);
                }
                else
                {
                    tunga = tunga.Where(t => t.Type == TableType.PRIVATE);
                }
                
                ViewBag.tableType = tableType;
            }
                return View(await tunga.ToListAsync());
        }

        // GET: Manager/Tables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Table
                .Include(t => t.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: Manager/Tables/Create
        public IActionResult Create()
        {
           
            ViewData["BranchId"] = new SelectList(_context.Set<Branch>(), "Id", "Id");
            return View();
        }

        // POST: Manager/Tables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,BranchId,NumberOfGuest,Status")] Table table)
        {
            if (ModelState.IsValid)
            {
                _context.Add(table);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Set<Branch>(), "Id", "Id", table.BranchId);
            return View(table);
        }

        // GET: Manager/Tables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _context.Table.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Set<Branch>(), "Id", "Id", table.BranchId);
            return View(table);
        }

        // POST: Manager/Tables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,BranchId,NumberOfGuest,Status")] Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Set<Branch>(), "Id", "Id", table.BranchId);
            return View(table);
        }

        // GET: Manager/Tables/Delete/5
        

        // POST: Manager/Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table = await _context.Table.FindAsync(id);
            _context.Table.Remove(table);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return _context.Table.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectUni.Data;
using FinalProjectUni.Models;
//using weka.gui.beans;
using Hangfire.Common;
//using weka.classifiers.functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace FinalProjectUni.Controllers
{
    public class MarketDashBsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarketDashBsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MarketDashBs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") { return NotFound(); }

            return View(await _context.MarketDashB.ToListAsync());
        }

        // GET: MarketDashBs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var marketDashB = await _context.MarketDashB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketDashB == null)
            {
                return NotFound();
            }

            return View(marketDashB);
        }

        // GET: MarketDashBs/Create
        [Authorize]
        public IActionResult Create()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            return View();
        }

        // POST: MarketDashBs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id")] MarketDashB marketDashB)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            if (ModelState.IsValid)
            {
                _context.Add(marketDashB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marketDashB);
        }

        // GET: MarketDashBs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var marketDashB = await _context.MarketDashB.FindAsync(id);
            if (marketDashB == null)
            {
                return NotFound();
            }
            return View(marketDashB);
        }

        // POST: MarketDashBs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] MarketDashB marketDashB)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            if (id != marketDashB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marketDashB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarketDashBExists(marketDashB.Id))
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
            return View(marketDashB);
        }

        // GET: MarketDashBs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var marketDashB = await _context.MarketDashB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketDashB == null)
            {
                return NotFound();
            }

            return View(marketDashB);
        }

        // POST: MarketDashBs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminm@admin.com") return NotFound();
            var marketDashB = await _context.MarketDashB.FindAsync(id);
            _context.MarketDashB.Remove(marketDashB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarketDashBExists(int id)
        {
            return _context.MarketDashB.Any(e => e.Id == id);
        }
    }
}

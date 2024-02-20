using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectUni.Data;
using FinalProjectUni.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

namespace FinalProjectUni.Controllers
{
    public class SystemMangerPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemMangerPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemMangerPages
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            return View(await _context.SystemMangerPage.ToListAsync());
        }

        // GET: SystemMangerPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemMangerPage = await _context.SystemMangerPage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemMangerPage == null)
            {
                return NotFound();
            }

            return View(systemMangerPage);
        }

        // GET: SystemMangerPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemMangerPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SystemMangerPage systemMangerPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemMangerPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemMangerPage);
        }

        // GET: SystemMangerPages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemMangerPage = await _context.SystemMangerPage.FindAsync(id);
            if (systemMangerPage == null)
            {
                return NotFound();
            }
            return View(systemMangerPage);
        }

        // POST: SystemMangerPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] SystemMangerPage systemMangerPage)
        {
            if (id != systemMangerPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemMangerPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemMangerPageExists(systemMangerPage.Id))
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
            return View(systemMangerPage);
        }
        [Authorize]
        // GET: SystemMangerPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemMangerPage = await _context.SystemMangerPage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemMangerPage == null)
            {
                return NotFound();
            }

            return View(systemMangerPage);
        }

        // POST: SystemMangerPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemMangerPage = await _context.SystemMangerPage.FindAsync(id);
            _context.SystemMangerPage.Remove(systemMangerPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemMangerPageExists(int id)
        {
            return _context.SystemMangerPage.Any(e => e.Id == id);
        }
    }
}

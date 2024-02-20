using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCRProject.Data;
using OCRProject.Models;

namespace OCRProject.Controllers
{
    public class OCRScansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OCRScansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OCRScans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OCRScan.Include(o => o.Pg).Include(o => o.Tem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OCRScans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oCRScan = await _context.OCRScan
                .Include(o => o.Pg)
                .Include(o => o.Tem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oCRScan == null)
            {
                return NotFound();
            }

            return View(oCRScan);
        }

        // GET: OCRScans/Create
        public IActionResult Create()
        {
            ViewData["PageId"] = new SelectList(_context.Set<Page>(), "Id", "Id");
            ViewData["TemplateId"] = new SelectList(_context.Set<Template>(), "Id", "Id");
            return View();
        }

        // POST: OCRScans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TemplateId,PageId,Value,Note")] OCRScan oCRScan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oCRScan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PageId"] = new SelectList(_context.Set<Page>(), "Id", "Id", oCRScan.PageId);
            ViewData["TemplateId"] = new SelectList(_context.Set<Template>(), "Id", "Id", oCRScan.TemplateId);
            return View(oCRScan);
        }

        // GET: OCRScans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oCRScan = await _context.OCRScan.FindAsync(id);
            if (oCRScan == null)
            {
                return NotFound();
            }
            ViewData["PageId"] = new SelectList(_context.Set<Page>(), "Id", "Id", oCRScan.PageId);
            ViewData["TemplateId"] = new SelectList(_context.Set<Template>(), "Id", "Id", oCRScan.TemplateId);
            return View(oCRScan);
        }

        // POST: OCRScans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TemplateId,PageId,Value,Note")] OCRScan oCRScan)
        {
            if (id != oCRScan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oCRScan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OCRScanExists(oCRScan.Id))
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
            ViewData["PageId"] = new SelectList(_context.Set<Page>(), "Id", "Id", oCRScan.PageId);
            ViewData["TemplateId"] = new SelectList(_context.Set<Template>(), "Id", "Id", oCRScan.TemplateId);
            return View(oCRScan);
        }

        // GET: OCRScans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oCRScan = await _context.OCRScan
                .Include(o => o.Pg)
                .Include(o => o.Tem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oCRScan == null)
            {
                return NotFound();
            }

            return View(oCRScan);
        }

        // POST: OCRScans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oCRScan = await _context.OCRScan.FindAsync(id);
            _context.OCRScan.Remove(oCRScan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OCRScanExists(int id)
        {
            return _context.OCRScan.Any(e => e.Id == id);
        }
    }
}

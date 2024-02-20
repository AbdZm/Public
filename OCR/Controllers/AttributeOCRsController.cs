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
    public class AttributeOCRsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeOCRsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttributeOCRs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AttributeOCR.ToListAsync());
        }

        // GET: AttributeOCRs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeOCR = await _context.AttributeOCR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeOCR == null)
            {
                return NotFound();
            }

            return View(attributeOCR);
        }

        // GET: AttributeOCRs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttributeOCRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Val")] AttributeOCR attributeOCR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributeOCR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attributeOCR);
        }

        // GET: AttributeOCRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeOCR = await _context.AttributeOCR.FindAsync(id);
            if (attributeOCR == null)
            {
                return NotFound();
            }
            return View(attributeOCR);
        }

        // POST: AttributeOCRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Val")] AttributeOCR attributeOCR)
        {
            if (id != attributeOCR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeOCR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeOCRExists(attributeOCR.Id))
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
            return View(attributeOCR);
        }

        // GET: AttributeOCRs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeOCR = await _context.AttributeOCR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeOCR == null)
            {
                return NotFound();
            }

            return View(attributeOCR);
        }

        // POST: AttributeOCRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attributeOCR = await _context.AttributeOCR.FindAsync(id);
            _context.AttributeOCR.Remove(attributeOCR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeOCRExists(int id)
        {
            return _context.AttributeOCR.Any(e => e.Id == id);
        }
    }
}

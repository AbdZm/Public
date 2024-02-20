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
    public class AccountUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountUsers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com")
            {
                
                var applicationDbContextNotAdmin = _context.AccountUser.Include(a => a.Account).Include(a => a.MyUser).Where(a => a.MyUser.Email.Contains(user));
                return View(await applicationDbContextNotAdmin.ToListAsync());
            }
            var applicationDbContext = _context.AccountUser.Include(a => a.Account).Include(a => a.MyUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AccountUsers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var accountUser = await _context.AccountUser
                .Include(a => a.Account)
                .Include(a => a.MyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountUser == null)
            {
                return NotFound();
            }

            return View(accountUser);
        }

        // GET: AccountUsers/Create
        [Authorize]
        public IActionResult Create()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home"); 
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Type");
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "FirstName");
            return View();
        }

        // POST: AccountUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Amount,AccountId,MyUserId")] AccountUser accountUser)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home");
            if (ModelState.IsValid)
            {
                _context.Add(accountUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id", accountUser.AccountId);
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id", accountUser.MyUserId);
            return View(accountUser);
        }

        // GET: AccountUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var accountUser = await _context.AccountUser.FindAsync(id);
            if (accountUser == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Type", accountUser.AccountId);
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "FirstName", accountUser.MyUserId);
            return View(accountUser);
        }

        // POST: AccountUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,AccountId,MyUserId")] AccountUser accountUser)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home");
            if (id != accountUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountUserExists(accountUser.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id", accountUser.AccountId);
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id", accountUser.MyUserId);
            return View(accountUser);
        }

        // GET: AccountUsers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home");
            if (id == null)
            {
                return NotFound();
            }

            var accountUser = await _context.AccountUser
                .Include(a => a.Account)
                .Include(a => a.MyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountUser == null)
            {
                return NotFound();
            }

            return View(accountUser);
        }

        // POST: AccountUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return RedirectToAction("Privacy", "Home");
            var accountUser = await _context.AccountUser.FindAsync(id);
            _context.AccountUser.Remove(accountUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountUserExists(int id)
        {
            return _context.AccountUser.Any(e => e.Id == id);
        }

        //public string GetUntilOrEmpty(string text)
        //{
        //    string stopAt = "@";
        //    if (!String.IsNullOrWhiteSpace(text))
        //    {
        //        int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

        //        if (charLocation > 0)
        //        {
        //            return text.Substring(0, charLocation);
        //        }
        //    }

        //    return String.Empty;
        //}
    }
}

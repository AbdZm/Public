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
    public class MyUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyUsers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            return View(await _context.MyUser.ToListAsync());
        }

        // GET: MyUsers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var myUser = await _context.MyUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myUser == null)
            {
                return NotFound();
            }

            return View(myUser);
        }

        // GET: MyUsers/Create
        [Authorize]
        public IActionResult Create()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            return View();
        }

        // POST: MyUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Gender,Email,Phone,SecretKey,Status")] MyUser myUser)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            if (ModelState.IsValid)
            {
                _context.Add(myUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myUser);
        }

        // GET: MyUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var myUser = await _context.MyUser.FindAsync(id);
            if (myUser == null)
            {
                return NotFound();
            }
            return View(myUser);
        }

        // POST: MyUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gender,Email,Phone,SecretKey,Status")] MyUser myUser)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            if (id != myUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyUserExists(myUser.Id))
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
            return View(myUser);
        }

        // GET: MyUsers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var myUser = await _context.MyUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myUser == null)
            {
                return NotFound();
            }

            return View(myUser);
        }

        // POST: MyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com") return NotFound();
            var myUser = await _context.MyUser.FindAsync(id);
            _context.MyUser.Remove(myUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyUserExists(int id)
        {
            return _context.MyUser.Any(e => e.Id == id);
        }
    }
}

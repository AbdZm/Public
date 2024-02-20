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
using Microsoft.CodeAnalysis.Classification;
using FinalProjectUni.Classifier;

namespace FinalProjectUni.Controllers
{
    public class FinancesController : Controller
    {
       // InstanceEvent AllAtribute = null;
        private readonly ApplicationDbContext _context;

        public FinancesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Thnx()
        {
            return View();
        }

        // GET: Finances
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com")
            {
                //var applicationDbContextClient = _context.Finance.Include(f => f.MyUser).Where(f => f.MyUser.Email == user);
                //return View(await applicationDbContextClient.ToListAsync());
                return RedirectToAction("Privacy", "Home");
            }
            var applicationDbContext = _context.Finance.Include(f => f.MyUser);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> IndexClient()
        {
            var user = User.Identity.GetUserName();
            if (user.StartsWith("admin"))
            {
                //return NotFound();
                return RedirectToAction("Privacy", "Home");

            }
            var applicationDbContextClient = _context.Finance.Include(f => f.MyUser).Where(f => f.MyUser.Email == user);
            return View(await applicationDbContextClient.ToListAsync());
        }
        [Authorize]
        // GET: Finances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com") return RedirectToAction("Privacy", "Home");
            if (id == null)
            {
                return RedirectToAction("Privacy", "Home");
            }

            var finance = await _context.Finance
                .Include(f => f.MyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finance == null)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(finance);
        }
        [Authorize]
        // GET: Finances/Create
        public IActionResult Create()
        {
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id");
            return View();
        }
        
        // POST: Finances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,SecretKey,Type,Gender,Age,Married,Dependents,Education,SelfEmployed,ApplicantIncome,IncomeContinuation,CoapplicantIncome,FinanceAmount,FinanceAmountTerm,Credit_History,ReleaseDate")] Finance finance)
        {
            double[] input = new double[10];
            ID3 classifier = new ID3();
            try
            {
                if (ModelState.IsValid)
                {
            
                    if (finance.Gender == "Male") input[0] = 1;
                    else input[0] = 0;

                    if (finance.Married == "Yes") input[1] = 1;
                    else input[1] = 0;

                    if (finance.Dependents >= 3) input[2] = 3;
                    else input[2] = finance.Dependents; 

                    if (finance.Education == "Graduate") input[3] = 1;
                    else input[3] = 0;

                    if (finance.SelfEmployed == "Yes") input[4] = 1;
                    else input[4] = 0;

                    input[5] = finance.ApplicantIncome;

                    input[6] = finance.CoapplicantIncome;

                    input[7] = finance.FinanceAmount;

                    input[8] = finance.FinanceAmountTerm;

                    input[9] = finance.Credit_History;

                    var rs = classifier.Score(input);
                    if (rs[0] == 0) finance.Loan_Status = "Yes";
                    else finance.Loan_Status = "No";
                    
                    var rate = finance.ApplicantIncome / (finance.FinanceAmount/finance.FinanceAmountTerm);
                    finance.IncomeRate = rate;
                    finance.IncomeRisk = Risk(rate, finance.Loan_Status);
                    finance.Done = false;
                    var IdU = _context.MyUser.Where(x => x.SecretKey.Equals(finance.SecretKey.ToString())).ToList();
                    if (!IdU.Any())
                    {
                        return View(finance);
                    }
                    foreach (var item in IdU)
                    {
                        finance.MyUserId = item.Id;
                    }
                    _context.Add(finance);
                    await _context.SaveChangesAsync();

                    //return RedirectToAction(nameof(Index));
                    //return View();
                    return RedirectToAction("Thnx", "Finances");
                }
                ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id", finance.MyUserId);
                return View();
            }
            catch (Exception e)
            {
                return View();
                
            }
        }

        // GET: Finances/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com") return RedirectToAction("Privacy", "Home");
            if (id == null)
            {
                return RedirectToAction("Privacy", "Home");
            }

            var finance = await _context.Finance.FindAsync(id);
            if (finance == null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id", finance.MyUserId);
            return View(finance);
        }

        // POST: Finances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MyUserId,SecretKey,Type,Gender,Age,Married,Dependents,Education,SelfEmployed,ApplicantIncome,IncomeContinuation,IncomeRate,IncomeRisk,CoapplicantIncome,FinanceAmount,FinanceAmountTerm,Credit_History,Loan_Status,ReleaseDate,Done,final_decision")] Finance finance)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com") return RedirectToAction("Privacy", "Home");
            if (id != finance.Id)
            {
                return RedirectToAction("Privacy", "Home");
            }

            if (ModelState.IsValid)
            {
                var Loan_Status = finance.Loan_Status;
                try
                {
                    Loan_Status = "Yes";
                    var rate = finance.ApplicantIncome / finance.FinanceAmount;
                    finance.IncomeRate = rate;
                    finance.IncomeRisk = Risk(rate, Loan_Status);
                    finance.Loan_Status = Loan_Status;
                    var IdU = _context.MyUser.Where(x => x.SecretKey.Equals(finance.SecretKey.ToString())).ToList();
                    if (!IdU.Any())
                    {
                        return View(finance);
                    }
                    foreach (var item in IdU)
                    {
                        finance.MyUserId = item.Id;
                    }

                    _context.Update(finance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinanceExists(finance.Id))
                    {
                        return RedirectToAction("Privacy", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MyUserId"] = new SelectList(_context.MyUser, "Id", "Id", finance.MyUserId);
            return View(finance);
        }

        // GET: Finances/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com") return RedirectToAction("Privacy", "Home");
            if (id == null)
            {
                return RedirectToAction("Privacy", "Home");
            }

            var finance = await _context.Finance
                .Include(f => f.MyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finance == null)
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(finance);
        }

        // POST: Finances/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User.Identity.GetUserName();
            if (user != "admin@admin.com" && user != "adminf@admin.com") return RedirectToAction("Privacy", "Home");
            var finance = await _context.Finance.FindAsync(id);
            _context.Finance.Remove(finance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public String Cls(int Dep)
        //{
        //    if (Dep < 3) return "Yes";
        //    else return "No";
        //}

        public String Risk(int rate, String LS)
        {
            if (LS == "No") return "//";
            else if (rate <= 0.25 && LS == "Yes") return "No Risk";
            else if ((rate > 0.25 && rate < 0.5) && LS == "Yes") return "Low Risk";
            else if ((rate >= 0.5 && rate < 0.75) && LS == "Yes") return "Risky";
            else if (rate >= 0.75  && LS == "Yes") return "High Risk";
            else return "//";
        }

        public int Count1()
        {
            var num = _context.Finance.Where(x=>x.Type.Contains("Personal")).Count();
            return num;
        }
        public int Count2()
        {
            var num = _context.Finance.Where(x => x.Type.Contains("Education")).Count();
            return num;
        }
        public int Count3()
        {
            var num = _context.Finance.Where(x => x.Type.Contains("Business")).Count();
            return num;
        }

        public int Count4()
        {
            var num = _context.Finance.Where(x => x.final_decision==true).Count();
            return num;
        }

        public int Count5()
        {
            var num = _context.Finance.Where(x => x.final_decision == false).Count();
            return num;
        }

        public int Count6()
        {
            var num = _context.Finance.Where(x => x.Done == true).Count();
            return num;
        }

        public int Count7()
        {
            var num = _context.Finance.Where(x => x.Done == false).Count();
            return num;
        }

        private bool FinanceExists(int id)
        {
            return _context.Finance.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcelToDatabase.Models;

namespace ExcelToDatabase.Controllers
{
    public class Pay_EarningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pay_EarningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pay_Earning
        public async Task<IActionResult> Index()
        {
              return _context.Pay_Earning != null ? 
                          View(await _context.Pay_Earning.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pay_Earning'  is null.");
        }

        // GET: Pay_Earning/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pay_Earning == null)
            {
                return NotFound();
            }

            var pay_Earning = await _context.Pay_Earning
                .FirstOrDefaultAsync(m => m.EarningId == id);
            if (pay_Earning == null)
            {
                return NotFound();
            }

            return View(pay_Earning);
        }

        // GET: Pay_Earning/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pay_Earning/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EarningId,Earning,EarningDescription")] Pay_Earning pay_Earning)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pay_Earning);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pay_Earning);
        }

        // GET: Pay_Earning/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pay_Earning == null)
            {
                return NotFound();
            }

            var pay_Earning = await _context.Pay_Earning.FindAsync(id);
            if (pay_Earning == null)
            {
                return NotFound();
            }
            return View(pay_Earning);
        }

        // POST: Pay_Earning/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("EarningId,Earning,EarningDescription")] Pay_Earning pay_Earning)
        {
            if (id != pay_Earning.EarningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pay_Earning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pay_EarningExists((int?)pay_Earning.EarningId))
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
            return View(pay_Earning);
        }

        // GET: Pay_Earning/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pay_Earning == null)
            {
                return NotFound();
            }

            var pay_Earning = await _context.Pay_Earning
                .FirstOrDefaultAsync(m => m.EarningId == id);
            if (pay_Earning == null)
            {
                return NotFound();
            }

            return View(pay_Earning);
        }

        // POST: Pay_Earning/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Pay_Earning == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pay_Earning'  is null.");
            }
            var pay_Earning = await _context.Pay_Earning.FindAsync(id);
            if (pay_Earning != null)
            {
                _context.Pay_Earning.Remove(pay_Earning);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pay_EarningExists(int? id)
        {
          return (_context.Pay_Earning?.Any(e => e.EarningId == id)).GetValueOrDefault();
        }
    }
}

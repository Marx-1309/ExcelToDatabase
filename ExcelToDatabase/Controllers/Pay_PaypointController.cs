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
    public class Pay_PaypointController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pay_PaypointController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pay_Paypoint
        public async Task<IActionResult> Index()
        {
            var pp = await _context.Pay_Paypoint.ToListAsync();
              return _context.Pay_Paypoint != null ? 
                          View(await _context.Pay_Paypoint.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pay_Paypoint'  is null.");
        }

        // GET: Pay_Paypoint/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pay_Paypoint == null)
            {
                return NotFound();
            }

            var pay_Paypoint = await _context.Pay_Paypoint
                .FirstOrDefaultAsync(m => m.PayPointId == id);
            if (pay_Paypoint == null)
            {
                return NotFound();
            }

            return View(pay_Paypoint);
        }

        // GET: Pay_Paypoint/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pay_Paypoint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayPointId,PayPointCode,PayPointDescription")] Pay_Paypoint pay_Paypoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pay_Paypoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pay_Paypoint);
        }

        // GET: Pay_Paypoint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pay_Paypoint == null)
            {
                return NotFound();
            }

            var pay_Paypoint = await _context.Pay_Paypoint.FindAsync(id);
            if (pay_Paypoint == null)
            {
                return NotFound();
            }
            return View(pay_Paypoint);
        }

        // POST: Pay_Paypoint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PayPointId,PayPointCode,PayPointDescription")] Pay_Paypoint pay_Paypoint)
        {
            if (id != pay_Paypoint.PayPointId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pay_Paypoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pay_PaypointExists((int)pay_Paypoint.PayPointId))
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
            return View(pay_Paypoint);
        }

        // GET: Pay_Paypoint/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pay_Paypoint == null)
            {
                return NotFound();
            }

            var pay_Paypoint = await _context.Pay_Paypoint
                .FirstOrDefaultAsync(m => m.PayPointId == id);
            if (pay_Paypoint == null)
            {
                return NotFound();
            }

            return View(pay_Paypoint);
        }

        // POST: Pay_Paypoint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pay_Paypoint == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pay_Paypoint'  is null.");
            }
            var pay_Paypoint = await _context.Pay_Paypoint.FindAsync(id);
            if (pay_Paypoint != null)
            {
                _context.Pay_Paypoint.Remove(pay_Paypoint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pay_PaypointExists(int id)
        {
          return (_context.Pay_Paypoint?.Any(e => e.PayPointId == id)).GetValueOrDefault();
        }
    }
}

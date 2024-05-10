using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcelToDatabase.Models;
using System.Text.Json;
using BenchmarkDotNet.Running;


namespace ExcelToDatabase.Controllers
{
    public class Pay_AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pay_AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pay_Account
        public async Task<IActionResult> Index()
        {
            var linkedAccountsId = await _context.Pay_Accounts.Where(x => x.AccountId > 0).Select(r=>r.ACTINDX).ToListAsync();

            //var linkedGlAccounts = await _context.GL00100.Where(x => x.ACCATNUM == 10 && linkedAccountsId.Contains(x.ACTINDX)).ToListAsync();

            //var linkedGlAccounts = await _context.GL00100
            //.Where(gl => linkedAccountsId.Contains(gl.ACTINDX) && gl.ACCATNUM == 10)
            //.ToListAsync();

            //var linkedGlAccounts = await _context.GL00100
            //                            .Where(gl => linkedAccountsId.Contains(gl.ACTINDX) && gl.ACCATNUM == 10)
            //                            .GroupBy(gl => gl.ACTDESCR)
            //                            .Select(group => group.First())
            //                            .ToListAsync();

            List<GL00100> gL00100sList = new List<GL00100>();
            gL00100sList.Clear();
            foreach (var gl in linkedAccountsId)
            {
                var glrecord = await _context.GL00100.Where(r => r.ACTINDX == gl).FirstOrDefaultAsync();
                 gL00100sList.Add(glrecord);
            }
            
            var Cb = gL00100sList;

            var allGlAccounts = await _context.GL00100.Where(x => x.ACCATNUM == 10 ).ToListAsync();

            var payPoint  = await _context.Pay_Paypoint.ToListAsync();
            var earnings = await _context.Pay_Earning.ToListAsync();

            ViewBag.AllGLAccountsDd = allGlAccounts;


            return View(new AccountVm
            {
                GL00100s = gL00100sList.Select(account => new GL00100
                {
                    AccountId = account.AccountId,
                    ACTINDX = account.ACTINDX,
                    ACCATNUM = account.ACCATNUM,
                    ACTDESCR = account.ACTDESCR?.Trim() 
                }).ToList(),
                PayPaypoints = payPoint.Select(paypoint => new Pay_Paypoint
                {
                    PayPointId = paypoint.PayPointId,
                    PayPointCode = paypoint.PayPointCode,
                    PayPointDescription = paypoint.PayPointDescription?.Trim() 
                }).ToList(),
                PayEarnings = earnings.Select(earning => new Pay_Earning
                {
                    EarningId = earning.EarningId,
                    //EarningDescription = earning.EarningDescription?.Trim(),
                    Earning = earning.Earning?.Trim() 
                }).ToList()
            });


            //return _context.Pay_Account != null ? 
            //              View(await _context.Pay_Account.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Pay_Account'  is null.");
        }

        // GET: /pay_Account/Details?id=123
        [HttpGet]
        public JsonResult Details(int id)
        {
            if (id == null)
            {
                return Json(false,"Please provide a value for the id");
            }
            try
            {
                Pay_Account accountRecord =  _context.Pay_Accounts.FirstAsync(m => m.ACTINDX == id).GetAwaiter().GetResult();

                var anonymousRec = new
                {
                    ACTDESCR = _context.GL00100
                    .Where(r => r.ACTINDX == accountRecord.ACTINDX)
                    .Select(r => r.ACTDESCR)
                    .FirstOrDefault()?.ToString().Trim(),

                    PayPointDescription = _context.Pay_Paypoint
                    .Where(r => r.PayPointId == accountRecord.PayPointId)
                    .Select(r => r.PayPointDescription)
                    .FirstOrDefault()?.ToString().Trim(),

                    Earning = _context.Pay_Earning
                    .Where(r => r.EarningId == accountRecord.EarningId)
                    .Select(r => r.Earning)
                    .FirstOrDefault()?.ToString().Trim(),

                    // Adding IDs and descriptions
                    ACTINDX = accountRecord.ACTINDX,
                    PayPointId = accountRecord.PayPointId,
                    EarningId = accountRecord.EarningId,
                    DateCreated = accountRecord?.DateCreated
                };



                if (anonymousRec == null)
                {
                    return Json(false, $"Account :{anonymousRec.ACTDESCR} , has no record");
                }

                return Json(anonymousRec);
            }
            catch (Exception ex) 
            {
                return Json($"{ex.StackTrace}");
            }
        }

        public JsonResult AccountListData(int id)
        {
            if (id == 1)
            {
                #region oldCode
                //List<GL00100> glAccounts = _context.GL00100.Where(r=>r.ACCATNUM==10).ToList();

                //if (glAccounts == null || !glAccounts.Any())
                //{
                //    return Json(false, $"GL00100 Account has no records");
                //}

                //foreach (var account in glAccounts)
                //{
                //    account.ACTDESCR = account.ACTDESCR?.Trim();
                //}
                #endregion
                List<SelectListItem> glAccounts = (from account in this._context.GL00100.Where(r=>r.ACCATNUM ==10)
                                                   select new SelectListItem
                                                    {
                                                        Value = account.ACTINDX.ToString(),
                                                        Text = account.ACTDESCR,
                                                    }).ToList();
                return Json(glAccounts);
            }
            if (id == 2)
            {
                #region oldCode
                //List<Pay_Paypoint> paypoints = _context.Pay_Paypoint.ToList();

                //if (paypoints == null || !paypoints.Any())
                //{
                //    return Json(false, $"Pay_Paypoint has no records");
                //}

                //foreach (var paypoint in paypoints)
                //{
                //    paypoint.PayPointCode = paypoint.PayPointCode?.Trim();
                //}
                #endregion
                List<SelectListItem> paypoints = (from paypoint in this._context.Pay_Paypoint
                                                   select new SelectListItem
                                                   {
                                                       Value = paypoint.PayPointCode,
                                                       Text = paypoint.PayPointDescription,
                                                   }).ToList();
                return Json(paypoints);
            }
            if (id == 3)
            {
                #region oldCode
                //List<Pay_Earning> earnings = _context.Pay_Earning.ToList();

                //if (earnings == null || !earnings.Any())
                //{
                //    return Json(false, $"Pay_Earning has no records");
                //}

                //foreach (var earning in earnings)
                //{
                //    earning.Earning = earning.Earning;
                //}
                #endregion
                List<SelectListItem> earnings = (from earning in this._context.Pay_Earning
                                                  select new SelectListItem
                                                  {
                                                      Value = earning.EarningId.ToString(),
                                                      Text = earning.Earning,
                                                  }).ToList();

                return Json(earnings);
            }
            return Json("Something went wrong while retrieving data from the server ");

        }


        // GET: Pay_Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pay_Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(Pay_Account record)
        {
            try
            {
                if (record != null)
                {
                    record.DateCreated =  DateTime.UtcNow.Date;
                    var existigRecord =  _context.Pay_Accounts.Where(r=>r.EarningId == record.EarningId 
                    && r.ACTINDX == record.ACTINDX 
                    && r.PayPointId == record.PayPointId).ToListAsync().GetAwaiter().GetResult();
                    if(existigRecord.Count> 0)
                    {
                        return Json(false,"An account with same configurations exist");
                    }

                    //record.DateCreated = DateTime.Today;
                    _context.Add(record);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                    TempData["success"] = "Success Upload";
                    return Json("Record added success");
                }
                return Json("Unable to save , record is empty");
            }
            catch(Exception ex)
            {
                return Json(ex.InnerException);
            }
            
        }

        // POST: Pay_Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit(Pay_Account model)
        {
                try
                {
                    var existigRecord = _context.Pay_Accounts.Where(r => r.EarningId == model.EarningId
                    && r.ACTINDX == model.ACTINDX
                    && r.PayPointId == model.PayPointId).ToListAsync().GetAwaiter().GetResult();
                    if (existigRecord.Count > 0)
                    {
                        return Json(false, "An account with same configurations exist");
                    }

                    _context.Pay_Accounts.Update(model);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();

                return Json("Record update success");
 
                }
                catch (Exception ex)
                {
                    return Json(false,$"Unable to save,error : {ex.InnerException.Message.ToString()}");
                }
            return Json("Please fill all fields");
        }

        [HttpGet]
        public JsonResult GetAccountId(Pay_Account account)
        {
            if (account != null)
            {
                var  accRecord = _context.Pay_Accounts.AsNoTracking()
                    .Where(x=>x.PayPointId == account.PayPointId && x.EarningId == account.EarningId && x.ACTINDX == account.ACTINDX)
                    .FirstOrDefault();
                if (accRecord != null)
                {
                    return Json(accRecord.AccountId);
                }
            }
            return Json("");
        }

        // GET: Pay_Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pay_Accounts == null)
            {
                return NotFound();
            }

            var pay_Account = await _context.Pay_Accounts
                .FirstOrDefaultAsync(m => m.ACTINDX == id);
            if (pay_Account == null)
            {
                return NotFound();
            }

            return View(pay_Account);
        }

        // POST: Pay_Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pay_Accounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pay_Account'  is null.");
            }
            var pay_Account = await _context.Pay_Accounts.FindAsync(id);
            if (pay_Account != null)
            {
                _context.Pay_Accounts.Remove(pay_Account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pay_AccountExists(int id)
        {
          return (_context.Pay_Accounts?.Any(e => e.ACTINDX == id)).GetValueOrDefault();
        }
    }
}

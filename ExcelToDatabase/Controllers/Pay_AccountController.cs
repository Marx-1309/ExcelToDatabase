using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelToDatabase.Models;
using System.Text.Json;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Filters;
using HrGpIntegration.Custom_SelectLists;
using Microsoft.AspNetCore.Authorization;



namespace ExcelToDatabase.Controllers
{
    [Authorize]
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

            ViewBag.AllGLAccountsDd = allGlAccounts.OrderBy(r=>r.ACTDESCR);
            ViewBag.AllPayPointsDd = payPoint.OrderBy(r => r.PayPointDescription);
            ViewBag.AllEarningsDd = earnings.OrderBy(r => r.Earning);

            var accountsvm = await _context.Pay_Accounts.ToListAsync();

            List<AccountVm> accountVms = new List<AccountVm>();
            GL00100 accountGl = new GL00100();

            foreach (var vm in accountsvm)
            {
                AccountVm acc = new AccountVm();

                acc.AccountId = (int)vm.AccountId;
                acc.PayPointId = (int)vm.PayPointId;
                acc.PayPointCode = _context.Pay_Paypoint.Where(p => p.PayPointId == vm.PayPointId).Select(r => r.PayPointCode).FirstOrDefault();
                acc.PayPointName = _context.Pay_Paypoint.Where(p=>p.PayPointId == vm.PayPointId).Select(r=>r.PayPointDescription).FirstOrDefault();

                acc.ACTINDX = (int)vm.ACTINDX;
                acc.GlAccountName = _context.GL00100.Where(p => p.ACTINDX == vm.ACTINDX).Select(r => r.ACTDESCR).FirstOrDefault();

                acc.EarningId = (int)vm.EarningId;
                acc.EarningName = _context.Pay_Earning.Where(e=>e.EarningId == vm.EarningId).Select(r=>r.Earning).FirstOrDefault();

                accountVms.Add(acc);
            }
            

            return View(accountVms);


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
                Pay_Account accountRecord = _context.Pay_Accounts.FirstOrDefault(m => m.AccountId == id);

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
                List<CustomSelectListItem> glAccounts = (from account in this._context.GL00100
                                                   where account.ACCATNUM == 10
                                                   orderby account.ACTDESCR
                                                   select new CustomSelectListItem
                                                   {
                                                       Value = account.ACTINDX.ToString(),
                                                       Text = account.ACTDESCR,
                                                   }).ToList();
                return Json(glAccounts);
            }
            if (id == 2)
            {
                List<CustomSelectListItem> paypoints = (from paypoint in this._context.Pay_Paypoint
                                                  orderby paypoint.PayPointDescription
                                                  select new CustomSelectListItem
                                                  {
                                                      Value = paypoint.PayPointId.ToString(),
                                                      Text = paypoint.PayPointDescription,
                                                  }).ToList();
                return Json(paypoints);
            }
            if (id == 3)
            {
                List<CustomSelectListItem> earnings = (from earning in this._context.Pay_Earning
                                                 orderby earning.Earning
                                                 select new CustomSelectListItem
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
                        return Json("A record with same configurations exist!");
                    }

                    //record.DateCreated = DateTime.Today;
                    _context.Add(record);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                    TempData["success"] = "Success Upload";
                    return Json("");
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
                    && r.PayPointId == model.PayPointId).FirstOrDefaultAsync().GetAwaiter().GetResult();

                    //if (existigRecord.AccountId == model.AccountId)
                    //{
                    //    return Json("You did not make any changes");
                    //}
                    if (existigRecord is not null)
                    {
                        return Json("An account with same configurations exist");
                    }

                    _context.Pay_Accounts.Update(model);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();

                return Json("");
 
                }
                catch (Exception ex)
                {
                    return Json($"Unable to save,error : {ex.InnerException.Message.ToString()}");
                }
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
        public JsonResult DeleteAccount(int? id)
        {
            if (id !=0)
            {
                try
                {
                    var record = _context.Pay_Accounts
                    .FirstOrDefaultAsync(m => m.AccountId == id).GetAwaiter().GetResult();
                    if (record != null)
                    {
                        _context.Remove(record);
                        _context.SaveChangesAsync().GetAwaiter().GetResult();
                        return Json("");
                    }
                    return Json("Record Not found!");
                }
                catch (Exception ex)
                {
                    return Json(false, $"{ex.InnerException}");
                }
            }return Json(false, "The id is empty!");
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

        public async Task<JsonResult> RefreshAccountsConfigDropDown(AccountVm vm)
        {
            await Task.Delay(3000);
            try
            {
                

                if (vm.ACTINDX > 0 && vm.EarningId > 0)
                {
                    //Find an account with these matching ACTINDX,EarningId,PayPointId
                    //Then remove the PayPoint of the account with the matching ACTINDX & EarningId from the dropdownlist
                    var allLinkedAccountsPaypoints = _context.Pay_Accounts.AsNoTracking()
                                    .Where(r => r.ACTINDX == vm.ACTINDX && r.EarningId == vm.EarningId)
                                    .Select(r => r.PayPointId)
                                    .ToList();

                    var payPoints = _context.Pay_Paypoint.Where(r => !allLinkedAccountsPaypoints.Contains(r.PayPointId))
                                                       //.Select(r => r.PayPointId)
                                                       .ToList();
                    //populate pay points dropdown with these pay points 
                    List<CustomSelectListItem> payPointsList = (from pp in payPoints 
                                                          orderby pp.PayPointDescription
                                                 select new CustomSelectListItem
                                                 {
                                                     ModelName = "Pay_Paypoint",
                                                    Value = pp.PayPointId.ToString(),
                                                    Text = pp.PayPointDescription,
                                                }).ToList() ;
                    return Json(payPointsList);
                }

                if (vm.EarningId > 0 && vm.PayPointId > 0)
                {
                    var allLinkedAccountsAccounts = _context.Pay_Accounts.AsNoTracking()
                                                .Where(r => r.EarningId == vm.EarningId && r.PayPointId == vm.PayPointId)
                                                .Select(r => r.ACTINDX)
                                                .ToList();

                    var gL00100s = _context.GL00100.Where(r => !allLinkedAccountsAccounts.Contains(r.ACTINDX))
                                                       //.Select(r => r.PayPointId)
                                                       .ToList();

                    List<CustomSelectListItem> glsAccountsList = (from gls in gL00100s
                                                            orderby gls.ACTDESCR
                                                            where gls.ACCATNUM == 10
                                                 select new CustomSelectListItem
                                                 {
                                                     ModelName = "GL00100",
                                                     Value = gls.ACTINDX.ToString(),
                                                     Text = gls.ACTDESCR,
                                                 }).ToList();
                    return Json(glsAccountsList);
                }

                if (vm.PayPointId > 0 && vm.ACTINDX > 0)
                {
                    var allLinkedAccountsEarnings = _context.Pay_Accounts.AsNoTracking()
                                    .Where(r => r.PayPointId == vm.PayPointId && r.ACTINDX == vm.ACTINDX)
                                    .Select(r => r.EarningId)
                                    .ToList();

                    var earnings = _context.Pay_Earning.Where(r => !allLinkedAccountsEarnings.Contains(r.EarningId))
                                                       //.Select(r => r.PayPointId)
                                                       .ToList();
                    //populate pay points dropdown with these pay points 
                    List<CustomSelectListItem> earningList = (from earn in earnings
                                                        orderby earn.Earning
                                                 select new CustomSelectListItem
                                                 {
                                                     ModelName = "Pay_Earning",
                                                     Value = earn.EarningId.ToString(),
                                                     Text = earn.Earning,
                                                 }).ToList();
                    return Json(earningList);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(false);
        }
    }
}

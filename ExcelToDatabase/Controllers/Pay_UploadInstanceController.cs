using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcelToDatabase.Models;
using Newtonsoft.Json;
using System.Text;
using ExcelDataReader;
using System.IO;

namespace ExcelToDatabase.Controllers
{
    public class Pay_UploadInstanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pay_UploadInstanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pay_InstancePayroll
        public async Task<IActionResult> Index()
        {
            var months = await _context.Pay_Month.ToListAsync();
            var i = await _context.Pay_UploadInstance.Include(m => m.Month).ToListAsync();

            var ii = await _context.Pay_UploadInstance.Include(m=>m.Month).ToListAsync();
            var  instanceMonthVm = new InstanceMonthVm
            {
                uploadInstances = ii,
                months = months
            }; 
            return View( instanceMonthVm);
        }

        // GET: Pay_InstancePayroll/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pay_UploadInstance == null)
            {
                return NotFound();
            }

            var pay_InstancePayroll = await _context.Pay_UploadInstance
                .FirstOrDefaultAsync(m => m.UploadInstanceID == id);
            if (pay_InstancePayroll == null)
            {
                return NotFound();
            }

            return View(pay_InstancePayroll);
        }

        // GET: Pay_InstancePayroll/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pay_InstancePayroll/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public async Task<ActionResult> Create(Pay_UploadInstance uploadInstance, IFormFile FileUpload1, IFormFile FileUpload2)
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //    try
        //    {
        //        bool filesProcessed = false; // Flag to track if any files were processed

        //        if ((FileUpload1 != null))
        //        {


        //                var fileName = FileUpload2.FileName; // Assuming file.FileName is a string
        //                var fileExtension = Path.GetExtension(fileName);
        //                var xlsExt = ".xls";
        //                var xlsxExt = ".xlsx";

        //                if (fileExtension != xlsExt && fileExtension != xlsxExt)
        //                {
        //                    ViewBag.Message = "Please upload file with the correct extension ex. xlsx or xls!";
        //                    return View();
        //                }

        //                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

        //                if (!Directory.Exists(uploadsFolder))
        //                {
        //                    Directory.CreateDirectory(uploadsFolder);
        //                }

        //                var filePath = Path.Combine(uploadsFolder, FileUpload2.FileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await FileUpload2.CopyToAsync(stream);
        //                }

        //                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    //List<Pay_VIP> Pay_VIPsList = new List<Pay_VIP>();

        //                        bool isHeaderSkipped = false;

        //                        var items = await _context.Pay_VIP.AsTracking().Where(c => c.EmployeeCode > 0).ToListAsync();
        //                        if (items.Any())
        //                        {
        //                            _context.Pay_VIP.RemoveRange(items);
        //                            await _context.SaveChangesAsync();
        //                        }

        //                        while (reader.Read())
        //                        {
        //                            if (!isHeaderSkipped)
        //                            {
        //                                isHeaderSkipped = true;
        //                                continue;
        //                            }

        //                            Pay_VIP pay_VipItem = new Pay_VIP();

        //                            if (_context.Pay_VIP.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
        //                            {
        //                                ViewBag.Message = "A record with the same ID already exists";
        //                                continue; // Skip saving this record and move to the next one
        //                            }
        //                            #region properties populate
        //                            pay_VipItem.EmployeeCode = Convert.ToInt32(reader.GetValue(0));
        //                            pay_VipItem.Surname = reader.GetValue(1)?.ToString();
        //                            pay_VipItem.FullNames = reader.GetValue(2)?.ToString();
        //                            pay_VipItem.PayPoint = Convert.ToInt32(reader.GetValue(3));
        //                            pay_VipItem.EDSALARY = Convert.ToDecimal(reader.GetValue(4));
        //                            pay_VipItem.EDANNUALBONUS = Convert.ToDecimal(reader.GetValue(5));
        //                            pay_VipItem.EDSUBSIDYNON_TAX = Convert.ToDecimal(reader.GetValue(6));
        //                            pay_VipItem.EDSUBSIDYTAXABLE = Convert.ToDecimal(reader.GetValue(7));
        //                            pay_VipItem.EDCARALLTAXABLE = Convert.ToDecimal(reader.GetValue(8));
        //                            pay_VipItem.EDCARALLNONTAX = Convert.ToDecimal(reader.GetValue(9));
        //                            pay_VipItem.EDOVERTIME1_5 = Convert.ToDecimal(reader.GetValue(10));
        //                            pay_VipItem.EDOVERTIME2_0 = Convert.ToDecimal(reader.GetValue(11));
        //                            pay_VipItem.EDLEAVEPAIDOUT = Convert.ToDecimal(reader.GetValue(12));
        //                            pay_VipItem.EDUNPAIDLEAVE = Convert.ToDecimal(reader.GetValue(13));
        //                            pay_VipItem.EDBACKPAYSALARY = Convert.ToDecimal(reader.GetValue(14));
        //                            pay_VipItem.EDACTINGALL = Convert.ToDecimal(reader.GetValue(15));
        //                            pay_VipItem.EDTELEPHONEALL = Convert.ToDecimal(reader.GetValue(16));
        //                            pay_VipItem.EDFURNITUREALL = Convert.ToDecimal(reader.GetValue(17));
        //                            pay_VipItem.EDWATER_ELEC = Convert.ToDecimal(reader.GetValue(18));
        //                            pay_VipItem.EDTRAVELALL = Convert.ToDecimal(reader.GetValue(19));
        //                            pay_VipItem.EDHOUSING = Convert.ToDecimal(reader.GetValue(20));
        //                            pay_VipItem.EDREFUNDS = Convert.ToDecimal(reader.GetValue(21));
        //                            pay_VipItem.EDCARRUNNINGCOS = Convert.ToDecimal(reader.GetValue(22));
        //                            pay_VipItem.EDRENTALALLOWANC = Convert.ToDecimal(reader.GetValue(23));
        //                            pay_VipItem.EDTRANSPORTALLOW = Convert.ToDecimal(reader.GetValue(24));
        //                            pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(25));
        //                            pay_VipItem.EDS_TCLAIM = Convert.ToDecimal(reader.GetValue(26));
        //                            pay_VipItem.EDSEPARATIONGRAT = Convert.ToDecimal(reader.GetValue(27));
        //                            pay_VipItem.EDREMOTENESSALLO = Convert.ToDecimal(reader.GetValue(28));
        //                            pay_VipItem.EDREMOTENESSALL = Convert.ToDecimal(reader.GetValue(29));
        //                            pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(30));
        //                            pay_VipItem.EDALLOBACKPAY = Convert.ToDecimal(reader.GetValue(31));
        //                            pay_VipItem.EDBACKPAYNONTAX = Convert.ToDecimal(reader.GetValue(32));
        //                            pay_VipItem.EDBACKPAYTAXABLE = Convert.ToDecimal(reader.GetValue(33));
        //                            pay_VipItem.EDBACKPAYBONUS = Convert.ToDecimal(reader.GetValue(34));
        //                            pay_VipItem.EDFIXEDOVERTIME = Convert.ToDecimal(reader.GetValue(35));
        //                            pay_VipItem.EDT_SHIRTREFUND = Convert.ToDecimal(reader.GetValue(36));
        //                            pay_VipItem.EDOVERTIMBACKPAY = Convert.ToDecimal(reader.GetValue(37));
        //                            pay_VipItem.EDHOUSALLBACKPAY = Convert.ToDecimal(reader.GetValue(38));
        //                            pay_VipItem.EDTRANSBACKPAY = Convert.ToDecimal(reader.GetValue(39));
        //                            pay_VipItem.EDACTINGBACKPAY = Convert.ToDecimal(reader.GetValue(40));
        //                            pay_VipItem.EDCASHBBACKPAY = Convert.ToDecimal(reader.GetValue(41));
        //                            pay_VipItem.EDREMOTENESSBP = Convert.ToDecimal(reader.GetValue(42));
        //                            pay_VipItem.EDBACKPAYSUBSIDY = Convert.ToDecimal(reader.GetValue(43));
        //                            pay_VipItem.EDHOUSINGNONTAX = Convert.ToDecimal(reader.GetValue(44));
        //                            pay_VipItem.EDHOUSINGTAXABLE = Convert.ToDecimal(reader.GetValue(45));
        //                            pay_VipItem.EDBPMANNONTAX = Convert.ToDecimal(reader.GetValue(46));
        //                            pay_VipItem.EDBPMANTAXABLE = Convert.ToDecimal(reader.GetValue(47));
        //                            pay_VipItem.EDCARALLBPTAX = Convert.ToDecimal(reader.GetValue(48));
        //                            pay_VipItem.EDCARALLBPN_TAX = Convert.ToDecimal(reader.GetValue(49));
        //                            pay_VipItem.EDRUNCOSTBP = Convert.ToDecimal(reader.GetValue(50));
        //                            #endregion

        //                            _context.Add(pay_VipItem);
        //                            await _context.SaveChangesAsync();
        //                            //Pay_VIPsList.Add(pay_VipItem);
        //                        }
        //                    }

        //                    // Flag that files were processed
        //                    filesProcessed = true;
        //                }




        //        // Add the uploadInstance to the context
        //        _context.Pay_UploadInstance.Add(uploadInstance);
        //        await _context.SaveChangesAsync();

        //        return Json(nameof(Index)); // Redirect to the Index action after successful creation
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        public async Task<IActionResult> Upload(IFormFile FileUpload2 , Pay_UploadInstance savedRecord)
        {
            if (FileUpload2 != null)
            {
                var fileName = FileUpload2.FileName;
                var fileExtension = Path.GetExtension(fileName);
                var xlsExt = ".xls";
                var xlsxExt = ".xlsx";

                if (fileExtension != xlsExt && fileExtension != xlsxExt)
                {
                    ViewBag.Message = "Please upload file with the correct extension ex. xlsx or xls!";
                    return View();
                }

                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, FileUpload2.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FileUpload2.CopyToAsync(stream);
                }

                // Process the uploaded Excel file
                await ProcessExcelFile(filePath);
            }

            return Ok();
        }

        private async Task ProcessExcelFile(string filePath)
        {
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    bool isMyHeaderSkipped = false;

                    var items = await _context.Pay_Deduction.AsTracking().Where(c => c.EmployeeCode > 0).ToListAsync();
                    if (items.Any())
                    {
                        _context.Pay_Deduction.RemoveRange(items);
                        await _context.SaveChangesAsync();
                    }

                    while (reader.Read())
                    {
                        if (!isMyHeaderSkipped)
                        {
                            isMyHeaderSkipped = true;
                            continue;
                        }

                        Pay_Deductions pay_DeductionItem = new Pay_Deductions();

                        //if (_context.Pay_VIP.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                        //{
                        //    ViewBag.Message = "A record with the same ID already exists";
                        //    continue; // Skip saving this record and move to the next one
                        //}

                        #region properties populate
                        pay_DeductionItem.EmployeeCode = Convert.ToInt32(reader.GetValue(0));
                        pay_DeductionItem.Surname = reader.GetValue(1)?.ToString();
                        pay_DeductionItem.FullNames = reader.GetValue(2)?.ToString();
                        pay_DeductionItem.PayPoint = Convert.ToInt32(reader.GetValue(3));
                        pay_DeductionItem.DdSocialSecurity = Convert.ToDecimal(reader.GetValue(4));
                        pay_DeductionItem.DdPension = Convert.ToDecimal(reader.GetValue(5));
                        pay_DeductionItem.CcSocialSecurity = Convert.ToDecimal(reader.GetValue(6));
                        pay_DeductionItem.CcPension = Convert.ToDecimal(reader.GetValue(7));
                        #endregion

                        _context.Add(pay_DeductionItem);
                        await _context.SaveChangesAsync();
                    }
                }
                while (reader.NextResult());
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(Pay_UploadInstance uploadInstance, IFormFile FileUpload1, IFormFile FileUpload2)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                if (uploadInstance.MonthId != 0)
                {
                    // Add the new record to the context
                    var addedRecord = _context.Pay_UploadInstance.Add(new Pay_UploadInstance
                    {
                        MonthId = uploadInstance.MonthId,
                        Year = uploadInstance.Year,
                        Site = "ORC"
                    }).Entity;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Return the added record with its data and ID populated
                    Pay_UploadInstance savedRecord = addedRecord;

                    if (FileUpload1 != null)
                    {

                        var fileName = FileUpload1.FileName; // Assuming file.FileName is a string
                        var fileExtension = Path.GetExtension(fileName);
                        var xlsExt = ".xls";
                        var xlsxExt = ".xlsx";
                        var uploadCount = 0;

                        if (fileExtension != xlsExt && fileExtension != xlsxExt)
                        {
                            ViewBag.Message = "Please upload file with the correct extension ex. xlsx or xls!";
                            return View();
                        }

                        var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var filePath = Path.Combine(uploadsFolder, FileUpload1.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await FileUpload1.CopyToAsync(stream);
                        }

                        {
                            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                List<Pay_VIP> Pay_VIPsList = new List<Pay_VIP>();

                                do
                                {
                                    bool isHeaderSkipped = false;

                                    var items = await _context.Pay_VIP.Where(c => c.EmployeeCode > 0).ToListAsync();
                                    if (items.Count>0)
                                    {
                                        _context.Pay_VIP.RemoveRange(items);
                                        await _context.SaveChangesAsync();
                                    }

                                    while (reader.Read())
                                    {
                                        if (!isHeaderSkipped)
                                        {
                                            isHeaderSkipped = true;
                                            continue;
                                        }
                                        if (Convert.ToInt32(reader.GetValue(0)) == 0 && uploadCount > 0)
                                        {
                                            TempData["success"] = "Success Upload";
                                            await Upload(FileUpload2, savedRecord);
                                            return View(Pay_VIPsList);

                                        }

                                        //Student s = new Student();
                                        //s.Name = reader.GetValue(1).ToString();
                                        //s.Marks = Convert.ToInt32(reader.GetValue(2).ToString());
                                        Pay_VIP pay_VipItem = new Pay_VIP();

                                        if (_context.Pay_VIP.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                                        {
                                            ViewBag.Message = "A record with the same ID already exist";

                                        }



                                        pay_VipItem.EmployeeCode = Convert.ToInt32(reader.GetValue(0));
                                        pay_VipItem.Surname = reader.GetValue(1)?.ToString();
                                        pay_VipItem.FullNames = reader.GetValue(2)?.ToString();
                                        pay_VipItem.PayPoint = Convert.ToInt32(reader.GetValue(3));
                                        pay_VipItem.EDSALARY = Convert.ToDecimal(reader.GetValue(4));
                                        pay_VipItem.EDANNUALBONUS = Convert.ToDecimal(reader.GetValue(5));
                                        pay_VipItem.EDSUBSIDYNON_TAX = Convert.ToDecimal(reader.GetValue(6));
                                        pay_VipItem.EDSUBSIDYTAXABLE = Convert.ToDecimal(reader.GetValue(7));
                                        pay_VipItem.EDCARALLTAXABLE = Convert.ToDecimal(reader.GetValue(8));
                                        pay_VipItem.EDCARALLNONTAX = Convert.ToDecimal(reader.GetValue(9));
                                        pay_VipItem.EDOVERTIME1_5 = Convert.ToDecimal(reader.GetValue(10));
                                        pay_VipItem.EDOVERTIME2_0 = Convert.ToDecimal(reader.GetValue(11));
                                        pay_VipItem.EDLEAVEPAIDOUT = Convert.ToDecimal(reader.GetValue(12));
                                        pay_VipItem.EDUNPAIDLEAVE = Convert.ToDecimal(reader.GetValue(13));
                                        pay_VipItem.EDBACKPAYSALARY = Convert.ToDecimal(reader.GetValue(14));
                                        pay_VipItem.EDACTINGALL = Convert.ToDecimal(reader.GetValue(15));
                                        pay_VipItem.EDTELEPHONEALL = Convert.ToDecimal(reader.GetValue(16));
                                        pay_VipItem.EDFURNITUREALL = Convert.ToDecimal(reader.GetValue(17));
                                        pay_VipItem.EDWATER_ELEC = Convert.ToDecimal(reader.GetValue(18));
                                        pay_VipItem.EDTRAVELALL = Convert.ToDecimal(reader.GetValue(19));
                                        pay_VipItem.EDHOUSING = Convert.ToDecimal(reader.GetValue(20));
                                        pay_VipItem.EDREFUNDS = Convert.ToDecimal(reader.GetValue(21));
                                        pay_VipItem.EDCARRUNNINGCOS = Convert.ToDecimal(reader.GetValue(22));
                                        pay_VipItem.EDRENTALALLOWANC = Convert.ToDecimal(reader.GetValue(23));
                                        pay_VipItem.EDTRANSPORTALLOW = Convert.ToDecimal(reader.GetValue(24));
                                        pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(25));
                                        pay_VipItem.EDS_TCLAIM = Convert.ToDecimal(reader.GetValue(26));
                                        pay_VipItem.EDSEPARATIONGRAT = Convert.ToDecimal(reader.GetValue(27));
                                        pay_VipItem.EDREMOTENESSALLO = Convert.ToDecimal(reader.GetValue(28));
                                        pay_VipItem.EDREMOTENESSALL = Convert.ToDecimal(reader.GetValue(29));
                                        pay_VipItem.EDCASHBONUS = Convert.ToDecimal(reader.GetValue(30));
                                        pay_VipItem.EDALLOBACKPAY = Convert.ToDecimal(reader.GetValue(31));
                                        pay_VipItem.EDBACKPAYNONTAX = Convert.ToDecimal(reader.GetValue(32));
                                        pay_VipItem.EDBACKPAYTAXABLE = Convert.ToDecimal(reader.GetValue(33));
                                        pay_VipItem.EDBACKPAYBONUS = Convert.ToDecimal(reader.GetValue(34));
                                        pay_VipItem.EDFIXEDOVERTIME = Convert.ToDecimal(reader.GetValue(35));
                                        pay_VipItem.EDT_SHIRTREFUND = Convert.ToDecimal(reader.GetValue(36));
                                        pay_VipItem.EDOVERTIMBACKPAY = Convert.ToDecimal(reader.GetValue(37));
                                        pay_VipItem.EDHOUSALLBACKPAY = Convert.ToDecimal(reader.GetValue(38));
                                        pay_VipItem.EDTRANSBACKPAY = Convert.ToDecimal(reader.GetValue(39));
                                        pay_VipItem.EDACTINGBACKPAY = Convert.ToDecimal(reader.GetValue(40));
                                        pay_VipItem.EDCASHBBACKPAY = Convert.ToDecimal(reader.GetValue(41));
                                        pay_VipItem.EDREMOTENESSBP = Convert.ToDecimal(reader.GetValue(42));
                                        pay_VipItem.EDBACKPAYSUBSIDY = Convert.ToDecimal(reader.GetValue(43));
                                        pay_VipItem.EDHOUSINGNONTAX = Convert.ToDecimal(reader.GetValue(44));
                                        pay_VipItem.EDHOUSINGTAXABLE = Convert.ToDecimal(reader.GetValue(45));
                                        pay_VipItem.EDBPMANNONTAX = Convert.ToDecimal(reader.GetValue(46));
                                        pay_VipItem.EDBPMANTAXABLE = Convert.ToDecimal(reader.GetValue(47));
                                        pay_VipItem.EDCARALLBPTAX = Convert.ToDecimal(reader.GetValue(48));
                                        pay_VipItem.EDCARALLBPN_TAX = Convert.ToDecimal(reader.GetValue(49));
                                        pay_VipItem.EDRUNCOSTBP = Convert.ToDecimal(reader.GetValue(50));
                                        pay_VipItem.UploadInstanceId = savedRecord.UploadInstanceID;

                                        _context.Add(pay_VipItem);
                                        await _context.SaveChangesAsync();
                                        Pay_VIPsList.Add(pay_VipItem);
                                        uploadCount++;
                                    }
                                } while (reader.NextResult());
                            }
                        }

                    }
                }


            }
            catch (Exception ex)
            {

            }


            //// Add the uploadInstance to the context
            //_context.Pay_UploadInstance.Add(uploadInstance);
            //await _context.SaveChangesAsync();
            return Json(nameof(Index)); // Redirect to the Index action after successful creation
        }


        [HttpGet]
        public async Task<IActionResult> GetIndividualDeductionsListByUploadInstanceId(int UploadInstanceID)
        {
            var items = await _context.Pay_Deduction.Where(m=>m.UploadInstanceId == UploadInstanceID).ToListAsync();
            return View(items);
        }


        [HttpGet]
        public async Task<IActionResult> GetIndividualEarningsListByUploadInstanceId(int Id)
        {
            var items = await _context.Pay_VIP.Where(m => m.UploadInstanceId == Id).ToListAsync();
            return View(items);
        }

        // GET: Pay_InstancePayroll/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pay_UploadInstance == null)
            {
                return NotFound();
            }

            var pay_InstancePayroll = await _context.Pay_UploadInstance.FindAsync(id);
            if (pay_InstancePayroll == null)
            {
                return NotFound();
            }
            return View(pay_InstancePayroll);
        }

        // POST: Pay_InstancePayroll/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstancePayrollId,MonthId,Year,Date,Site")] Pay_UploadInstance pay_InstancePayroll)
        {
            if (id != pay_InstancePayroll.UploadInstanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pay_InstancePayroll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pay_InstancePayrollExists((int)pay_InstancePayroll.UploadInstanceID))
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
            return View(pay_InstancePayroll);
        }

        // GET: Pay_InstancePayroll/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pay_UploadInstance == null)
            {
                return NotFound();
            }

            var pay_InstancePayroll = await _context.Pay_UploadInstance
                .FirstOrDefaultAsync(m => m.UploadInstanceID == id);
            if (pay_InstancePayroll == null)
            {
                return NotFound();
            }

            return View(pay_InstancePayroll);
        }

        // POST: Pay_InstancePayroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pay_UploadInstance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pay_InstancePayroll'  is null.");
            }
            var pay_InstancePayroll = await _context.Pay_UploadInstance.FindAsync(id);
            if (pay_InstancePayroll != null)
            {
                _context.Pay_UploadInstance.Remove(pay_InstancePayroll);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pay_InstancePayrollExists(int id)
        {
          return (_context.Pay_UploadInstance?.Any(e => e.UploadInstanceID == id)).GetValueOrDefault();
        }
    }
}

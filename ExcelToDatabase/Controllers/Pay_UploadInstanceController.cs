using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelToDatabase.Models;

using System.Text;
using ExcelDataReader;


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

            var ii = await _context.Pay_UploadInstance.Include(m => m.Month).ToListAsync();
            var instanceMonthVm = new InstanceMonthVm
            {
                uploadInstances = ii,
                months = months
            };
            return View(instanceMonthVm);
        }

        // GET: Pay_InstancePayroll/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pay_UploadInstance == null)
            {
                return NotFound();
            }

            var pay_InstancePayroll = await _context.Pay_UploadInstance.FirstOrDefaultAsync(m => m.UploadInstanceID == id);
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

        //POST: Pay_InstancePayroll/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult UploadEarningsDoc(IFormFile FileUpload1,Pay_UploadInstance savedRecord)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                bool filesProcessed = false; // Flag to track if any files were processed

                if ((FileUpload1 != null))
                {
                    try
                    {
                        var fileName = FileUpload1.FileName; // Assuming file.FileName is a string
                        var fileExtension = Path.GetExtension(fileName);
                        var xlsExt = ".xls";
                        var xlsxExt = ".xlsx";
                        var uploadCount = 0;

                        if (fileExtension != xlsExt && fileExtension != xlsxExt)
                        {
                            ViewBag["error"] = "Please upload file with the correct extension ex. xlsx or xls!";
                            return Json("Please upload file with the correct extension ex.xlsx or xls!");
                        }

                        var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\Earning_Docs\\";

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var filePath = Path.Combine(uploadsFolder, FileUpload1.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                             FileUpload1.CopyToAsync(stream).GetAwaiter().GetResult();
                        }

                        {
                            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                List<Pay_VIP> Pay_VIPsList = new List<Pay_VIP>();

                                do
                                {
                                    bool isHeaderSkipped = false;

                                    //Incase you want to keep excel earnings data for one instance at a time 
                                    //var items =  _context.Pay_VIP.Where(c => c.EmployeeCode > 0).ToListAsync().GetAwaiter().GetResult();
                                    //if (items.Count > 0)
                                    //{
                                    //    _context.Pay_VIP.RemoveRange(items);
                                    //     _context.SaveChangesAsync().GetAwaiter().GetResult();
                                    //}

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
                                            //await Upload(FileUpload2, savedRecord);
                                            return Json("Successfully uploaded file1");

                                        }

                                        Pay_VIP pay_VipItem = new Pay_VIP();

                                        //if (_context.Pay_VIP.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                                        //{
                                        //    ViewBag.Message = "A record with the same ID already exist";
                                        //    break;
                                        //}

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

                                        _context.AddAsync(pay_VipItem);
                                         _context.SaveChangesAsync().GetAwaiter().GetResult();
                                        Pay_VIPsList.Add(pay_VipItem);
                                        uploadCount++;
                                    }
                                } while (reader.NextResult());
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(ex.Message);
            }
            return Json("File1 upload success");
        }

        public JsonResult UploadDeductionsDoc(IFormFile FileUpload2, Pay_UploadInstance savedRecord)
        {
            if (FileUpload2 != null)
            {
                var fileName = FileUpload2.FileName; // Assuming file.FileName is a string
                var fileExtension = Path.GetExtension(fileName);
                var xlsExt = ".xls";
                var xlsxExt = ".xlsx";
                var uploadCount = 0;

                if (fileExtension != xlsExt && fileExtension != xlsxExt)
                {
                    ViewBag["error"] = "Please upload file with the correct extension ex. xlsx or xls!";
                    return Json("Please upload file with the correct extension ex.xlsx or xls!");
                }

                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\Deduction_Docs\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, FileUpload2.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FileUpload2.CopyToAsync(stream).GetAwaiter().GetResult();
                }

                {
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        List<Pay_Deductions> Pay_DeductionsList = new List<Pay_Deductions>();

                        do
                        {

                            bool isMyHeaderSkipped = false;

                            //Incase you want to keep excel deductions data for one instance at a time 
                            //var items = _context.Pay_Deduction.Where(c => c.EmployeeCode > 0).ToListAsync().GetAwaiter().GetResult();
                            //if (items.Any())
                            //{
                            //    _context.RemoveRange(items);
                            //    _context.SaveChangesAsync().GetAwaiter().GetResult();
                            //}

                            while (reader.Read())
                            {
                                if (!isMyHeaderSkipped)
                                {
                                    isMyHeaderSkipped = true;
                                    continue;
                                }
                                if (Convert.ToInt32(reader.GetValue(0)) == 0 && uploadCount > 0)
                                {
                                    TempData["success"] = "Success Upload";
                                    //await Upload(FileUpload2, savedRecord);
                                    return Json("Successfully uploaded file1");

                                }

                                Pay_Deductions pay_DeductionItem = new Pay_Deductions();

                                //if (_context.Pay_Deduction.Any(e => e.EmployeeCode == Convert.ToInt32(reader.GetValue(0))))
                                //{

                                //    ViewBag.Message = "A record with the same ID already exists";
                                //    break; // Skip saving this record and move to the next one
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
                                pay_DeductionItem.UploadInstanceId = savedRecord.UploadInstanceID;
                                #endregion

                                _context.Add(pay_DeductionItem);
                                _context.SaveChangesAsync().GetAwaiter().GetResult();
                                Pay_DeductionsList.Add(pay_DeductionItem);
                                uploadCount++;
                            }
                            // Break after the loop if the condition was met at least once
                        }
                        while (reader.NextResult());
                    }
                }
            }
            return Json("File2 upload success");
        }

        //public int CountChars()
        //{
        //    return 4;
        //}

        [HttpPost]
        public async Task<ActionResult> Create(Pay_UploadInstance uploadInstance, IFormFile FileUpload1, IFormFile FileUpload2)
        {

            //Task<int> result = new Task<int>(CountChars);
            //result.Start();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                Pay_UploadInstance savedRecord = new Pay_UploadInstance();

                var record = await _context.Pay_UploadInstance.Where(r => r.Year == uploadInstance.Year && r.MonthId == uploadInstance.MonthId).FirstOrDefaultAsync();

                if (record != null)
                {
                    _context.RemoveRange(await _context.Pay_Deduction.Where(r => r.UploadInstanceId == record.UploadInstanceID).ToListAsync());
                    await _context.SaveChangesAsync();
                    _context.RemoveRange(await _context.Pay_VIP.Where(r => r.UploadInstanceId == record.UploadInstanceID).ToListAsync());
                    await _context.SaveChangesAsync();
                    _context.Remove(record);
                    await _context.SaveChangesAsync();

                };

                if (uploadInstance.MonthId != 0)
                {
                    // Add the new record to the context
                    var addedRecord = _context.Pay_UploadInstance.Add(new Pay_UploadInstance
                    {
                        MonthId = uploadInstance.MonthId,
                        Year = uploadInstance.Year,
                        DateCreated = uploadInstance.DateCreated,
                        Site = "ORC"
                    }).Entity;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Return the added record with its data and ID populated
                    savedRecord = addedRecord;
                }

                if (FileUpload1 != null)
                {
                    UploadEarningsDoc(FileUpload1, savedRecord);
                }

                if (FileUpload2 != null)
                {
                    UploadDeductionsDoc(FileUpload2,savedRecord);
                }


            }
            catch (Exception ex)
            {

            }


            //// Add the uploadInstance to the context
            //_context.Pay_UploadInstance.Add(uploadInstance);
            //await _context.SaveChangesAsync();
            return Json("Successfully saved!"); // Redirect to the Index action after successful creation
        }


        [HttpGet]
        public async Task<IActionResult> GetIndividualDeductionsListByUploadInstanceId(int id)
        {
            var instance = await _context.Pay_UploadInstance.Where(r => r.UploadInstanceID == id).FirstOrDefaultAsync();
            if (instance != null)
            {
                var insData = new
                {
                    MonthName = await _context.Pay_Month.Where(r => r.MonthId == instance.MonthId).Select(r => r.MonthName.ToString().Trim()).FirstOrDefaultAsync(),
                    Year = instance.Year,
                    Site = instance.Site.Trim(),
                    DateCreated = instance.DateCreated
                };

                var date = insData.DateCreated.ToString();

                char[] arrayStr =  date.ToCharArray();

                var indx = Array.LastIndexOf(arrayStr,'/')+5;
                TempData["instanceDetails"] = 
                    $" &nbsp;&nbsp;    Month : <strong> {insData.MonthName} </strong>" +
                    $" &nbsp;&nbsp;    Year : <strong>{insData.Year}</strong>" +
                    $" &nbsp;&nbsp;    Site : <strong>{insData.Site}</strong>" +
                    $" &nbsp;&nbsp;    Date : <strong>{insData.DateCreated.ToString().Substring(0, indx)}</strong>";

            }
            var items = await _context.Pay_Deduction.Where(m => m.UploadInstanceId == id).ToListAsync();
            return View(items); 
        }

        [HttpGet]
        public async Task<IActionResult> GetIndividualEarningsListByUploadInstanceId(int Id)
        {
            var instance = await _context.Pay_UploadInstance.Where(r => r.UploadInstanceID == Id).FirstOrDefaultAsync();
            if (instance != null)
            {
                var insData = new
                {
                    MonthName = await _context.Pay_Month.Where(r => r.MonthId == instance.MonthId).Select(r => r.MonthName.ToString().Trim()).FirstOrDefaultAsync(),
                    Year = instance.Year,
                    Site = instance.Site.Trim(),
                    DateCreated = instance.DateCreated
                };

                var date = insData.DateCreated.ToString();

                char[] arrayStr = date.ToCharArray();

                var indx = Array.LastIndexOf(arrayStr, '/') + 5;
                TempData["instanceDetails"] =
                                                $" &nbsp;&nbsp;    Month : <strong> {insData.MonthName} </strong>" +
                                                $" &nbsp;&nbsp;    Year : <strong>{insData.Year}</strong>" +
                                                $" &nbsp;&nbsp;    Site : <strong>{insData.Site}</strong>" +
                                                $" &nbsp;&nbsp;    Date : <strong>{insData.DateCreated.ToString().Substring(0, indx)}</strong>";

            }
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
        [HttpGet]
        public JsonResult DeleteInstance(int? id)
        {
            if (id == null || _context.Pay_UploadInstance == null)
            {
                return Json(""); 
            }

            try
            {
                var pay_InstancePayroll = _context.Pay_UploadInstance.FirstOrDefaultAsync(m => m.UploadInstanceID == id).GetAwaiter().GetResult();
                if (pay_InstancePayroll == null)
                {
                    return Json(false, $"Instance with an {id} not found!");
                }

                if (pay_InstancePayroll != null)
                {
                    _context.RemoveRange(_context.Pay_Deduction.Where(r => r.UploadInstanceId == pay_InstancePayroll.UploadInstanceID).ToListAsync().GetAwaiter().GetResult());
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                    _context.RemoveRange(_context.Pay_VIP.Where(r => r.UploadInstanceId == pay_InstancePayroll.UploadInstanceID).ToListAsync().GetAwaiter().GetResult());
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                    _context.Remove(pay_InstancePayroll);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                };
                return Json(true);
            }
            catch(Exception ex)
            {
                return Json(false,$"Error : {ex.InnerException}");
            }
            
        }
        [HttpGet]
        public JsonResult GetEmployeeDeductionsByCode(int Id)
        {
            if(Id == 0)
            {
                return Json("Invalid employee code");
            }
            else
            {
                return Json(_context.Pay_Deduction.AsNoTracking().Where(r => r.EmployeeCode == Id).FirstOrDefaultAsync().GetAwaiter().GetResult());
            }
        }

        [HttpGet]
        public JsonResult GetEmployeeEarningsByCode(int Id)
        {
            if (Id == 0)
            {
                return Json("Invalid employee code");
            }
            else
            {
                return Json(_context.Pay_VIP.AsNoTracking().Where(r => r.EmployeeCode == Id).FirstOrDefaultAsync().GetAwaiter().GetResult());
                
            }
            return Json("");
        }

        //// POST: Pay_InstancePayroll/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Pay_UploadInstance == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Pay_InstancePayroll'  is null.");
        //    }
        //    var pay_InstancePayroll = await _context.Pay_UploadInstance.FindAsync(id);
        //    if (pay_InstancePayroll != null)
        //    {
        //        _context.Pay_UploadInstance.Remove(pay_InstancePayroll);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool Pay_InstancePayrollExists(int id)
        {
            return (_context.Pay_UploadInstance?.Any(e => e.UploadInstanceID == id)).GetValueOrDefault();
        }
    }
}
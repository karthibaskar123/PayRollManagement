#nullable disable
using Admin_Models;
using Leaves_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using ActionFilterTrace.Models;
using Microsoft.AspNetCore.Authorization;
using Complaint_Models;
using System.Data.SqlClient;
using Exception_Filter;
using Microsoft.AspNetCore.Identity;

namespace Admin_Controllers

{   
    // [ExceptionFilter]
    [Authorize(Roles = "Admin")]
// [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        public readonly EmployeeDbContext dbContext;
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostenvironment;
        public IConfiguration Configuration {get;}
        

        public AdminController(UserManager<IdentityUser> userManager,EmployeeDbContext dbContext, IWebHostEnvironment hostenvironment,IConfiguration configuration)
        {
            // throw new Exception();
            this.dbContext = dbContext;
            _userManager= userManager;
            _hostenvironment = hostenvironment;
            Configuration = configuration;
        }

        public IActionResult UserProfile()
        {
            return View();
        }

// checking duplicate username
        // public JsonResult IsEmployeeNameAvailable(string EmployeeName)
        // {
        //     return Json(!dbContext.employeeinformation.Any(employee => employee.EmployeeName == EmployeeName), new Newtonsoft.Json.JsonSerializerSettings());
        // }
[AcceptVerbs("Get","Post")]
// [AllowAnonymous]
// public async Task<IActionResult> IsEmployeeNameUSe(string name)
//     {
//         var user= await _userManager.FindByNameAsync(name);
//         if(user==null)
//         {
//             return Json(true);
//         }
//         else
//         {
//             return Json($"EmployeeName{name} is already in use");
//         }

//     }


        [HttpGet]

        public IActionResult ComplaintList()
        {
            var employee = dbContext.Complaints.ToList();
            IEnumerable<Complaint> data = from employee1 in employee select employee1;
            return View(data);
        }

//Delete Complaintbox
[HttpPost]
        public IActionResult  Delete1(int id)
        { 
            
                var employee = dbContext.Complaints.FirstOrDefault(employee1=>employee1.id == id);
                if(employee !=null)
                {
                    dbContext.Complaints.Remove(employee);
                    dbContext.SaveChangesAsync();
                    return RedirectToAction("ComplaintList");
                }
                return RedirectToAction("Complaintlist");
        } 
        
//Traceing employee activities using Action Filter
        [HttpGet] 
       public IActionResult TraceList()
      {
        try{
        
         var ls=TraceActivity.GetTrace();
         var mystatus=dbContext.TraceActivity.ToList();
           TraceActivity list =new TraceActivity
           {
            Username=ls.Username,
            Actionname=ls.Actionname,
            date=ls.date,
            Controllername=ls.Controllername
           };

          dbContext.TraceActivity.Add(list);
          dbContext.SaveChanges();
          var employee = dbContext.TraceActivity.ToList();
        //   IEnumerable<TraceActivity> add = from e in employee select e;
         
         return View(employee);
        }
            catch(Exception ex)
            {
            ViewBag.ErrorMessage = ex.Message;
            return View("Error");
            } 
      
    }
        public IActionResult Index()
        {
                return View();
        }

//Add employee 

[Route("Admin/AddEmployee")]
[HttpGet]
        public IActionResult AddEmployee()
        {

            try
                {
                var userData = dbContext.GetUserData(); // Incorrect reference here
                return View(userData);
                }
            catch (Exception )
                {
                // Log the exception or display an error message
                ViewBag.ErrorMessage = "An error occurred while creating data.";
                return View();
                   }
                // return View();
        }

        private string UploadFile(Admin forms)
        {
            string uniqueFileName = null;

        if(forms.image != null)
        {
            string uploadsFolder = Path.Combine(_hostenvironment.WebRootPath, "Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + forms.image.FileName;
            string filepath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                forms.image.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
            }


[HttpPost]
        public IActionResult AddEmployee(Admin admin,string username)
        {  
                   try{
                    string uniqueFileName = UploadFile(admin);
                    admin.ProfilePicture = uniqueFileName;
                    if(ModelState.IsValid)
                    {
    
                    dbContext.Attach(admin);
                    dbContext.Entry(admin).State = EntityState.Added;
                    dbContext.SaveChanges();
                    return RedirectToAction(nameof(EmployeeList));
                    }
                   }
                    catch (Exception )
                    {
                    // Handle the exception
                    ModelState.AddModelError("", "An error occurred while adding the user.");
                    return View(admin);
                    }
                   
                    return View(admin);
                    // return RedirectToAction(nameof(EmployeeList));

        }
      


//Leave Request's
        public IActionResult LeaveRequest()
        {
            var request= dbContext.RequestLeavetbl.ToList();
            IEnumerable<Leave> info1 = from request1 in request where request1.Status=="Pending" select request1;
            return View(info1);
        }

        public ActionResult approve(int id)
        {
            var approve1 = dbContext.RequestLeavetbl.Find(id);
            if (approve1 != null){
            approve1.Status="Approved";
            dbContext.RequestLeavetbl.Update(approve1);
            dbContext.SaveChanges();
            return RedirectToAction("LeaveRequest","Admin");
            }
            return RedirectToAction("LeaveRequest","Admin");
        }

        public ActionResult reject(int id)
        {
            var reject1 = dbContext.RequestLeavetbl.Find(id);
            if (reject1 != null){
            reject1.Status="Rejected";
            dbContext.RequestLeavetbl.Update(reject1);
            dbContext.SaveChanges();
            return RedirectToAction("LeaveRequest","Admin");
            }
            return RedirectToAction("LeaveRequest","Admin");
        }
    // [Route("")]
    // [Route("Admin")]
    // [Route("Admin/Employeelist")]
    [HttpGet]
    public IActionResult EmployeeList(string department,string employeename,string month)
    { 
            // var employee=dbContext.employeeinformation.ToList();
            // IEnumerable<Admin> data= from emp in employee select emp;
            // return View(data);
        var employee=from employee1 in dbContext.employeeinformation select employee1;
        if(!String.IsNullOrEmpty(department))
        {
            employee = employee.Where(emp=>emp.Department!.Contains(department));
            if(employee!=null)
            {
                return View(employee.ToList());
            }
        }
        if(!String.IsNullOrEmpty(employeename))
        {
            employee = employee.Where(emp=>emp.EmployeeName!.Contains(employeename));
            if(employee!=null)
            {
                return View(employee.ToList());
            }
        } 

        return View(employee.ToList());
    }

[Route("Details/{id:int:regex(\\d{{3}}):range(100,120)}")]
        public IActionResult Details(int id,string name)
        {
                
                    var employee=dbContext.employeeinformation.ToList();
                    IEnumerable<Admin> data= from emp in employee where emp.Employeeid==id select emp;
                    if(employee==null)
                    {
                            return View("Error");
                    }
                    return View(data);
        }
        public new IActionResult Dispose()
        {
            dbContext.Dispose();
            return View();
        }

//update employee details

// [HttpGet("UpdateEmployeeList/{id:int}")]
[HttpGet]
// [Route("UpdateEmployeeList/{id:int}")]
        public IActionResult UpdateEmployeeList(int? id)
        {              
            var employee = dbContext.employeeinformation.FirstOrDefault( employee1=> employee1.Employeeid==id );
            if(employee !=null)
            {
                employee.Employeeid=employee.Employeeid;
                employee.EmployeeName=employee.EmployeeName;
                employee.Age=employee.Age;
                employee.JoiningDate=employee.JoiningDate;
                employee.Gender=employee.Gender;
                employee.PhoneNumber=employee.PhoneNumber;
                employee.Department=employee.Department;
                employee.Address=employee.Address;
                employee.AccountNumber=employee.AccountNumber;
                employee.Email=employee.Email;
                employee.ProfilePicture=employee.ProfilePicture;
                employee.Username=employee.Username;
                employee.Password=employee.Password;
                return View(employee);
            }
            return RedirectToAction("EmployeeList");
            }

    [HttpPost]
        public async Task<IActionResult> UpdateEmployeeList(Admin admin,int id)
        {  
            string uniqueFileName = UploadFile(admin);
            admin.ProfilePicture = uniqueFileName;
            var employee = dbContext.employeeinformation.FirstOrDefault(employee1 => employee1.Employeeid == id );
            if(employee !=null)
               {
                    employee.Employeeid = admin.Employeeid;
                    employee.EmployeeName = admin.EmployeeName;
                    employee.Age = admin.Age;
                    employee.JoiningDate = admin.JoiningDate;
                    employee.Gender = admin.Gender;
                    employee.PhoneNumber=admin.PhoneNumber;
                    employee.Department=admin.Department;
                    employee.Address = admin.Address;
                    employee.AccountNumber = admin.AccountNumber;
                    employee.Email =admin.Email;
                    employee.ProfilePicture=admin.ProfilePicture;
                    employee.Username=admin.Username;
                    employee.Password=admin.Password;
               };
            dbContext.employeeinformation.Update(employee);
            await dbContext.SaveChangesAsync();
            return View(employee);
            }

//Delete employee
[HttpPost]
        public IActionResult Delete(int id)
        {        
                using (dbContext)
                {
                var employee = dbContext.employeeinformation.FirstOrDefault(employee1=>employee1.Employeeid == id);
                dbContext.employeeinformation.Remove(employee);
                dbContext.SaveChanges();       
                return RedirectToAction("EmployeeList");
                }
        } 

            
            
        }
    }


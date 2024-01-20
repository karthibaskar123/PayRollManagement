#nullable disable
using Admin_Models;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Complaint_Models;
using BonusModel;
using Newtonsoft.Json;
using Leaves_Models;
using ActionFilterTrace.Models;
using Microsoft.AspNetCore.Authorization;

namespace Employee_Controllers
{
    [TraceActivity]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public readonly EmployeeDbContext dbContext;
        private readonly IWebHostEnvironment _hostenvironment;
        public EmployeeController(EmployeeDbContext dbContext,IWebHostEnvironment hostenvironment)
        {
            this.dbContext = dbContext;
            _hostenvironment = hostenvironment;
        }

        public IActionResult ComplaintBox()
        {
           return View();
        }
        
//userprofile calling data using session
        public IActionResult EmployeeUserProfile()
        {
            var name=HttpContext.Session.GetString("loginname");
            var emp=dbContext.employeeinformation.ToList();
            IEnumerable<Admin> employee = from employee1 in emp where employee1.Username==name select employee1;
            return View(employee);
        }

        public IActionResult UpdateEmployeeList()
        {
            return View();
        }



//Receipt data     
        public IActionResult Receipt(int id)
        {   
            var report = dbContext.employeeinformation.Include(salary=>salary.roles).ToList();
            IEnumerable<Admin> query=from report1 in report where report1.Employeeid==id  select report1;
            return View(query);
        }





    [HttpGet]    
    public IActionResult ViewReportEmployee(string employeename,string month)
    {
        var name=HttpContext.Session.GetString("loginname");
        var employee=from employee1 in dbContext.employeeinformation.Include(salary=>salary.roles).Where(employee=>employee.Username==name).ToList() select employee1;
        var salary = from emp in dbContext.Salarydata.Include(salary=>salary.myadmin).ToList() select emp;
        if(!String.IsNullOrEmpty(employeename))
        {
            employee = employee.Where(employee=>employee.EmployeeName!.Contains(employeename));
            if(employee!=null)
            {
                return View(employee.ToList());
            }
        }
        // if(!String.IsNullOrEmpty(month))
        // {
        //     employee = employee.Where(employee=>employee.Month!.Contains(month));
        //     if(employee!=null)
        //     {
        //         return View(employee.ToList());
        //     }
        // }
        // if(!String.IsNullOrEmpty(month))
        // {
            
        //     salary = salary.Where(employee=>employee.Month==month);
        //     if(employee!=null)
        //     {
        //         return View(employee.ToList());
        //     }
        // }
        return View(employee!.ToList());

    }


[HttpGet]
        public IActionResult UpdateProfile()
        {
            var name = HttpContext.Session.GetString("loginname");
            var user = dbContext.employeeinformation.FirstOrDefault(x=>x.Username==name);
                 if(user!=null){
                 user.Employeeid = user.Employeeid;
                 user.EmployeeName=user.EmployeeName;
                 user.Age=user.Age;
                 user.JoiningDate=user.JoiningDate;
                 user.Gender=user.Gender;         
                 user.PhoneNumber=user.PhoneNumber;
                 user.Address=user.Address;
                 user.AccountNumber=user.AccountNumber;
                 user.Email=user.Email;
                 user.ProfilePicture=user.ProfilePicture;
                 user.Username=user.Username;
                 user.Password=user.Password;
                 };
           return View(user); 
        }
//upload profile photo
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
        public async Task<IActionResult> UpdateProfile(Admin admin)
        {
            var name=HttpContext.Session.GetString("loginname");
            string uniqueFileName = UploadFile(admin);
            admin.ProfilePicture = uniqueFileName;
            var user = dbContext.employeeinformation.SingleOrDefault(x=>x.Username==name);
             
             if(user!=null){  
                 user.Employeeid=admin.Employeeid;
                 user.EmployeeName=admin.EmployeeName;
                 user.Age=admin.Age;
                 user.JoiningDate=admin.JoiningDate;
                 user.Gender=admin.Gender;
                 user.PhoneNumber=admin.PhoneNumber;
                 user.Address=admin.Address;
                 user.AccountNumber=admin.AccountNumber;
                 user.Email=admin.Email;
                 user.ProfilePicture=admin.ProfilePicture;
                 user.Username=admin.Username;
                 user.Password=admin.Password;
           
                 
                  dbContext.employeeinformation.Update(user);
                  await dbContext.SaveChangesAsync();
                  return View(user);
             };
             TempData["AlertMessage"]="Updated Profile Details";
             return View(user);
            }
  
//Complainbox adding data
[HttpPost]
        public IActionResult ComplaintBox(Complaint employee)
        {  
                    dbContext.Attach(employee);
                    dbContext.Entry(employee).State = EntityState.Added;
                    dbContext.SaveChanges();
                    return View(employee);
        }

// [HttpPost]
// [ValidateAntiForgeryToken]
//         public IActionResult ComplaintBox([Bind(Username,Mailid,Subject,Reason)]Complaint employee)
//         {  
//             if(ModelState.IsValid)
//             {
//                     dbContext.Attach(employee);
//                     dbContext.Entry(employee).State = EntityState.Added;
//                     dbContext.SaveChanges();
//                     return View(employee);
//             }
//             return View(employee);
//         }


//Leave report by empid using session
        public ActionResult LeaveReport()
        {
            var name = HttpContext.Session.GetString("loginname");
            var employee = dbContext.RequestLeavetbl.ToList();
            IEnumerable<Leave> data = from employee1 in employee  where employee1.Username==name select employee1;
            return View(data);
        }
//Request Leave
        public ActionResult RequestLeave()
        {
            return View();
        }

[HttpPost]
        public async Task<IActionResult> RequestLeave(Leave request){
            var request2 = new Leave()
            {
               id=request.id,
               Username=request.Username,
               FromDate=request.FromDate,
               NumberofDates=request.NumberofDates,
               Reason=request.Reason,
               Status= request.Status="Pending"
                
            };
            await dbContext.RequestLeavetbl.AddAsync(request2);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(RequestLeave));
        }
  
    }
}
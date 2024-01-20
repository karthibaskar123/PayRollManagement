#nullable disable
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Data.SqlClient;
using Login_Models;
using Microsoft.AspNetCore.Mvc;
using Admin_Models;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using ActionFilterTrace.Models;
using Salary_Models;

namespace Login_Controllers
{
    public class LoginController : Controller
    
    {
        public readonly EmployeeDbContext dbContext;
        public IConfiguration Configuration {get;}
        public LoginController(IConfiguration configuration, EmployeeDbContext context)
        

        {
            dbContext = context;
            Configuration = configuration;
        }

//         [Authorize(Roles = "Admin")]
//         public IActionResult AdminLogin()
        
//         {
//             if(Request.Cookies["Username"]!=null && Request.Cookies["Password"]!=null)
//         {
//             TempData["uname"]=Request.Cookies["Username"];
//             TempData["pword"]=Request.Cookies["Password"];
//         }
//             return View();
//         }

        
// [HttpPost]
// [Authorize(Roles = "Admin")]
// public IActionResult AdminLogin(Account admin)
// {
// string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];
//             using(SqlConnection connection = new SqlConnection(connectionstring))
            
//             {
//                 connection.Open();
//                 string query = "select * from AdminLogin where Username = @Username and Password = @Password";
//                 SqlCommand command = new SqlCommand(query, connection);
//                 command.Parameters.AddWithValue("@Username",admin.Username);
//                 command.Parameters.AddWithValue("@Password",admin.Password);
//                 SqlDataReader datareader = command.ExecuteReader();
//                 if(datareader.Read())
                
//                 { 
//                     //Logger
//                      var logger=new Logger();
//                     logger.log(admin.Username);

//                     //set session
//                     HttpContext.Session.SetString("Name",admin.Username);


                   
//                     //Cookie
//                     CookieOptions options=new CookieOptions();
//                     if(admin.Rememberme !=false)
                    
//                     {
//                         options.Expires=DateTime.Now.AddMinutes(1);
//                         Response.Cookies.Append("Username",admin.Username);
//                         Response.Cookies.Append("Password",admin.Password);
//                     }


//                     return RedirectToAction("AdminDashBoard","Login");
                
//                 }

//                 else

//                 {
//                     ModelState.AddModelError("", "Invalid user!");
//                 }

//                 return View();
//             }
// }

// [Authorize(Roles = "Employee")]
// [HttpGet]
//         public IActionResult EmployeeLogin()
//         {
//             Admin emps = new Admin();
//             var emp = HttpContext.Session.GetString("loginname");
//                 // Viewbag.count = emp;
//             HttpContext.Session.GetInt32("id");
//             return View();
// }

// [HttpPost]
// [Authorize(Roles = "Employee")]

//         public async Task<IActionResult> EmployeeLogin(Admin admin)
//         {
//             var emp = await dbContext.employeeinformation.FirstOrDefaultAsync(u => u.Username == admin.Username && u.Password == admin.Password);
            
//             if (emp != null)
//             {
//                 var logger=new Logger();
//                 logger.log(emp.Username);
//                 HttpContext.Session.SetString("loginname",emp.Username);
//                 HttpContext.Session.SetInt32("id",emp.Employeeid);
//                 // HttpContext.Session.SetInt32("admin", 0);
//                 HttpContext.Session.SetString("Name",admin.Username);
//                 return RedirectToAction("EmployeeDashboard","Login");
//             }
//             else
//             {
//                 ModelState.AddModelError("", "Invalid User!");
//                 return View(admin);
//             }
//         }

        [Authorize(Roles = "Employee")]
 public IActionResult EmployeeDashBoard()
        {
            var name=HttpContext.Session.GetString("loginname");
            var emp = dbContext.employeeinformation.Include(e=>e.roles).ToList();           
            IEnumerable<Admin> employee = from e in emp where e.Username==name select e;
        
            return View(employee);
        }

[Authorize(Roles = "Admin")]

public IActionResult AdminDashboard()
{
            var name=HttpContext.Session.GetString("loginname");
            // IEnumerable<Admin> employee = from e in name where e.Username==name select e;
            var emps = dbContext.employeeinformation.Count();
            ViewBag.empcount = emps;
            return View();
}

    }

}
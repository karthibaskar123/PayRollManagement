#nullable disable
using Admin_Models;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Data;

using Microsoft.EntityFrameworkCore;
using SelectPdf;
using BonusModel;
using Newtonsoft.Json;
using RoleSalary_Models;

namespace Role_Controllers

{
    public class RoleController : Controller
    {
        public readonly EmployeeDbContext dbContext;

        public RoleController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

public IActionResult CreateRoleSalary()
{
    return View();
}

[HttpPost]
        public IActionResult CreateRoleSalary(RoleSalary roleSalary)
        {  

                    dbContext.Attach(roleSalary);
                    dbContext.Entry(roleSalary).State = EntityState.Added;
                    dbContext.SaveChanges();
                    return RedirectToAction(nameof(RoleSalaryList));
        }     

        [HttpGet]
        public IActionResult RoleSalaryList()
        {
            var rolesalary = dbContext.RoleSalaries.ToList();
            IEnumerable<RoleSalary> add = from rolesalary1 in rolesalary select rolesalary1;
            return View(add);
        }
        
        
        
    }  
      
}
    
    
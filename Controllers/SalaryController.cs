#nullable disable
using Admin_Models;
using Microsoft.AspNetCore.Mvc;
using PayrollManagementSystem.Data;
using Salary_Models;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using BonusModel;
using Newtonsoft.Json;

namespace Salary_Controllers

{
    public class SalaryController : Controller
    {
        public readonly EmployeeDbContext dbContext;

        public SalaryController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult RoleBaseSalary()
        {
            return View();
        }

//generate pdf
        public FileResult GeneratePdf(string html)
        {
            html = html.Replace("strtTag", "<").Replace("EndTag", ">");
            HtmlToPdf objhtml = new HtmlToPdf();
            PdfDocument objdoc = objhtml.ConvertHtmlString(html);
            byte[] pdf = objdoc.Save();
            objdoc.Close();
            return File(pdf,"application/pdf","Receipt.pdf");
        }
        public IActionResult Repo()
        {
            return View();
        }

// search box for viewreport by employee id,deoartment,month

    [HttpGet]
    public IActionResult SalaryReport(string department,int employeeid,string month)
    {
        var employee=from employeedata in dbContext.employeeinformation.Include(salary=>salary.roles).ToList() select employeedata;
        
        if(employeeid!=0)
        {
            employee = employee.Where(data=>data.Employeeid==employeeid);
            if(employee!=null)
            {
                return View(employee.ToList());
            }
        }
        if(!String.IsNullOrEmpty(department))
        {
            employee = employee.Where(data=>data.Department!.Contains(department));
            if(employee!=null){
                return View(employee.ToList());
            }
        }
        // if(!String.IsNullOrEmpty(month))
        // {
        //     employee = employee.Where(data=>data.Month!.Contains(month));
        //     if(employee!=null)
        //     {
        //         return View(employee);
        //     }
        // }
        return View(employee!.ToList());
    }



//Receipt data     
        public IActionResult Receipt(int id)
        {   
            var report = dbContext.employeeinformation.Include(salary=>salary.roles).ToList();
            IEnumerable<Admin> data=from report1 in report where report1.Employeeid==id  select report1;
            return View(data);
        }

//using web api to list the bonus list
        public async Task<IActionResult> BonusList()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            using(var client = new HttpClient(clientHandler))
            {               
                var response = await client.GetAsync("http://localhost:5004/api/Bonus");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Bonuss>>(json);
                return View(data);
            }
        }

            [HttpGet]
    public async Task<IActionResult> RoleBAseSalaryList(string month,string rolename,int employeeid)
    {
        var name = HttpContext.Session.GetString("name");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                // Pass the handler to httpclient(from you are calling api)
                using(var client = new HttpClient(clientHandler))
                {               
                var response = await client.GetAsync("http://localhost:5004/api/Bonus");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Bonuss>>(json);
                data=data.ToList();
                var fdata=data.Select(e=>new{e.Bonus,e.BounusAmount});
                ViewBag.Bonus=fdata;
                }

        var Employee=from employee in dbContext.Salarydata select employee;
        if(!String.IsNullOrEmpty(month))
        {
            Employee = Employee.Where(course=>course.Month!.Contains(month));
            if(Employee!=null){
                return View(Employee.ToList());
            }
        }
        if(!String.IsNullOrEmpty(rolename))
        {
            Employee = Employee.Where(data=>data.Rolename!.Contains(rolename));
            if(Employee!=null){
                return View(Employee.ToList());
            }
        }
         if(employeeid!=0)
        {
            Employee = Employee.Where(data=>data.Employeeid==employeeid);
            if(Employee!=null)
            {
                return View(Employee.ToList());
            }
        }
        return View(Employee!.ToList());
    }


[HttpPost]
        public async Task<IActionResult>RoleBaseSalary(Salary salary)
        {       
                if(ModelState.IsValid)
                {
                var name = HttpContext.Session.GetString("name");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                // Pass the handler to httpclient(from you are calling api)
                using(var client = new HttpClient(clientHandler))
                {               
                var response = await client.GetAsync("http://localhost:5004/api/Bonus");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Bonuss>>(json);
                data=data.ToList();
                var fdata=data.Where(bonusdata=>bonusdata.Bonus==salary.Bonus).Select(bonusdata=>bonusdata.BounusAmount);
                ViewBag.BonusAmount=fdata;
                foreach(var item in ViewBag.BonusAmount)
                {
                    ViewBag.Value=item;
                }
                var rolesalary = dbContext.RoleSalaries.ToList();
                var Salary = rolesalary.Where(salary1=>salary1.RoleName==salary.Rolename).Select(rolesal=>rolesal.BasicPay);
                ViewBag.BasicPay=Salary;
                foreach(var item in ViewBag.BasicPay)
                {
                    ViewBag.Salary=item;
                }

                
                }

            var Salary1 = new Salary();

            Salary1.GrossSalary = ViewBag.Salary + salary.TravellAllowance + salary.MedicalAllowance;
            Salary1.PF = Salary1.GrossSalary * 10/100;
            Salary1.ESI = Salary1.GrossSalary *10/100;
                if(Salary1.GrossSalary>50000)
                {
                 Salary1.Tax = Salary1.GrossSalary * 10/100;
                }
                else if(Salary1.GrossSalary>30000)
                {
                 Salary1.Tax = Salary1.GrossSalary * 10/100;
                }
                else
                {
                 Salary1.Tax = 0;
                }
                if(ViewBag.Value==null)
                {
                    Salary1.NetPay=Salary1.GrossSalary - Salary1.PF - Salary1.ESI - Salary1.Tax;
                }
                else{
                Salary1.NetPay=Salary1.GrossSalary - Salary1.PF - Salary1.ESI - Salary1.Tax + ViewBag.Value;

                }
                var Salary2 = new Salary()

            {   Employeeid = salary.Employeeid,    
                Rolename = salary.Rolename,
                Bonus=salary.Bonus,
                Month =salary.Month,
                GrossSalary = Salary1.GrossSalary,
                TravellAllowance = salary.TravellAllowance,
                MedicalAllowance = salary.MedicalAllowance,
                PF = Salary1.PF,
                ESI = Salary1.ESI,
                Tax = Salary1.Tax,
                NetPay = Salary1.NetPay              
            };
            await dbContext.Salarydata.AddAsync(Salary2);
            await dbContext.SaveChangesAsync();
                }
            return RedirectToAction(nameof(RoleBAseSalaryList));
        }
        
//update Role
[HttpGet]
         public IActionResult Update(int id)
         
        {
            var salary = dbContext.Salarydata.FirstOrDefault(x => x.Salaryid == id );
            if(salary !=null)
            {   
                salary.Employeeid=salary.Employeeid;
                salary.Rolename = salary.Rolename;
                salary.Month = salary.Month;
                salary.BasicPay = salary.BasicPay;
                salary.GrossSalary = salary.GrossSalary;
                salary.TravellAllowance = salary.TravellAllowance;
                salary.MedicalAllowance = salary.MedicalAllowance;
                salary.PF = salary.PF;
                salary.ESI = salary.ESI;
                salary.Tax = salary.Tax;
                salary.NetPay = salary.NetPay;
                return View(salary);
            }
            return RedirectToAction("RoleBAseSalaryList"); 

        }

[HttpPost]
        public async Task<IActionResult> Update(Salary salary,int id)
        {
            try{
                var name = HttpContext.Session.GetString("name");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                // Pass the handler to httpclient(from you are calling api)
                using(var client = new HttpClient(clientHandler))
                {               
                var response = await client.GetAsync("http://localhost:5004/api/Bonus");
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Bonuss>>(json);
                data=data.ToList();
                var fdata=data.Where(e=>e.Bonus==salary.Bonus).Select(e=>e.BounusAmount);
                ViewBag.BonusAmount=fdata;
                foreach(var item in ViewBag.BonusAmount)
                {
                    ViewBag.Value=item;
                }
                var rolesalary = dbContext.RoleSalaries.ToList();
                var Salary = rolesalary.Where(salary1=>salary1.RoleName==salary.Rolename).Select(rolesal=>rolesal.BasicPay);
                ViewBag.BasicPay=Salary;
                foreach(var item in ViewBag.BasicPay)
                {
                    ViewBag.Salary=item;
                }      
                }

            var Salary1 = new Salary();

            Salary1.GrossSalary = ViewBag.Salary + salary.TravellAllowance + salary.MedicalAllowance;
            Salary1.PF = Salary1.GrossSalary * 10/100;
            Salary1.ESI = Salary1.GrossSalary * 10/100;
                if(Salary1.GrossSalary>50000)
                {
                 Salary1.Tax = Salary1.GrossSalary * 10/100;
                }
                else if(Salary1.GrossSalary>30000)
                {
                 Salary1.Tax = Salary1.GrossSalary * 10/100;
                }
                else
                {
                 Salary1.Tax = 0;
                }
                if(ViewBag.Value==null)
                {
                    Salary1.NetPay=Salary1.GrossSalary - Salary1.PF - Salary1.ESI - Salary1.Tax;
                }
                else
                {
                    Salary1.NetPay=Salary1.GrossSalary - Salary1.PF - Salary1.ESI - Salary1.Tax + ViewBag.Value;

                }
            var salary1 = dbContext.Salarydata.FirstOrDefault(x => x.Salaryid == id );
            if(salary1 !=null)
            {
                salary1.Employeeid = salary.Employeeid;    
                salary1.Rolename = salary.Rolename;
                salary1.Bonus=salary1.Bonus;
                salary.Month = salary.Month;
                salary1.GrossSalary = Salary1.GrossSalary;
                salary1.TravellAllowance = salary.TravellAllowance;
                salary1.MedicalAllowance = salary.MedicalAllowance;
                salary1.PF = Salary1.PF;
                salary1.ESI = Salary1.ESI;
                salary1.Tax = Salary1.Tax;
                salary1.NetPay = Salary1.NetPay;
               };
             dbContext.Salarydata.Update(salary1);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("RoleBAseSalaryList"); 
            }
            catch(ArithmeticException)
            {
                    throw new ArithmeticException();
            }
        }


//delete role
[HttpPost]
        public IActionResult  Delete(int id)
        {
                var salary = dbContext.Salarydata.FirstOrDefault(salary=>salary.Salaryid == id);
                if(salary !=null)
                {
                    dbContext.Salarydata.Remove(salary);
                    dbContext.SaveChanges();     
                }
                return RedirectToAction("RoleBAseSalaryList");
        }               
    }
}

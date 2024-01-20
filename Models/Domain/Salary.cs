#nullable disable
using System.ComponentModel.DataAnnotations;
using Admin_Models;

namespace Salary_Models
{
   public class Salary
    {
        public int Salaryid {get;set;}
        [Required(ErrorMessage ="Please Enter Employeeid")]
        [Display(Name = "Employeeid")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        public int Employeeid{get;set;}//Forgein key
        [Required(ErrorMessage = "please click Role")]
        [Display(Name = "Role")]
        public string Rolename {get;set;}
        [Required(ErrorMessage = "Choose bonus")]
        [Display(Name = "Bonus")]
        public string Bonus{get;set;}
        [Required(ErrorMessage = "Choose Month")]
        [Display(Name = "Month")]
        public string Month{get;set;}
        
        [Required(ErrorMessage = "please enter TravellAllowance")]
        [Display(Name = "TravellAllowance")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        public float TravellAllowance {get;set;}
        [Required(ErrorMessage = "please enter MedicalAllowance")]
        [Display(Name = "MedicalAllowance")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        public float MedicalAllowance {get;set;}
        public float GrossSalary {get;set;}
        public float BasicPay {get;set;}
        public float PF{get;set;}
        public float ESI{get;set;}
        public float Tax {get;set;}
        public float NetPay {get;set;}  
        public Admin myadmin{get;set;}

        internal static string GetUserData()
        {
            throw new NotImplementedException();
        }
    }
    }

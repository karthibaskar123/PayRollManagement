#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Salary_Models;
using validate;

namespace Admin_Models
{   
    public class Admin 
    {

        [Required(ErrorMessage ="Please Enter Employeeid")]
        [Display(Name = "EmployeeID")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]

        public int Employeeid{get;set;}

        [Required(ErrorMessage ="Please add Employee")]
        [Display(Name = "EmployeeName")]
        [RegularExpression(@"^([A-Z]{1,})+[a-z]+\S$", ErrorMessage = "Please Enter First letter chapital and only Allow Character")]
        [MaxLength(15, ErrorMessage = "Max 15 characters")]
        // [Remote("IsEmployeeNameAvailable", "Admin", ErrorMessage = "Employee EmployeeName already exists.")]
        // [Remote(action:"IsEmployeeNameUSe", controller:"Admin", ErrorMessage = "Employee EmployeeName already exists.")]
        public string EmployeeName{get;set;}

        [Required(ErrorMessage = "Please enter Age")]
        [Display(Name = "Age")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        [Range(18,60,ErrorMessage ="Age should be Between 18 to 60")]
        public int Age{get;set;}

        [Required(ErrorMessage = "Please Choose JoiningDate")]
        [Display(Name = "JoiningDate")]
        [LessDateAttribute]
        [DataType(DataType.Time)]
        // [RegularExpression(@"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/(19|20)\d\d$",ErrorMessage = "Enter valid joiningDate.")]
        public DateTime JoiningDate{get;set;}
        [Required(ErrorMessage = "Please Choose Gender")]
        [Display(Name = "Gender")]

        public string Gender{get;set;}

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        [MinLength(10, ErrorMessage = "Min 10 characters")]

        public string Address{get;set;}

        [Required(ErrorMessage = "Please enter PhoneNumber")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please Enter Valid Numbers and start with 6 to 9")]
        [Display(Name = "PhoneNumber")]

        public long PhoneNumber{get;set;}

        [Required(ErrorMessage = "Please Choose Department")]
        [Display(Name = "Department")]

        public string Department{get;set;}
      
        [Required(ErrorMessage = "Please enter the AccountNo")]
        [Display(Name = "AccountNumber")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        [Range(111111111111,999999999999,ErrorMessage ="Account Number is Invalid should be 12 number only")]

        public long AccountNumber{get;set;}
        
        [Required(ErrorMessage = "Email id is required")]
        [Display(Name = "Email")]
        [StringLength(30, ErrorMessage = "Should use Character, any special character and number", MinimumLength = 10)]
        [RegularExpression( "^[A-Z][a-z_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "Invalid email format." )]
        public string Email{get;set;}
        public string ProfilePicture{get; set;}

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [MaxLength(15, ErrorMessage = "Max 10 characters")]
        public string Username{get;set;}    
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(15, ErrorMessage = "Should use firstletter capital ,use any special character and number", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password{get;set;}
        public List<Salary> roles{get;set;}
        [NotMapped]
        public IFormFile image {get; set;}

        internal static string GetUserData()
        {
            throw new NotImplementedException();
        }
    }
}
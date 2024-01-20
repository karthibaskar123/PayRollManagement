using System;
using System.ComponentModel.DataAnnotations;

namespace  RoleSalary_Models
{
    public class  RoleSalary
    {
        public int Roleid{get;set;}
        [Required(ErrorMessage = "Enter Rolename")]
        [Display(Name = "RoleName")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please Enter only Character")]
        [MaxLength(15, ErrorMessage = "Max 15 characters")]
        public string? RoleName {get;set;}
        [Required(ErrorMessage = "Enter Basic Pay")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        public float BasicPay{get;set;}
    }
    
}
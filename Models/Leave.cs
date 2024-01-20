using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaves_Models
{
    public class Leave
    {
        public int id{get;set;}

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [MaxLength(15, ErrorMessage = "Max 10 characters")]
        public string? Username{get;set;}  
        public DateTime FromDate{get;set;}
        [Required(ErrorMessage ="Please enter Number of dates")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter only Numbers")]
        public int NumberofDates {get;set;}
        [Required(ErrorMessage ="Please add Reason")]
        [Display(Name = "Reason")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string ?Reason {get;set;}
        public string ?Status {get;set;}
    }
}
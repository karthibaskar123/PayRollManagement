using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaint_Models
{
    public class Complaint
    {
        public int id{get;set;}
        
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        [MaxLength(15, ErrorMessage = "Max 10 characters")]
        public string ?Username{get;set;}   
         [Required(ErrorMessage = "Email id is required")]
        [RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "Invalid email format." )]
        public string ?Mailid{get;set;}
        [Required(ErrorMessage ="Please add Reason")]
        [Display(Name = "Subject")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string ?Subject {get;set;}
        [Required(ErrorMessage ="Please add Reason")]
        [Display(Name = "Message")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string ?Reason {get;set;}
    }
}
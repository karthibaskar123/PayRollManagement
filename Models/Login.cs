using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Login_Models
{
    public class Account
    {
        [Required(ErrorMessage ="Username required")]
        [Display(Name = "Username")]
        public string? Username {get;set;}
        [Required(ErrorMessage ="Password required")]
        [Display(Name = "Password")]
        public string? Password {get;set;}
        public bool Rememberme {get;set;}
    }
   public abstract class LogBase
    {
        /* abstract class */
        public abstract void log(string message);
    }
    public class Logger:LogBase
    {
        private string Currentdirectory{get;set;}
        private string Filename{get;set;}
        private string Filepath{get;set;}
        public Logger()
        {
            this.Currentdirectory=Directory.GetCurrentDirectory();
            this.Filename="Log.txt";
            this.Filepath=this.Currentdirectory+"/"+this.Filename;
        }
        public override void log(string message)
        {
            //write to file
            using(StreamWriter writer=File.AppendText(this.Filepath))
            {
                writer.Write("\n Log Entry : ");
                writer.Write("{0} {1}",DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString());
                writer.Write("Username : {0} ",message);
                writer.Write("\n------------------------------------------------------ ");
            }
        }
    }


}

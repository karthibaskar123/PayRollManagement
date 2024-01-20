using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Exception_Filter{
public class ExceptionFilter: Attribute,IExceptionFilter   
{  
    public void OnException(ExceptionContext filterContext)   
    { 

        if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)   
        {  
            filterContext.ExceptionHandled = true; 
            filterContext.Result = new RedirectResult("/Home/customErrorPage");  
        }   
    }  
}

}
using Admin_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidateEmployeeDetailsAttribute : ActionFilterAttribute
{
        public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            base.OnActionExecuting(context);
    }
}




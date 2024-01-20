#nullable disable
using Microsoft.AspNetCore.Mvc.Filters;
namespace ActionFilterTrace.Models
{
    public class TraceActivity : ActionFilterAttribute
{
    public int traceid{get;set;}
    public string Controllername{get;set;}
    public string Username{get;set;}
    public string Actionname{get;set;}
    public DateTime date{get;set;}
    public static List<TraceActivity>Trace=new List<TraceActivity>();
    public static TraceActivity status;


    public override void OnActionExecuted(ActionExecutedContext filtercontext)
    {
        var name=filtercontext.HttpContext.Session.GetString("loginname");
        //  var name=filtercontext.HttpContext.User.Identity?.Name;
        
        status =new TraceActivity
        {
            Username=name,
            Controllername=filtercontext.ActionDescriptor.DisplayName,
            Actionname=filtercontext.ActionDescriptor.DisplayName,
            date=DateTime.Now
        };


        Trace.Add(status);
        base.OnActionExecuted(filtercontext);
    }
    public static List<TraceActivity>TraceList()
    {
        return Trace;
    }
    public static TraceActivity GetTrace()
    {
        return status;
    }


}}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;


namespace WebApiDemo.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            File.WriteAllText("logs.txt", context.Exception.ToString());
            context.Result = new ObjectResult("An error occurred") { StatusCode = 500 };
        }
    }
}
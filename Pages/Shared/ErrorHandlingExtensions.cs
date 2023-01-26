using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Diagnostics;
using CodeMechanic.Models;

namespace CodeMechanic.GlobalErrorHandling.Extensions
{
    public interface ILoggerManager {
        public void LogError(string e);
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                var trace = new StackTrace();

                // TODO: Log this record to Airtable or some other SQL DB like Postgres...       
                var my_custom_logrow = new LogRow()
                        {
                            CreatedBy = "Nugs",
                            ExceptionMessage = trace.ToString()                            
                        };


                // Other stuff that apparently works only with http request errors (ewww):

                // appError.Run(async context =>
                // {
                //     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //     context.Response.ContentType = "application/json";

                //     var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                //     if(contextFeature != null)
                //     { 
                //         logger.LogError($"Something went wrong: {contextFeature.Error}");

                //         await context.Response.WriteAsync(my_custom_logrow.ToString());
                //     }
                // });
            });
        }
    }
}
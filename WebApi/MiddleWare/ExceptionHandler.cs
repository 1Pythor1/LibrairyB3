using App.Error;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.MiddleWare
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;      

        public ExceptionHandler(RequestDelegate next) =>
            _next = next;
        

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if(ex is not BaseAppException)
                    ex = BaseAppException.FromException(ex);

                await HandleException(context, (BaseAppException)ex);
            }
        }

        private Task HandleException(HttpContext context, BaseAppException ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Une erreur est survenue",
                Detail = ex.Message,
                Status = (int)ex.Status
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problem.Status.Value;

            return context.Response.WriteAsJsonAsync(problem);
        }
    }
}



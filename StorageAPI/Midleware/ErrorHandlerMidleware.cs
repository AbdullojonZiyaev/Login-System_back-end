using Newtonsoft.Json;
using StorageAPI.Errors;
using StorageAPI.Interfase;
using StorageAPI.Interfase.implementations;
using StorageAPI.Models;

namespace StorageAPI.Midleware
{
    public class ErrorHandlerMidleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMidleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ToException ex)
            {
                await HandleCustomError(context, ex.Message);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }
        private  Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var message = $"{exception.Message} \n{exception.StackTrace}";
            if (exception.InnerException != null)
                message = $"{message}\n {exception.InnerException.Message}\n " +
                    $"{exception.InnerException.StackTrace}";

            return  HandleCustomError(context, message);
        }
        private Task HandleCustomError(HttpContext context, string message, int statusCode = 500)
        {
            var response = new { message = message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}
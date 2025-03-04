using Newtonsoft.Json;
using Smart.Core.Exceptions;

namespace Smart.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {

            var code = StatusCodes.Status500InternalServerError;
            var exMessage = ex.Message;

            switch (ex)
            {
                case EntityNotFoundException:
                    code = StatusCodes.Status404NotFound;
                    break;
            }

            var errorResponse = new
            {
                status = code,
                message = exMessage,
                inner = ex.InnerException?.Message,
                inner2 = ex.InnerException?.InnerException?.Message,
            };

            var result = JsonConvert.SerializeObject(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}

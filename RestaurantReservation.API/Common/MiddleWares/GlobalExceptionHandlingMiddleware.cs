using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.API.Models.ErrorsHandling;
using System.Net;
using System.Text.Json;

namespace RestaurantReservation.API.Common.MiddleWares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                await HandleForeignKeyViolationAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleInternalServerErrorAsync(context, ex);
            }

        }

        private bool IsForeignKeyViolation(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlEx && sqlEx.Number == 547;
        }

        private async Task HandleForeignKeyViolationAsync(HttpContext context, DbUpdateException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var problemDetails = Error.Validate(
                    "Foreign Key constraints violation",
                    "The operation failed due to a foreign key constraint violation"
            );
            string json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
      
        private async Task HandleInternalServerErrorAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var problemDetails = Error.Validate(
                    "Server Error",
                    ex.Message
            );
            string json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        } 
    }
}

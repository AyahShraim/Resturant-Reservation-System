using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantReservation.API.Models.ErrorsHandling;

namespace RestaurantReservation.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                     .Where(m => m.Value.Errors.Any())
                     .SelectMany(m => m.Value.Errors.Select(e => new ErrorModel
                     {
                         FieldName = m.Key,
                         Message = e.ErrorMessage
                     }))
                     .ToList();

                var errorResponse = new ErrorResponse
                {
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();    
        }
    }
}

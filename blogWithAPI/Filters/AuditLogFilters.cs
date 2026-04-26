using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using blogWithAPI.BusinessLayer.Abstract;

namespace blogWithAPI.Filters
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        public string ActionName { get; set; } = string.Empty;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var details = string.Join(", ", context.ActionArguments.Select(a => $"{a.Key}: {a.Value}"));
            
            var resultContext = await next();

            if (resultContext.Exception == null)
            {
                var auditService = context.HttpContext.RequestServices.GetRequiredService<IAuditService>();
                await auditService.LogAsync(ActionName, details, context.HttpContext);
            }
        }
    }
}
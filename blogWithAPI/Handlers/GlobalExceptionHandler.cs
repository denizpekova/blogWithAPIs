using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace blogWithAPI.Handlers
{
    public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Beklenmeyen bir hata oluştu. TraceId: {TraceId}", httpContext.TraceIdentifier);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Sunucu Hatası",
                Detail = "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                Instance = httpContext.Request.Path,
            };

            // İsteğe bağlı: Hata tipine göre özel durumlar eklenebilir
            // if (exception is UnauthorizedAccessException) { problemDetails.Status = StatusCodes.Status401Unauthorized; }

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true; 
        }
    }
}

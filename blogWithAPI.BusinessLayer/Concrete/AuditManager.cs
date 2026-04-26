using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.DataAccessLayer.Concrete;
using blogWithAPI.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace blogWithAPI.BusinessLayer.Concrete
{
    public class AuditManager : IAuditService
    {
        private readonly Context _context;

        public AuditManager(Context context)
        {
            _context = context;
        }

        public async Task LogAsync(string action, string details, HttpContext httpContext)
        {
            var log = new AuditLog
            {
                Action = action,
                Details = details,
                IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                UserName = httpContext.User.Identity?.Name ?? "Anonymous",
                UserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };

            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
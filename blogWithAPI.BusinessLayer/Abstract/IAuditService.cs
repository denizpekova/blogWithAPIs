using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace blogWithAPI.BusinessLayer.Abstract
{
    public interface IAuditService
    {
        Task LogAsync(string action, string details, HttpContext httpContext);
    }
}
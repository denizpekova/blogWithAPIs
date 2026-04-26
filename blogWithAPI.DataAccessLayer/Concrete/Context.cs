using blogWithAPI.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogWithAPI.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

    }
}

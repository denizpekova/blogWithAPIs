using Microsoft.AspNetCore.Identity;

namespace blogWithAPI.Entity.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string ImageUrl { get; set; }
        
    }
}
    
using Microsoft.AspNetCore.Identity;

namespace blog_e18.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

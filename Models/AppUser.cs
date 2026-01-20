using Microsoft.AspNetCore.Identity;

namespace WebApplication_Templeto_F777.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Identity;

namespace TaskSimulation6.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}

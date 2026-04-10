using Microsoft.AspNetCore.Identity;

namespace SmartCampusMVC.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }

        public int StudentNumber { get; set; }

        public string Faculty { get; set; }
        
        public string? ProfileImagePath { get; set; }


    }
}

using Microsoft.AspNetCore.Http;

namespace SmartCampusMVC.ViewModels
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }
        public int StudentNumber { get; set; }
        public string Faculty { get; set; }
        public string Email { get; set; }

        public IFormFile ProfileImage { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
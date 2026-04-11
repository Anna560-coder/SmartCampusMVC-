using System.ComponentModel.DataAnnotations;

namespace SmartCampusMVC.Models
{
    public class Issues
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string IssueType { get; set; } = string.Empty;

        public string? CustomIssue { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public DateTime DateCreated { get; set; }
    }
}
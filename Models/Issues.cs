namespace SmartCampusMVC.Models
{
    public class Issues
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string IssueType { get; set; }

        public string? CustomIssue { get; set; }

        public string? Description { get; set; }

        public string? FilePath { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? AssignedTo { get; set; }
    }
}

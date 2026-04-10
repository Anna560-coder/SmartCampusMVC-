namespace SmartCampusMVC.ViewModels
{
    public class IssueViewModel
    {
        public string IssueType { get; set; }
        public string? CustomIssue { get; set; }
        public string? Description { get; set; }

        public IFormFile? File { get; set; }
    }
}

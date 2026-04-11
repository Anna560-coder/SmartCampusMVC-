namespace SmartCampusMVC.Models
{
    public class ConsultationRequest
    {

        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string Faculty { get; set; }
        public string Description { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedTime { get; set; }
        public string Status { get; set; } = "Pending";
        public string? LecturerNote { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}

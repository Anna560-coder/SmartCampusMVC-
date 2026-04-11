using System;

namespace SmartCampusMVC.Models
{
    public class Consultation
    {
        public int Id { get; set; }

        public string StudentId { get; set; } 

        public string Lecturer { get; set; }

        public DateTime ConsultationDate { get; set; }

        public TimeSpan ConsultationTime { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; } = "Pending"; // default
      
        public string? RejectionReason { get; set; }
    }
}
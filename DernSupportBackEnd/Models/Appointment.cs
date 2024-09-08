using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DernSupportBackEnd.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Location { get; set; } // Location for the appointment

        // Foreign key for SupportRequest
        [ForeignKey("SupportRequest")]
        public int SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }

        // Foreign key for ApplicationUser (technician or customer)
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

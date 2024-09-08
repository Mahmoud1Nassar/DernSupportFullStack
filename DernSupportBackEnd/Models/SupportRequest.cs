using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DernSupportBackEnd.Models
{
    public class SupportRequest
    {
        [Key]
        public int SupportRequestId { get; set; }

        [Required]
        public string IssueType { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        // Foreign key for ApplicationUser (customer who requested support)
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // One-to-many relationship with appointments
        public ICollection<Appointment> Appointments { get; set; }

        // One-to-one relationship with Quote
        public Quote Quote { get; set; }

        // Many-to-many relationship with spare parts
        public ICollection<SparePart> RequiredSpareParts { get; set; }
    }
}

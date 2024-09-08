using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DernSupportBackEnd.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        public string Description { get; set; }

        // Foreign key for SupportRequest
        [ForeignKey("SupportRequest")]
        public int SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }
    }
}

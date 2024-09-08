using System.ComponentModel.DataAnnotations;

namespace DernSupportBackEnd.Models
{
    public class SparePart
    {
        [Key]
        public int SparePartId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StockLevel { get; set; }

        [Required]
        public decimal Cost { get; set; }

        // Many-to-many relationship with SupportRequest
        public ICollection<SupportRequest> SupportRequests { get; set; }
    }
}

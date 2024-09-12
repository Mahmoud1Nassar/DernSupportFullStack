using System.ComponentModel.DataAnnotations;

public class Quote
{
    [Key]
    public int QuoteId { get; set; }
    public decimal TotalCost { get; set; }
    public string Description { get; set; }

    // Foreign key for SupportRequest
    public int SupportRequestId { get; set; }

    // Navigation property for SupportRequest
    public SupportRequest SupportRequest { get; set; }
}

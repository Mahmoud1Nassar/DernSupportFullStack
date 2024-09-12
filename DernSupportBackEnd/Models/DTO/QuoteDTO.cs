namespace DernSupportBackEnd.Models.DTO
{
    public class QuoteDTO
    {
        public int QuoteId { get; set; }
        public decimal TotalCost { get; set; }
        public string Description { get; set; }
        public int SupportRequestId { get; set; }  // Link to SupportRequest
    }
}

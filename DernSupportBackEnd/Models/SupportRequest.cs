public class SupportRequest
{
    public int SupportRequestId { get; set; }
    public string IssueType { get; set; }
    public string Description { get; set; }
    public DateTime RequestDate { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    // Collection of SpareParts related to this SupportRequest
    public ICollection<SupportRequestSparePart> SupportRequestSpareParts { get; set; } = new List<SupportRequestSparePart>();

    public ICollection<Quote> Quotes { get; set; } = new List<Quote>(); // One SupportRequest can have multiple Quotes
}

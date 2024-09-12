namespace DernSupportBackEnd.Models.DTO
{
    public class SupportRequestDTO
    {
        public int SupportRequestId { get; set; }
        public string IssueType { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserId { get; set; }  // Link to ApplicationUser
        public string? UserEmail { get; internal set; }
    }
}

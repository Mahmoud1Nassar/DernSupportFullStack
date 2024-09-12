using DernSupportBackEnd.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<SupportRequest> SupportRequests { get; set; }
}

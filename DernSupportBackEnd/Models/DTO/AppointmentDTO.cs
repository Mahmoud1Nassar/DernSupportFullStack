using System.Text.Json.Serialization;

namespace DernSupportBackEnd.Models.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }  // Link to ApplicationUser
        public string? UserPhone { get; set; }  // The phone number entered by the user
  
        public string? UserName { get;  set; }
    }
}

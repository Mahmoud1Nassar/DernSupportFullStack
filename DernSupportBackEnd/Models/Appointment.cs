public class Appointment
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Location { get; set; }

    // Foreign key and navigation property for User
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    // Navigation property for related quotes
    public ICollection<Quote> Quotes { get; set; }  // Related quotes

   
}

using DernSupportBackEnd.Repositories.Interfaces;
using DernSupportBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Services
{
    public class AppointmentService : IAppointment
    {
        private readonly DernDbContext _context;

        public AppointmentService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            // Include the related ApplicationUser entity to access the UserName and PhoneNumber
            var appointments = await _context.Appointments
                                             .Include(a => a.User)  // Eagerly load the User
                                             .ToListAsync();

            return appointments.Select(a => new AppointmentDTO
            {
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                Location = a.Location,
                UserId = a.UserId,
                UserName = a.User.UserName,
                UserPhone = a.User.PhoneNumber  // Access the User's phone number
            }).ToList();
        }


        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return null;

            return new AppointmentDTO
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                Location = appointment.Location,
                UserId = appointment.UserId
            };
        }

        public async Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            // Fetch the user by UserId from the ApplicationUser (AspNetUsers) table
            var user = await _context.Users.FindAsync(appointmentDTO.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update the user's phone number with the provided phone number
            user.PhoneNumber = appointmentDTO.UserPhone;

            // Create a new appointment
            var appointment = new Appointment
            {
                AppointmentDate = appointmentDTO.AppointmentDate,
                Location = appointmentDTO.Location,
                UserId = appointmentDTO.UserId,
          
            };

            _context.Appointments.Add(appointment);

            // Update the user's phone number in the database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Set the created appointment's ID in the DTO and return it
            appointmentDTO.AppointmentId = appointment.AppointmentId;
            return appointmentDTO;
        }


        public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            // Fetch the appointment by ID
            var appointment = await _context.Appointments.FindAsync(appointmentDTO.AppointmentId);

            if (appointment == null)
                return null;

            // Fetch the user by UserId from the ApplicationUser (AspNetUsers) table
            var user = await _context.Users.FindAsync(appointmentDTO.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update the user's phone number with the provided phone number
            user.PhoneNumber = appointmentDTO.UserPhone;

            // Update the appointment details
            appointment.AppointmentDate = appointmentDTO.AppointmentDate;
            appointment.Location = appointmentDTO.Location;
            appointment.UserId = appointmentDTO.UserId;

            // Save changes to both the user (phone number) and the appointment
            _context.Users.Update(user);  // Update the user's phone number
            _context.Appointments.Update(appointment);  // Update the appointment details
            await _context.SaveChangesAsync();

            return appointmentDTO;
        }


        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface IAppointment
    {
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();
        Task<AppointmentDTO> GetAppointmentByIdAsync(int id);
        Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDTO);
        Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}

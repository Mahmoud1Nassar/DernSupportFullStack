using DernSupportBackEnd.Models.DTO;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Technician,Customer")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentService;

        public AppointmentController(IAppointment appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            return await _appointmentService.GetAllAppointmentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentService.GetAppointmentByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            return await _appointmentService.CreateAppointmentAsync(appointmentDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
         return await _appointmentService.UpdateAppointmentAsync(appointmentDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            return await _appointmentService.DeleteAppointmentAsync(id);
           
        }
    }
}

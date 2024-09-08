using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentService;

        public AppointmentController(IAppointment appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.AppointmentId }, createdAppointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId) return BadRequest();
            var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(appointment);
            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

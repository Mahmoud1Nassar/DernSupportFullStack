using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/appointment (Admin, Technician, and Customer can view appointments)
        [HttpGet]
        [Authorize(Roles = "Admin,Technician,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET: api/appointment/{id} (Admin, Technician, and Customer can view a specific appointment)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Technician,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        // POST: api/appointment (Only Admin can create appointments)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.AppointmentId }, createdAppointment);
        }

        // PUT: api/appointment/{id} (Admin and Technician can update appointments)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> Update(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId) return BadRequest();
            var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(appointment);
            return Ok(updatedAppointment);
        }

        // DELETE: api/appointment/{id} (Only Admin can delete appointments)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

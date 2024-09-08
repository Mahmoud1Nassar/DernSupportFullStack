using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestController : ControllerBase
    {
        private readonly ISupportRequest _supportRequestService;

        public SupportRequestController(ISupportRequest supportRequestService)
        {
            _supportRequestService = supportRequestService;
        }

        // GET: api/supportrequest (Admin, Technician, and Customer can view support requests)
        [HttpGet]
        [Authorize(Roles = "Admin,Technician,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var supportRequests = await _supportRequestService.GetAllSupportRequestsAsync();
            return Ok(supportRequests);
        }

        // GET: api/supportrequest/{id} (Admin, Technician, and Customer can view a specific support request)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Technician,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var supportRequest = await _supportRequestService.GetSupportRequestByIdAsync(id);
            if (supportRequest == null) return NotFound();
            return Ok(supportRequest);
        }

        // POST: api/supportrequest (Admin and Customer can create support requests)
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create(SupportRequest supportRequest)
        {
            var createdSupportRequest = await _supportRequestService.CreateSupportRequestAsync(supportRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdSupportRequest.SupportRequestId }, createdSupportRequest);
        }

        // PUT: api/supportrequest/{id} (Admin and Technician can update support requests)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> Update(int id, SupportRequest supportRequest)
        {
            if (id != supportRequest.SupportRequestId) return BadRequest();
            var updatedRequest = await _supportRequestService.UpdateSupportRequestAsync(supportRequest);
            return Ok(updatedRequest);
        }

        // DELETE: api/supportrequest/{id} (Only Admin can delete support requests)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _supportRequestService.DeleteSupportRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

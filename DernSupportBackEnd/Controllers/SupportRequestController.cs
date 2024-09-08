using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supportRequests = await _supportRequestService.GetAllSupportRequestsAsync();
            return Ok(supportRequests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supportRequest = await _supportRequestService.GetSupportRequestByIdAsync(id);
            if (supportRequest == null) return NotFound();
            return Ok(supportRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupportRequest supportRequest)
        {
            var createdSupportRequest = await _supportRequestService.CreateSupportRequestAsync(supportRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdSupportRequest.SupportRequestId }, createdSupportRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupportRequest supportRequest)
        {
            if (id != supportRequest.SupportRequestId) return BadRequest();
            var updatedRequest = await _supportRequestService.UpdateSupportRequestAsync(supportRequest);
            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _supportRequestService.DeleteSupportRequestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

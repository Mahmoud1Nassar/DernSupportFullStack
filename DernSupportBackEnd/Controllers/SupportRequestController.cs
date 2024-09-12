using DernSupportBackEnd.Models.DTO;
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

        [HttpGet]
        public async Task<IEnumerable<SupportRequestDTO>> GetAllSupportRequestsAsync()
        {
            return await _supportRequestService.GetAllSupportRequestsAsync();
        }

        [HttpGet("{id}")]
        public async Task<SupportRequestDTO> GetSupportRequestByIdAsync(int id)
        {
            return await _supportRequestService.GetSupportRequestByIdAsync(id);
        }

        [HttpPost]
        
        public async Task<SupportRequestDTO> CreateSupportRequestAsync(SupportRequestDTO request)
        {
            return await _supportRequestService.CreateSupportRequestAsync(request);
        }

        [HttpPut("{id}")]
        public async Task<SupportRequestDTO> UpdateSupportRequestAsync(SupportRequestDTO request)
        {
            return await _supportRequestService.UpdateSupportRequestAsync(request);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSupportRequestAsync(int id)
        {
           return await _supportRequestService.DeleteSupportRequestAsync(id);
        }
    }
}

using DernSupportBackEnd.Models.DTO;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartController : ControllerBase
    {
        private readonly ISparePart _sparePartService;

        public SparePartController(ISparePart sparePartService)
        {
            _sparePartService = sparePartService;
        }

        [HttpGet]

        public async Task<IEnumerable<SparePartDTO>> GetAllSparePartsAsync()
        {
            return await _sparePartService.GetAllSparePartsAsync();
        }

        [HttpGet("{id}")]

        public async Task<SparePartDTO> GetSparePartByIdAsync(int id)
        {
            return await _sparePartService.GetSparePartByIdAsync(id);
        }

        [HttpPost]
        public async Task<SparePartDTO> CreateSparePartAsync(SparePartDTO sparePartDTO)
        {
            return await _sparePartService.CreateSparePartAsync(sparePartDTO);
        }

        [HttpPut("{id}")]
        public async Task<SparePartDTO> UpdateSparePartAsync(SparePartDTO sparePartDTO)
        {
            return await _sparePartService.UpdateSparePartAsync(sparePartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSparePartAsync(int id)
        {
            return await _sparePartService.DeleteSparePartAsync(id);
        }
    }
}

using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var spareParts = await _sparePartService.GetAllSparePartsAsync();
            return Ok(spareParts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sparePart = await _sparePartService.GetSparePartByIdAsync(id);
            if (sparePart == null) return NotFound();
            return Ok(sparePart);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SparePart sparePart)
        {
            var createdSparePart = await _sparePartService.CreateSparePartAsync(sparePart);
            return CreatedAtAction(nameof(GetById), new { id = createdSparePart.SparePartId }, createdSparePart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SparePart sparePart)
        {
            if (id != sparePart.SparePartId) return BadRequest();
            var updatedSparePart = await _sparePartService.UpdateSparePartAsync(sparePart);
            return Ok(updatedSparePart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sparePartService.DeleteSparePartAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

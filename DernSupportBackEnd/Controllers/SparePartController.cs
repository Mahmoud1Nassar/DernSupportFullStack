using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Technician")] // Only Admin and Technician have access to spare parts
    public class SparePartController : ControllerBase
    {
        private readonly ISparePart _sparePartService;

        public SparePartController(ISparePart sparePartService)
        {
            _sparePartService = sparePartService;
        }

        // GET: api/sparepart (Admin and Technician can view spare parts)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var spareParts = await _sparePartService.GetAllSparePartsAsync();
            return Ok(spareParts);
        }

        // GET: api/sparepart/{id} (Admin and Technician can view a specific spare part)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sparePart = await _sparePartService.GetSparePartByIdAsync(id);
            if (sparePart == null) return NotFound();
            return Ok(sparePart);
        }

        // POST: api/sparepart (Only Admin can create spare parts)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SparePart sparePart)
        {
            var createdSparePart = await _sparePartService.CreateSparePartAsync(sparePart);
            return CreatedAtAction(nameof(GetById), new { id = createdSparePart.SparePartId }, createdSparePart);
        }

        // PUT: api/sparepart/{id} (Admin and Technician can update spare parts)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SparePart sparePart)
        {
            if (id != sparePart.SparePartId) return BadRequest();
            var updatedSparePart = await _sparePartService.UpdateSparePartAsync(sparePart);
            return Ok(updatedSparePart);
        }

        // DELETE: api/sparepart/{id} (Only Admin can delete spare parts)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sparePartService.DeleteSparePartAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

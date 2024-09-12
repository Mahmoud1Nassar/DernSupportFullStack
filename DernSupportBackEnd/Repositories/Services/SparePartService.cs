using DernSupportBackEnd.Repositories.Interfaces;
using DernSupportBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Services
{
    public class SparePartService : ISparePart
    {
        private readonly DernDbContext _context;

        public SparePartService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SparePartDTO>> GetAllSparePartsAsync()
        {
            var spareParts = await _context.SpareParts.ToListAsync();

            return spareParts.Select(sp => new SparePartDTO
            {
                SparePartId = sp.SparePartId,
                Name = sp.Name,
                StockLevel = sp.StockLevel,
                Cost = sp.Cost
            }).ToList();
        }

        public async Task<SparePartDTO> GetSparePartByIdAsync(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);

            if (sparePart == null)
                return null;

            return new SparePartDTO
            {
                SparePartId = sparePart.SparePartId,
                Name = sparePart.Name,
                StockLevel = sparePart.StockLevel,
                Cost = sparePart.Cost
            };
        }

        public async Task<SparePartDTO> CreateSparePartAsync(SparePartDTO sparePartDTO)
        {
            var sparePart = new SparePart
            {
                Name = sparePartDTO.Name,
                StockLevel = sparePartDTO.StockLevel,
                Cost = sparePartDTO.Cost
            };

            _context.SpareParts.Add(sparePart);
            await _context.SaveChangesAsync();

            sparePartDTO.SparePartId = sparePart.SparePartId;
            return sparePartDTO;
        }

        public async Task<SparePartDTO> UpdateSparePartAsync(SparePartDTO sparePartDTO)
        {
            var sparePart = await _context.SpareParts.FindAsync(sparePartDTO.SparePartId);

            if (sparePart == null)
                return null;

            sparePart.Name = sparePartDTO.Name;
            sparePart.StockLevel = sparePartDTO.StockLevel;
            sparePart.Cost = sparePartDTO.Cost;

            _context.SpareParts.Update(sparePart);
            await _context.SaveChangesAsync();

            return sparePartDTO;
        }

        public async Task<bool> DeleteSparePartAsync(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);

            if (sparePart == null)
                return false;

            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using DernSupportBackEnd.Data;
using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DernSupportBackEnd.Repositories.Services
{
    public class SparePartService : ISparePart
    {
        private readonly DernDbContext _context;

        public SparePartService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SparePart>> GetAllSparePartsAsync()
        {
            return await _context.SpareParts.ToListAsync();
        }

        public async Task<SparePart> GetSparePartByIdAsync(int id)
        {
            return await _context.SpareParts.FirstOrDefaultAsync(sp => sp.SparePartId == id);
        }

        public async Task<SparePart> CreateSparePartAsync(SparePart sparePart)
        {
            _context.SpareParts.Add(sparePart);
            await _context.SaveChangesAsync();
            return sparePart;
        }

        public async Task<SparePart> UpdateSparePartAsync(SparePart sparePart)
        {
            _context.SpareParts.Update(sparePart);
            await _context.SaveChangesAsync();
            return sparePart;
        }

        public async Task<bool> DeleteSparePartAsync(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null) return false;

            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

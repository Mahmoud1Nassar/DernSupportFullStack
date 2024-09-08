using DernSupportBackEnd.Data;
using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DernSupportBackEnd.Repositories.Services
{
    public class SupportRequestService : ISupportRequest
    {
        private readonly DernDbContext _context;

        public SupportRequestService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupportRequest>> GetAllSupportRequestsAsync()
        {
            return await _context.SupportRequests
                .Include(sr => sr.Appointments)
                .Include(sr => sr.RequiredSpareParts)
                .Include(sr => sr.Quote)
                .ToListAsync();
        }

        public async Task<SupportRequest> GetSupportRequestByIdAsync(int id)
        {
            return await _context.SupportRequests
                .Include(sr => sr.Appointments)
                .Include(sr => sr.RequiredSpareParts)
                .Include(sr => sr.Quote)
                .FirstOrDefaultAsync(sr => sr.SupportRequestId == id);
        }

        public async Task<SupportRequest> CreateSupportRequestAsync(SupportRequest request)
        {
            _context.SupportRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<SupportRequest> UpdateSupportRequestAsync(SupportRequest request)
        {
            _context.SupportRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteSupportRequestAsync(int id)
        {
            var request = await _context.SupportRequests.FindAsync(id);
            if (request == null) return false;

            _context.SupportRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

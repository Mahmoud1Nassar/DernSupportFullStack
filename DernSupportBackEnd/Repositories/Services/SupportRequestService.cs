using DernSupportBackEnd.Repositories.Interfaces;
using DernSupportBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Services
{
    public class SupportRequestService : ISupportRequest
    {
        private readonly DernDbContext _context;

        public SupportRequestService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupportRequestDTO>> GetAllSupportRequestsAsync()
        {
            var supportRequests = await _context.SupportRequests
                                                .Include(sr => sr.User) // Include the User data
                                                .ToListAsync();

            return supportRequests.Select(sr => new SupportRequestDTO
            {
                SupportRequestId = sr.SupportRequestId,
                IssueType = sr.IssueType,
                Description = sr.Description,
                RequestDate = sr.RequestDate,
                UserId = sr.UserId,
                UserEmail = sr.User.Email // Include the user's email
            }).ToList();
        }

        public async Task<SupportRequestDTO> GetSupportRequestByIdAsync(int id)
        {
            var supportRequest = await _context.SupportRequests
                                                .Include(sr => sr.User) // Include the User data
                                                .FirstOrDefaultAsync(sr => sr.SupportRequestId == id);

            if (supportRequest == null)
                return null;

            return new SupportRequestDTO
            {
                SupportRequestId = supportRequest.SupportRequestId,
                IssueType = supportRequest.IssueType,
                Description = supportRequest.Description,
                RequestDate = supportRequest.RequestDate,
                UserId = supportRequest.UserId,
                UserEmail = supportRequest.User.Email // Include the user's email
            };
        }

        public async Task<SupportRequestDTO> CreateSupportRequestAsync(SupportRequestDTO supportRequestDTO)
        {
            var supportRequest = new SupportRequest
            {
                IssueType = supportRequestDTO.IssueType,
                Description = supportRequestDTO.Description,
                RequestDate = supportRequestDTO.RequestDate,
                UserId = supportRequestDTO.UserId
            };

            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync();

            supportRequestDTO.SupportRequestId = supportRequest.SupportRequestId;
            return supportRequestDTO;
        }

        public async Task<SupportRequestDTO> UpdateSupportRequestAsync(SupportRequestDTO supportRequestDTO)
        {
            var supportRequest = await _context.SupportRequests.FindAsync(supportRequestDTO.SupportRequestId);

            if (supportRequest == null)
                return null;

            supportRequest.IssueType = supportRequestDTO.IssueType;
            supportRequest.Description = supportRequestDTO.Description;
            supportRequest.RequestDate = supportRequestDTO.RequestDate;
            supportRequest.UserId = supportRequestDTO.UserId;

            _context.SupportRequests.Update(supportRequest);
            await _context.SaveChangesAsync();

            return supportRequestDTO;
        }

        public async Task<bool> DeleteSupportRequestAsync(int id)
        {
            var supportRequest = await _context.SupportRequests.FindAsync(id);

            if (supportRequest == null)
                return false;

            _context.SupportRequests.Remove(supportRequest);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

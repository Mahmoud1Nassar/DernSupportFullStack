using DernSupportBackEnd.Models;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface ISupportRequest
    {
        Task<IEnumerable<SupportRequest>> GetAllSupportRequestsAsync();
        Task<SupportRequest> GetSupportRequestByIdAsync(int id);
        Task<SupportRequest> CreateSupportRequestAsync(SupportRequest request);
        Task<SupportRequest> UpdateSupportRequestAsync(SupportRequest request);
        Task<bool> DeleteSupportRequestAsync(int id);
    }
}

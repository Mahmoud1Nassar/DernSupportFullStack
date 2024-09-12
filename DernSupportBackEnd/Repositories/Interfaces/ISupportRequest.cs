using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface ISupportRequest
    {
        Task<IEnumerable<SupportRequestDTO>> GetAllSupportRequestsAsync();
        Task<SupportRequestDTO> GetSupportRequestByIdAsync(int id);
        Task<SupportRequestDTO> CreateSupportRequestAsync(SupportRequestDTO supportRequestDTO);
        Task<SupportRequestDTO> UpdateSupportRequestAsync(SupportRequestDTO supportRequestDTO);
        Task<bool> DeleteSupportRequestAsync(int id);
    }
}

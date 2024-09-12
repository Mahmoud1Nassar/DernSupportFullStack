using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface ISparePart
    {
        Task<IEnumerable<SparePartDTO>> GetAllSparePartsAsync();
        Task<SparePartDTO> GetSparePartByIdAsync(int id);
        Task<SparePartDTO> CreateSparePartAsync(SparePartDTO sparePartDTO);
        Task<SparePartDTO> UpdateSparePartAsync(SparePartDTO sparePartDTO);
        Task<bool> DeleteSparePartAsync(int id);
    }
}

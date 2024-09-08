using DernSupportBackEnd.Models;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface ISparePart
    {
        Task<IEnumerable<SparePart>> GetAllSparePartsAsync();
        Task<SparePart> GetSparePartByIdAsync(int id);
        Task<SparePart> CreateSparePartAsync(SparePart sparePart);
        Task<SparePart> UpdateSparePartAsync(SparePart sparePart);
        Task<bool> DeleteSparePartAsync(int id);
    }
}

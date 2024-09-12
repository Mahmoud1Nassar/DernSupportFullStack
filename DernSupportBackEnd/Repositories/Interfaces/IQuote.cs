using DernSupportBackEnd.Models.DTO;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface IQuote
    {
        Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync();
        Task<QuoteDTO> GetQuoteByIdAsync(int id);
        Task<QuoteDTO> CreateQuoteAsync(QuoteDTO quoteDTO);
        Task<QuoteDTO> UpdateQuoteAsync(QuoteDTO quoteDTO);
        Task<bool> DeleteQuoteAsync(int id);
    }
}

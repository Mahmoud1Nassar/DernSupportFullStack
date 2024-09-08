using DernSupportBackEnd.Models;

namespace DernSupportBackEnd.Repositories.Interfaces
{
    public interface IQuote
    {
        Task<IEnumerable<Quote>> GetAllQuotesAsync();
        Task<Quote> GetQuoteByIdAsync(int id);
        Task<Quote> CreateQuoteAsync(Quote quote);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task<bool> DeleteQuoteAsync(int id);
    }
}

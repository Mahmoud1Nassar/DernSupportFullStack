using DernSupportBackEnd.Data;
using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DernSupportBackEnd.Repositories.Services
{
    public class QuoteService : IQuote
    {
        private readonly DernDbContext _context;

        public QuoteService(DernDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quote>> GetAllQuotesAsync()
        {
            return await _context.Quotes.Include(q => q.SupportRequest).ToListAsync();
        }

        public async Task<Quote> GetQuoteByIdAsync(int id)
        {
            return await _context.Quotes.Include(q => q.SupportRequest).FirstOrDefaultAsync(q => q.QuoteId == id);
        }

        public async Task<Quote> CreateQuoteAsync(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return quote;
        }

        public async Task<Quote> UpdateQuoteAsync(Quote quote)
        {
            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync();
            return quote;
        }

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null) return false;

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using DernSupportBackEnd.Data;
using DernSupportBackEnd.Models.DTO;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class QuoteService : IQuote
{
    private readonly DernDbContext _context;

    public QuoteService(DernDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync()
    {
        var quotes = await _context.Quotes.ToListAsync();

        return quotes.Select(q => new QuoteDTO
        {
            QuoteId = q.QuoteId,
            TotalCost = q.TotalCost,
            Description = q.Description,
            SupportRequestId = q.SupportRequestId
        }).ToList();
    }

    public async Task<QuoteDTO> GetQuoteByIdAsync(int id)
    {
        var quote = await _context.Quotes.FindAsync(id);

        if (quote == null)
            return null;

        return new QuoteDTO
        {
            QuoteId = quote.QuoteId,
            TotalCost = quote.TotalCost,
            Description = quote.Description,
            SupportRequestId = quote.SupportRequestId
        };
    }

    public async Task<QuoteDTO> CreateQuoteAsync(QuoteDTO quoteDTO)
    {
        // Fetch the support request (without fetching spare parts or recalculating costs)
        var supportRequest = await _context.SupportRequests
            .FirstOrDefaultAsync(sr => sr.SupportRequestId == quoteDTO.SupportRequestId);

        if (supportRequest == null)
        {
            throw new Exception("Support request not found.");
        }

        // Trust the totalCost calculated on the frontend and passed in quoteDTO
        var quote = new Quote
        {
            TotalCost = quoteDTO.TotalCost, // Use the total cost from the frontend
            Description = quoteDTO.Description,
            SupportRequestId = quoteDTO.SupportRequestId
        };

        _context.Quotes.Add(quote);
        await _context.SaveChangesAsync();

        quoteDTO.QuoteId = quote.QuoteId;
        return quoteDTO;
    }


    public async Task<QuoteDTO> UpdateQuoteAsync(QuoteDTO quoteDTO)
    {
        var quote = await _context.Quotes.FindAsync(quoteDTO.QuoteId);

        if (quote == null)
            return null;

        // Fetch the support request with related spare parts
        var supportRequest = await _context.SupportRequests
            .Include(sr => sr.SupportRequestSpareParts)
            .ThenInclude(srsp => srsp.SparePart)
            .FirstOrDefaultAsync(sr => sr.SupportRequestId == quoteDTO.SupportRequestId);

        if (supportRequest == null)
        {
            throw new Exception("Support request not found.");
        }

        // Recalculate total cost based on the updated spare parts used
        decimal totalCost = supportRequest.SupportRequestSpareParts
            .Sum(srsp => srsp.SparePart.Cost);

        quote.TotalCost = totalCost;
        quote.Description = quoteDTO.Description;
        quote.SupportRequestId = quoteDTO.SupportRequestId;

        _context.Quotes.Update(quote);
        await _context.SaveChangesAsync();

        quoteDTO.TotalCost = totalCost;
        return quoteDTO;
    }

    public async Task<bool> DeleteQuoteAsync(int id)
    {
        var quote = await _context.Quotes.FindAsync(id);

        if (quote == null)
            return false;

        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();

        return true;
    }
}

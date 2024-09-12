using DernSupportBackEnd.Models.DTO;
using DernSupportBackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DernSupportBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class QuoteController : ControllerBase
    {
        private readonly IQuote _quoteService;

        public QuoteController(IQuote quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync()
        {
            return await _quoteService.GetAllQuotesAsync();
        }

        [HttpGet("{id}")]
        public async Task<QuoteDTO> GetQuoteByIdAsync(int id)
        {
            return await _quoteService.GetQuoteByIdAsync(id);
        }

        [HttpPost]

        public async Task<QuoteDTO> CreateQuoteAsync(QuoteDTO quoteDTO)
        {
            return await _quoteService.CreateQuoteAsync(quoteDTO);
        }

        [HttpPut("{id}")]

        public async Task<QuoteDTO> UpdateQuoteAsync(QuoteDTO quoteDTO)
        {
            return await _quoteService.UpdateQuoteAsync(quoteDTO);
        }

        [HttpDelete("{id}")]

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            return await _quoteService.DeleteQuoteAsync(id);
        }
    }
}

using DernSupportBackEnd.Models;
using DernSupportBackEnd.Repositories.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var quotes = await _quoteService.GetAllQuotesAsync();
            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);
            if (quote == null) return NotFound();
            return Ok(quote);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Quote quote)
        {
            var createdQuote = await _quoteService.CreateQuoteAsync(quote);
            return CreatedAtAction(nameof(GetById), new { id = createdQuote.QuoteId }, createdQuote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Quote quote)
        {
            if (id != quote.QuoteId) return BadRequest();
            var updatedQuote = await _quoteService.UpdateQuoteAsync(quote);
            return Ok(updatedQuote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _quoteService.DeleteQuoteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

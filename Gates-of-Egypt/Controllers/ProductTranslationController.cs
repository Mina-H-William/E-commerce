using Gates_of_Egypt.Dtos;
using Gates_of_Egypt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gates_of_Egypt.Controllers
{
    [Route("api/products/{productId}/translations")]
    [ApiController]
    public class ProductTranslationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductTranslationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/products/{productId}/translations
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTranslation(Guid productId, [FromBody] ProductTranslationDto request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            var translation = new ProductTranslation
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                LanguageCode = request.LanguageCode,
                Name = request.Name,
                Description = request.Description
            };

            _context.ProductTranslations.Add(translation);
            await _context.SaveChangesAsync();

            return Ok(translation);
        }

        // GET: api/products/{productId}/{language}
        [HttpGet("{language}")]
        public async Task<IActionResult> GetTranslation(Guid productId, string language)
        {
            var translation = await _context.ProductTranslations
                .FirstOrDefaultAsync(t => t.ProductId == productId && t.LanguageCode == language);

            if (translation == null)
                return NotFound(new { message = "Translation not found" });

            return Ok(translation);
        }
    }
}

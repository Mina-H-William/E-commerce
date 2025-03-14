using Gates_of_Egypt.Dtos;
using Gates_of_Egypt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gates_of_Egypt.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/products
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Translations)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Translations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PATCH: api/products/{id}
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDto request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Price = request.Price;
            product.Quantity = request.Quantity;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product removed successfully" });
        }
    }
}

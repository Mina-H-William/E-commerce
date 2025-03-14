using Gates_of_Egypt.Dtos;
using Gates_of_Egypt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gates_of_Egypt.Controllers
{
    [Route("api/cart/{userId}")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/cart/{userId}/items - Add product to cart
        [HttpPost("items")]
        [Authorize]
        public async Task<IActionResult> AddToCart(Guid userId, [FromBody] CartItemDto request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { Id = Guid.NewGuid(), UserId = userId, CreatedAt = DateTime.UtcNow };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (cartItem == null)
            {
                cartItem = new CartItem { Id = Guid.NewGuid(), CartId = cart.Id, ProductId = request.ProductId, Quantity = request.Quantity };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(cartItem);
        }

        // GET: api/cart/{userId} - Get cart details
        [HttpGet]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return NotFound(new { message = "Cart not found" });

            return Ok(cart);
        }

        // DELETE: api/cart/{userId}/items/{productId} - Remove a product from the cart
        [HttpDelete("items/{productId}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(Guid userId, Guid productId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return NotFound(new { message = "Cart not found" });

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
                return NotFound(new { message = "Product not in cart" });

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product removed from cart" });
        }
    }
}

using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly ProductService _productService;

        public CartController(CartService cartService, ProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetUserCartAsync(userId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(int userId, int productId, int quantity)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null) return NotFound("Product not found");

            await _cartService.AddItemToCart(userId, productId, quantity);
            return Ok("Item added to cart");
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            await _cartService.RemoveItemFromCart(cartItemId);
            return NoContent();
        }
    }
}

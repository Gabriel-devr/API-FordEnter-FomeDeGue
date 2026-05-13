
using API_Curso_Angular.Services.Carts;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Controllers {

    [ApiController]
    [Route("v1/[controller]")]
    public class CartController :ControllerBase{

        private readonly ICartService _cartService;

        public CartController(ICartService cartService) {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("{clientId}/items")]
        public async Task<IActionResult> AddItem(long clientId, [FromQuery] long productId, [FromQuery] int quantidade = 1) {

            var cart = await _cartService.AddItemToCart(clientId, productId, quantidade);
            return Ok(cart);
        }

        [HttpGet]
        [Route("{clienteId}")]
        public async Task<IActionResult> GetCart(long clienteId) {
            var cart = await _cartService.GetCart(clienteId);
            if (cart == null) {
                return NotFound("Carrinho não encontrado para este usuário.");
            }
            return Ok(cart);
        }

        [HttpPost]
        [Route("{clientId}/items/reduce")]
        public async Task<IActionResult> ReduceItem(long clientId, long productId, int quantidade = 1) {
            var cart = await _cartService.ReduceItemCart(clientId, productId, quantidade);

            if (cart == null) {
                return NotFound("O item já não existe mais.");
            }

            return Ok(cart);
        }

        [HttpDelete]
        [Route("{clientId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(long clientId, long productId) {
            var cart = await _cartService.RemoveItemCart(clientId, productId);

            if (cart == null) {
                return NotFound("Carrinho não encontrado para este utilizador.");
            }

            return Ok(cart);
        }

    }
}

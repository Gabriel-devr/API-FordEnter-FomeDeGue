using API_Curso_Angular.Models;
using API_Curso_Angular.Repositories.Carts;
using API_Curso_Angular.Repositories.Products;

namespace API_Curso_Angular.Services.Carts {
    public class CartService : ICartService {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository prodRepository) {
            _cartRepository = cartRepository;
            _productRepository = prodRepository;
        }

        public async Task<Carrinho> AddItemToCart(long clienteId, long produtoId, int quantidade) {
            var produto = await _productRepository.ProdutoPorId(produtoId);
            if (produto == null) {
                throw new Exception("Produto não encontrado no sistema.");
            }

            var cart = await _cartRepository.GetCartByClientId(clienteId);

            if (cart == null) {
                cart = new Carrinho
                {
                    ClienteId = clienteId,
                    CriadoEm = DateTime.UtcNow,
                    AtualizadoEm = DateTime.UtcNow,
                    Items = new List<ItemCarrinho>()
                };
                await _cartRepository.CreateCart(cart);
            }

            var itemExiste = cart.Items.FirstOrDefault(x => x.ProdutoId == produtoId);
                
            if (itemExiste != null) {
                itemExiste.Quantidade += quantidade;
            }
            else {
                cart.Items.Add(new ItemCarrinho { ProdutoId = produtoId, Quantidade = quantidade, Produto = produto});
            }

            cart.AtualizadoEm = DateTime.UtcNow;

            cart.ValorTotal = cart.Items.Sum(x => x.Quantidade * x.Produto.Valor);

            await _cartRepository.UpdateCart(cart);
                
            return cart;
        }

        public async Task<Carrinho> ReduceItemCart(long clienteId, long produtoId, int quantidade) {
            var cart = await _cartRepository.GetCartByClientId(clienteId);
            if (cart == null) {
                return null;
            }

            var item = cart.Items.FirstOrDefault(x => x.ProdutoId == produtoId);
            
            if (item == null) {
                return null;
            }
            item.Quantidade -= quantidade;

            if (item.Quantidade <= 0) {
                cart.Items.Remove(item);
            }

            cart.AtualizadoEm = DateTime.UtcNow;
            cart.ValorTotal = cart.Items.Sum(x => x.Quantidade*x.Produto.Valor);

            await _cartRepository.UpdateCart(cart);

            //aplicação estática

            return cart;
        }

        public async Task<Carrinho?> GetCart(long clientId) {
            var cart = await _cartRepository.GetCartByClientId(clientId);
            return cart;
        }

        public async Task<Carrinho> RemoveItemCart(long clienteId, long produtoId) {
            var cart = await _cartRepository.GetCartByClientId(clienteId);
            if (cart == null) {
                return null;
            }

            var item = cart.Items.FirstOrDefault(x=>x.ProdutoId == produtoId);
            if (item != null) {
                cart.Items.Remove(item);
                cart.AtualizadoEm = DateTime.UtcNow;
                cart.ValorTotal = cart.Items.Sum(x=>x.Quantidade * (x.Produto?.Valor ?? 0));

                await _cartRepository.UpdateCart(cart);
            }

            return cart;

        }
    }
}

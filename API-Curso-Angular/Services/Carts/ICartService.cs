using API_Curso_Angular.Models;

namespace API_Curso_Angular.Services.Carts {
    public interface ICartService {
        Task<Carrinho> AddItemToCart(long clienteId, long produtoId, int quantidade);
        Task<Carrinho?> GetCart(long clientId);
        Task<Carrinho> ReduceItemCart(long clienteId, long produtoId, int quantidade);
        Task<Carrinho> RemoveItemCart(long clienteId, long produtoId);
    }
}

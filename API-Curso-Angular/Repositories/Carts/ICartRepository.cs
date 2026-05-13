using API_Curso_Angular.Models;

namespace API_Curso_Angular.Repositories.Carts {
    public interface ICartRepository {

        Task<Carrinho?> GetCartByClientId(long clientId);
        Task CreateCart(Carrinho carrinho);
        Task UpdateCart(Carrinho carrinho);
    }
}

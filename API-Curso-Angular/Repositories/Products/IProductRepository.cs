using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Products;
using API_Curso_Angular.Models;

namespace API_Curso_Angular.Repositories.Products
{
    public interface IProductRepository
    {
         Task<Produto?> ProdutoPorId(long id);
         Task<IEnumerable<Produto>> ListarProdutos();
         Task<Produto> CriarProduto(Produto produto);
         Task<Produto> DeletarProduto(Produto produto);
         Task<Produto> AtualizarProduto(Produto produto);
    }
}

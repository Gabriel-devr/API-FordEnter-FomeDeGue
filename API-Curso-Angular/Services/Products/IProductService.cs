using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Products;
using API_Curso_Angular.Models;

namespace API_Curso_Angular.Services.Products {
    public interface IProductService {
        Task<ResultDTO<Produto>> ProdutoPorId(long id);
        Task<ResultDTO<IEnumerable<Produto>>> ListarProdutos();
        Task<ResultDTO<Produto>> CriarProduto(CreateProductRequestDTO model);
        Task<ResultDTO<string>> DeletarProduto(long id);
        Task<ResultDTO<Produto>> AtualizarProduto(long id, UpdateProductRequestDTO model);
    }
}


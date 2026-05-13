using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Products;
using API_Curso_Angular.Models;
using API_Curso_Angular.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace API_Curso_Angular.Services.Products {
    public class ProductService : IProductService {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) {
            _repository = repository;
        }

        public async Task<ResultDTO<Produto>> AtualizarProduto(long id, UpdateProductRequestDTO model) {
            try {
                var produto = await _repository.ProdutoPorId(id);

                if (produto == null) {
                    return new ResultDTO<Produto>("Produto não encontrado para atualização.");
                }

                produto.Nome = model.Nome;
                produto.Categoria = model.Categoria;
                produto.Img = model.Img;
                produto.Valor = model.Valor;
                produto.Descricao = model.Descricao;
                produto.QuantidadeEmEstoque = model.QuantidadeEmEstoque;

                var prodAtt = await _repository.AtualizarProduto(produto);

                return new ResultDTO<Produto>(produto);
            }
            catch (Exception) {
                return new ResultDTO<Produto>("Ocorreu um erro interno ao tentar atualizar o produto.");
            }

        }

        public async Task<ResultDTO<Produto>> CriarProduto(CreateProductRequestDTO model) {
            var product = new Produto() {
                Nome = model.Nome,
                Categoria = model.Categoria,
                Img = model.Img,
                Valor = model.Valor,
                Descricao = model.Descricao,
                QuantidadeEmEstoque = model.QuantidadeEmEstoque,
            };

            try {
                var produto = await _repository.CriarProduto(product);
                return new ResultDTO<Produto>(product, new List<string>());
            }
            catch (Exception) {
                return new ResultDTO<Produto>("Não foi possível cadastrar o produto devido a um erro interno");
            }
        }

        public async Task<ResultDTO<string>> DeletarProduto(long id) {
            var produto = await _repository.ProdutoPorId(id);
            if (produto == null) {
                return new ResultDTO<string>("Produto não encontrado para deleção");
            }

            try {
                var prod = await _repository.DeletarProduto(produto);

                return new ResultDTO<string>("Produto deletado com sucesso.", new List<string>());
            }
            catch (Exception) {
                return new ResultDTO<string>("Não foi possível deletar o produto devido a um erro interno.");
            }

        }

        public async Task<ResultDTO<Produto>> ProdutoPorId(long id) {
            var produto = await _repository.ProdutoPorId(id);

            if (produto == null) {
                return new ResultDTO<Produto>("Produto não encontrado.");
            }

            return new ResultDTO<Produto>(produto);
        }

        public async Task<ResultDTO<IEnumerable<Produto>>> ListarProdutos() {
            try {
                var produtos = await _repository.ListarProdutos();

                return new ResultDTO<IEnumerable<Produto>>(produtos);
            }
            catch (Exception) {
                return new ResultDTO<IEnumerable<Produto>>("Ocorreu um erro interno ao tentar listar os produtos");
            }
        }
    }
}

using API_Curso_Angular.Data;
using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Products;
using API_Curso_Angular.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Curso_Angular.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDataContext _context;

        public ProductRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Produto> AtualizarProduto(Produto produto) {
            var attProd = _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> CriarProduto(Produto produto) {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> DeletarProduto(Produto produto) {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<IEnumerable<Produto>> ListarProdutos() {
            var list = await _context.Produtos.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<Produto?> ProdutoPorId(long id) {
            var prod = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            return prod;
        }
    }
}

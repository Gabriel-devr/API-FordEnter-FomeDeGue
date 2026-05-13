using API_Curso_Angular.Data;
using API_Curso_Angular.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Curso_Angular.Repositories.Carts {
    public class CartRepository : ICartRepository {
        private readonly AppDataContext _context;

        public CartRepository(AppDataContext context) {
            _context = context;
        }

        public async Task CreateCart(Carrinho carrinho) {
            await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task<Carrinho?> GetCartByClientId(long clientId) {
            //var cliente = await _context.Carrinhos.AsNoTracking().FirstOrDefaultAsync(x=>x.ClienteId == clientId);
            //return cliente;

            return await _context.Carrinhos.Include(c => c.Items).ThenInclude(i => i.Produto).FirstOrDefaultAsync(x => x.ClienteId == clientId);
        }

        public async Task UpdateCart(Carrinho carrinho) {
            _context.Carrinhos.Update(carrinho);
            await _context.SaveChangesAsync();
        }
    }
}

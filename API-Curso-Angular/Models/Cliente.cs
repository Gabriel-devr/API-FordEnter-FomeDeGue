using API_Curso_Angular.Models.Auth;

namespace API_Curso_Angular.Models {
    public class Cliente {
        public long Id { get; set; }
        public long IdentityUserId { get; set; }
        public User User { get; set; } = null!;
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public Carrinho Carrinho { get; set; } = null!;
    }
}

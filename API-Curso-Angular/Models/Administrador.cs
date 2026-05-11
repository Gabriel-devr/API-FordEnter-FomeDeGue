using API_Curso_Angular.Models.Auth;

namespace API_Curso_Angular.Models {
    public class Administrador {
        public long Id { get; set; }
        public long IdentityUserId { get; set; }
        public User User { get; set; } = null!;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}

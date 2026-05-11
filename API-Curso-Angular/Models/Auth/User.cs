using Microsoft.AspNetCore.Identity;

namespace API_Curso_Angular.Models.Auth {
    public class User :IdentityUser<long> {

        //Lista de Roles do usuário (Admin, Customer, etc)
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual Cliente? PerfilCliente { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
    }
}

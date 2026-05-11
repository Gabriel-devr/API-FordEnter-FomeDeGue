using Microsoft.AspNetCore.Identity;

namespace API_Curso_Angular.Models.Auth {
    public class Role : IdentityRole<long>{
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

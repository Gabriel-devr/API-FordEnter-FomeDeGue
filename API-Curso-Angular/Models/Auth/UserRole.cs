using Microsoft.AspNetCore.Identity;

namespace API_Curso_Angular.Models.Auth {
    public class UserRole : IdentityUserRole<long>{
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}

using API_Curso_Angular.Models;
using API_Curso_Angular.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Curso_Angular.Data.Mappings {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.PerfilCliente)
                .WithOne(x => x.User)
                .HasForeignKey<Cliente>(x => x.IdentityUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

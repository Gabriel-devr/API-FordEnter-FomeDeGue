using API_Curso_Angular.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Curso_Angular.Data.Mappings {
    public class UserRoleMap : IEntityTypeConfiguration<UserRole> {
        public void Configure(EntityTypeBuilder<UserRole> builder) {

            builder.ToTable("UserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.UserRoles)
                .HasForeignKey(x=>x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

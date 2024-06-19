using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infra.Data.Mappings
{
  public class RoleMap : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.HasKey(r => r.Id);

      builder.Property(r => r.RoleName).HasMaxLength(50).IsRequired();
      builder.Property(r => r.CreatedAt).IsRequired();

      //mapeamento do relacionamento (1 para N)
      builder.HasMany(r => r.Users) //1 Role TEM N User
          .WithOne(r => r.Role) // 1 User TEM 1 Role
          .HasForeignKey(r => r.RoleId); //definindo o campo CHAVE ESTRANGEIRA

      //mapeamento do relacionamento (1 para N)
      builder.HasMany(r => r.Permissions) // 1 Role tem N Permissions 
          .WithOne(r => r.Role) // 1 Permission tem 1 Role
          .HasForeignKey(r => r.RoleId); // definindo o campo CHAVE ESTRANGEIRA

    }
  }
}

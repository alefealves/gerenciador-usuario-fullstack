using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infra.Data.Mappings
{
  public class PermissionMap : IEntityTypeConfiguration<Permission>
  {
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
      builder.HasKey(p => p.Id);

      builder.Property(p => p.RoleId).IsRequired();
      builder.Property(p => p.SubModuleId).IsRequired();
      builder.Property(p => p.Create).HasColumnType("bit");
      builder.Property(p => p.Update).HasColumnType("bit");
      builder.Property(p => p.Delete).HasColumnType("bit");
      builder.Property(p => p.Read).HasColumnType("bit");

      //mapeamento do relacionamento (N para N)
      builder.HasOne(p => p.SubModule) // N SubModules tem 1 Permission
          .WithMany(p => p.Permissions)
          .HasForeignKey(p => p.SubModuleId); // N Permissions tem N SubModules


      builder.HasOne(p => p.Role) // N Roles tem N Permissions
          .WithMany(p => p.Permissions)
          .HasForeignKey(p => p.RoleId); // N Permissions tem N Roles

    }
  }
}

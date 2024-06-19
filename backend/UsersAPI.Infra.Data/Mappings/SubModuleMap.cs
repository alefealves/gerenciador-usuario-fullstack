using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infra.Data.Mappings
{
  public class SubModuleMap : IEntityTypeConfiguration<SubModule>
  {
    public void Configure(EntityTypeBuilder<SubModule> builder)
    {
      builder.HasKey(s => s.Id);

      builder.Property(s => s.ModuleId).IsRequired();
      builder.Property(s => s.ParentSubModuleId).IsRequired();
      builder.Property(s => s.SubModuleName).HasMaxLength(50).IsRequired();
      builder.Property(s => s.CreatedAt).IsRequired();

      //mapeamento do relacionamento (1 para N)
      builder.HasOne(s => s.Module) //1 Module TEM N SubModules
          .WithMany(s => s.SubModules) //N SubModules TEM 1 Module
          .HasForeignKey(s => s.ModuleId); //definindo o campo CHAVE ESTRANGEIRA

      //mapeamento do relacionamento (N para N)
      builder.HasMany(s => s.Permissions) // N Permissions tem N SubModules
          .WithOne(s => s.SubModule)
          .HasForeignKey(s => s.SubModuleId); // N SubModules tem N Permissions

    }
  }
}

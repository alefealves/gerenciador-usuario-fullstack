using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infra.Data.Mappings
{
  public class ModuleMap : IEntityTypeConfiguration<Module>
  {
    public void Configure(EntityTypeBuilder<Module> builder)
    {
      builder.HasKey(m => m.Id);

      builder.Property(m => m.ModuleName).HasMaxLength(50).IsRequired();
      builder.Property(m => m.CreatedAt).IsRequired();

      //mapeamento do relacionamento (1 para N)
      builder.HasMany(m => m.SubModules) //1 Module TEM N SubModules
          .WithOne(m => m.Module) //N SubModules TEM 1 Module
          .HasForeignKey(m => m.ModuleId); //definindo o campo CHAVE ESTRANGEIRA

    }
  }
}

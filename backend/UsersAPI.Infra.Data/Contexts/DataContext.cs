using Microsoft.EntityFrameworkCore;
using UsersAPI.Infra.Data.Mappings;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infra.Data.Contexts
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ModuleMap());
      modelBuilder.ApplyConfiguration(new PermissionMap());
      modelBuilder.ApplyConfiguration(new RoleMap());
      modelBuilder.ApplyConfiguration(new SubModuleMap());
      modelBuilder.ApplyConfiguration(new UserMap());
      //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Module> Module { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<SubModule> SubModule { get; set; }
    public DbSet<User> User { get; set; }
  }
}

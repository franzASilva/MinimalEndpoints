using Microsoft.EntityFrameworkCore;
using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Infrastructure.Data;

public sealed class MinimalEndpointsDbContext(DbContextOptions<MinimalEndpointsDbContext> options) : DbContext(options)
{
    public DbSet<Dummy> Dummies => Set<Dummy>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "MinimalEndpointsDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(255);

            entity.HasQueryFilter(e => e.Active);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Guid)
                      .IsRequired()
                      .HasMaxLength(255);

            entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);

            entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(255);

            entity.HasQueryFilter(e => e.Active);

            entity.HasOne(d => d.Role)
                      .WithMany(p => p.Users)
                      .HasForeignKey(d => d.RoleId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}

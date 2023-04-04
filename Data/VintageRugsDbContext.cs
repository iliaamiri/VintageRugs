using Microsoft.EntityFrameworkCore;
using VintageRugsApi.Models;

namespace VintageRugsApi.Data;

public class VintageRugsDbContext : DbContext
{
    public VintageRugsDbContext(DbContextOptions<VintageRugsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // `CURRENT_TIMESTAMP` only works for MySQL. Change it to `now()` for PostgreSQL.
        modelBuilder.Entity<Rug>().Property("CreatedAt")
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Rugmaker>().Property("CreatedAt")
            .HasDefaultValueSql("now()");
    }

    public DbSet<Rug> Rugs => Set<Rug>();
    public DbSet<Rugmaker> Rugmakers => Set<Rugmaker>();
}
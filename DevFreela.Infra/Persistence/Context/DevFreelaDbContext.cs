using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Context;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DevFreelaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using ChurchFlow.Domain.Entities;

namespace ChurchFlow.Infrastructure.Persistence;

public class ChurchFlowDbContext : DbContext
{
    public ChurchFlowDbContext(
        DbContextOptions<ChurchFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ChurchFlowDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
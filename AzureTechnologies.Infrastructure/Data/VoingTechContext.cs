using AzureTechnologies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzureTechnologies.Infrastructure.Data;

public partial class AzureTechContext : DbContext
{
    public AzureTechContext()
    {
    }

    public AzureTechContext(DbContextOptions<AzureTechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
        }
    }
}
using Keycloak.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Keycloak.API.Data;

public class KeycloakDbContext : DbContext
{
    public KeycloakDbContext()
    {

    }

    public KeycloakDbContext(DbContextOptions<KeycloakDbContext> options) : base(options)
    {

    }

    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}


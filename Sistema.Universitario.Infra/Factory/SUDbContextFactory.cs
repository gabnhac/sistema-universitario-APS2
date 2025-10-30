using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sistema.Universitario.Infra.Context;

namespace Sistema.Universitario.Infra.Factory;

public class AulaDbContextFactory
: IDesignTimeDbContextFactory<SUDbContext>
{
    public SUDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SUDbContext>();
        optionsBuilder.UseSqlServer(
        "Server=localhost;Database=SistemaUniversitario;Trusted_Connection=True; TrustServerCertificate = True");
    return new SUDbContext(optionsBuilder.Options);
    }
}
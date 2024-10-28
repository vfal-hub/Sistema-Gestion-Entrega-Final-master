using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestionData;
using SistemaGestionData.Context;
using SistemaGestionData.DataAccess;

namespace SistemaGestionData;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer(
        this IServiceCollection services,
        IConfiguration configuration
        )

    {
        services.AddDbContext<CoderhouseContext>(
        optionBuilder =>
        {
            var connectionString = configuration.GetConnectionString("Coderhouse");
            optionBuilder.UseSqlServer(connectionString);
        }
        );

        services.AddScoped<ProductosDataAccess>();
        services.AddScoped<UsuariosDataAccess>();
        services.AddScoped<ProductoVendidoDataAccess>();
        services.AddScoped<VentasDataAccess>();
        return services;
    }

}



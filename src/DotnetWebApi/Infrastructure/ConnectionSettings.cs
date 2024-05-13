using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConnectionSettings
{
    public static void AddDbConnection(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(
            o => o.UseSqlServer(
                connectionString,
                sqlServerOptions => sqlServerOptions.CommandTimeout(180)
            )
        );
    }
}

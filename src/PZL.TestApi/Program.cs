using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PZL.TestApi.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Brak połączenia DefaultConnection w konfiguracji.");
}

SqliteConnectionStringBuilder connectionStringBuilder =
    new SqliteConnectionStringBuilder(connectionString);

connectionStringBuilder.DataSource = Path.GetFullPath(
    connectionStringBuilder.DataSource,
    builder.Environment.ContentRootPath);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionStringBuilder.ConnectionString);
});

WebApplication app = builder.Build();

app.MapGet("/", () =>
{
    return "API działa.";
});

app.Run();

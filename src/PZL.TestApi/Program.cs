using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PZL.TestApi.Data;
using PZL.TestApi.Demo;

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

string? demoMode = builder.Configuration["demo"];

if (demoMode != null)
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (demoMode == "crud")
        {
            await CrudDemo.RunAsync(context);
        }
        else if (demoMode == "read")
        {
            int projectId;

            if (!int.TryParse(builder.Configuration["project-id"], out projectId)
                || projectId <= 0)
            {
                Console.WriteLine("Podaj dodatni identyfikator projektu w parametrze --project-id.");
                Environment.ExitCode = 1;
                return;
            }

            await CrudDemo.ReadAsync(context, projectId);
        }
        else
        {
            Console.WriteLine("Nieznany tryb demonstracji. Użyj --demo=crud lub --demo=read.");
            Environment.ExitCode = 1;
        }
    }

    return;
}

app.MapGet("/", () =>
{
    return "API działa.";
});

app.Run();

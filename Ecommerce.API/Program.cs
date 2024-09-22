using Ecommerce.API.DataEF.Context;
using Ecommerce.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.InitServices(configuration);

var app = builder.Build();

// NOTE: Check if the DB was fully migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        throw new Exception("Database is not fully migrated for EcommerceContext.");
    }
}

app.InitAppConfig();
app.Run();

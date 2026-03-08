using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using BadgeCatalog.Application.Queries.GetBadgeBySlug;
using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Adapters.Issuer;
using BadgeCatalog.Adapters.Security;
using BadgeCatalog.Application.Commands.DeactivateBadgeClass;
using BadgeCatalog.Application.Commands.UpdateBadgeClass;
using Microsoft.EntityFrameworkCore;
using BadgeCatalog.Adapters.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Procure todos os Handlers do MediatR dentro do assembly da camada Application
//e registre automaticamente no DI.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateBadgeClassCommand).Assembly));

builder.Services.AddDbContext<BadgeCatalogDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        );
});
// builder.Services.AddSingleton<IBadgeClassRepository, InMemoryBadgeClassRepository>();
builder.Services.AddScoped<IBadgeClassRepository, BadgeClassRepository>();
builder.Services.AddSingleton<IIssuerProvider, ConfigIssuerProvider>();
builder.Services.AddSingleton<IJwkProvider, StaticJwkProvider>();
builder.Services.AddScoped<CreateBadgeClassHandler>();
builder.Services.AddScoped<DeactivateBadgeClassHandler>();
builder.Services.AddScoped<UpdateBadgeClassHandler>();
builder.Services.AddScoped<GetAllBadgesHandler>();
builder.Services.AddScoped<GetBadgeBySlugHandler>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();
app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

// app.MapPatch("/badges/{id:guid}/deactivate", async (
//     Guid id,
//     DeactivateBadgeClassHandler handler,
//     CancellationToken cancellationToken) =>
// {
//     var result = await handler.Handle(id, cancellationToken);

//     return result ? Results.NoContent() : Results.NotFound();
// });


app.MapGet("/issuer", (IIssuerProvider issuerProvider) =>
{
    var issuer = issuerProvider.GetIssuer();
    return Results.Ok(issuer);
});
app.MapGet("/keys/current", (IJwkProvider provider) =>
{
    var key = provider.GetCurrent();
    return Results.Ok(key);
});
app.MapPut("/badges/{id}", async (
    Guid id,
    UpdateBadgeClassCommand command,
    UpdateBadgeClassHandler handler,
    CancellationToken cancellationToken) =>
{
    try
    {
        var updated = await handler.Handle(id, command, cancellationToken);

        if (!updated)
        {
            return Results.NotFound();
        }
        return Results.NoContent();
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict("This badge was modified by another user.");
    }
});

app.Run();

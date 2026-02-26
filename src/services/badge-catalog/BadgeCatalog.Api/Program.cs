using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using BadgeCatalog.Application.Queries.GetBadgeBySlug;
using BadgeCatalog.Ports.Repositories;
using BadgeCatalog.Adapters.Issuer;
using BadgeCatalog.Adapters.Security;
using BadgeCatalog.Application.Commands.DeactivateBadgeClass;
using BadgeCatalog.Application.Commands.UpdateBadgeClass;
using BadgeCatalog.Api.Requests;
using Microsoft.EntityFrameworkCore;
using BadgeCatalog.Adapters.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BadgeCatalogDbContext>(options =>
{
    options.UseSqlite("BadgeCatalogDb");
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

app.MapPost("/badges", async (
    CreateBadgeClassCommand command,
    CreateBadgeClassHandler handler,
    CancellationToken cancellationToken) =>
{
    var badge = await handler.Handle(command, cancellationToken);
    return Results.Created($"/badges/{badge.Id}", badge);
});
app.MapPatch("/badges/{id:guid}/deactivate", async (
    Guid id,
    DeactivateBadgeClassHandler handler,
    CancellationToken cancellationToken) =>
{
    var result = await handler.Handle(id, cancellationToken);

    return result ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/badges", async (
    bool? active,
    GetAllBadgesHandler handler,
    CancellationToken cancellationToken) =>
{
    var badges = await handler.Handle(active, cancellationToken);
    return Results.Ok(badges);
});

app.MapGet("/badges/{slug}", async (
    string slug,
    GetBadgeBySlugHandler handler,
    CancellationToken cancellationToken) =>
{
    var badge = await handler.Handle(slug, cancellationToken);

    return badge is null
        ? Results.NotFound()
        : Results.Ok(badge);
});
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
app.MapPut("/badges/{id:guid}", async (
    Guid id,
    UpdateBadgeClassHandler handler,
    UpdateBadgeRequest request,
    CancellationToken cancellationToken) =>
{
    var result = await handler.Handle(id, request.Name, request.Description, cancellationToken);

    return result ? Results.NoContent() : Results.NotFound();
});

app.Run();

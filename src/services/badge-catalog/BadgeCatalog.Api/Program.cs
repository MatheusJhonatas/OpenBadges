using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using BadgeCatalog.Application.Queries.GetBadgeBySlug;
using BadgeCatalog.Ports;
using BadgeCatalog.Ports.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBadgeClassRepository, InMemoryBadgeClassRepository>();
builder.Services.AddScoped<CreateBadgeClassHandler>();
builder.Services.AddScoped<GetAllBadgesHandler>();
builder.Services.AddScoped<GetBadgeBySlugHandler>();

var app = builder.Build();

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
    var id = await handler.Handle(command, cancellationToken);
    return Results.Created($"/badges/{id}", new { Id = id });
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

app.Run();

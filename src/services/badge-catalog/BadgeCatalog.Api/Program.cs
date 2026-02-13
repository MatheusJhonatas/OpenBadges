using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Ports.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBadgeClassRepository, InMemoryBadgeClassRepository>();
builder.Services.AddScoped<CreateBadgeClassHandler>();

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

app.Run();

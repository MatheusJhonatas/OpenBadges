using Issuance.Application.Commands.IssueBadge;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IssueBadgeCommand).Assembly));

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/scalar", () => Results.Redirect("/scalar/v1"));
app.MapGet("/scalar/", () => Results.Redirect("/scalar/v1"));
app.MapScalarApiReference(options =>
{
    options.AddDocument("v1");
    options.WithTitle("Issuance API");
});

app.MapControllers();

app.Run();

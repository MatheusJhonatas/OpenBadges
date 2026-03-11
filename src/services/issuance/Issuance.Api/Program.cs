using Issuance.Application.Commands.IssueBadge;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// aqui definimos o documento v1
builder.Services.AddOpenApi("v1");

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IssueBadgeCommand).Assembly));

var app = builder.Build();

// expõe o schema
app.MapOpenApi();

// interface Scalar
app.MapScalarApiReference(options =>
{
    options.AddDocument("v1");
    options.WithTitle("Issuance API");
});

app.MapControllers();

app.Run();
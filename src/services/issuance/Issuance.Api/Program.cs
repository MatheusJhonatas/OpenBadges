using Issuance.Adapters.Clients;
using Issuance.Adapters.Persistence;
using Issuance.Adapters.Repositories;
using Issuance.Api.Middlewares;
using Issuance.Application.Commands.IssueBadge;
using Issuance.Ports.Clients;
using Issuance.Ports.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// aqui definimos o documento v1
builder.Services.AddOpenApi("v1");

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IssueBadgeCommand).Assembly));

builder.Services.AddDbContext<IssuanceDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAssertionRepository, AssertionRepository>();

builder.Services.AddHttpClient<IBadgeCatalogClient, BadgeCatalogHttpClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5184");
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
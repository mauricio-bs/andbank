using ApiRest.Data;
using ApiRest.Domain.Service;
using ApiRest.Repository;
using ApiRest.Repository.Implementation;
using ApiRest.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Controller
builder.Services.AddControllers();

// Services
builder.Services.AddScoped<IPositionService, PositionService>();

// Repositories
builder.Services.AddScoped<IPositionRepository, PositionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

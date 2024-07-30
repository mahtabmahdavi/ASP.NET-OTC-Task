using GroceryStore.Data;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Repositories.Interfaces;
using GroceryStore.Repositories;
using GroceryStore.Services.Interfaces;
using GroceryStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IEntityRepository, EntityRepository>();
builder.Services.AddScoped<IGroceryService, GroceryService>();
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database (Entity Framework)
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

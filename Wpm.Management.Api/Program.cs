using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ManagementDbContext>(options =>
{
    options.UseSqlite("Data Source=Wpm.db");
});
builder.Services.AddScoped<IBreedService, BreedService>();
var app = builder.Build();
app.EnsureDbIsCreated();

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

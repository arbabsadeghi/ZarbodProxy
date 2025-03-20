using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZarbodProxy;
using ZarbodProxy.Controllers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<AssetController>();
builder.Services.AddDbContext<ZarbodDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")).LogTo(Console.WriteLine, LogLevel.Information));

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);
var app = builder.Build();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ZarbodDbContext>();
    dbContext.Database.Migrate(); // This applies pending migrations
}

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

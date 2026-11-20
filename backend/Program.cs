using backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing DefaultConnection string.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("HollyTravelsCors", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://hollytravels.co.uk",
                "https://www.hollytravels.co.uk"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("HollyTravelsCors");
app.UseAuthorization();
app.MapControllers();

app.Run();
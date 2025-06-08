using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventService.API.Services;
using Microsoft.EntityFrameworkCore;
using EventService.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register the EventService
builder.Services.AddScoped<IEventService, EventService.API.Services.EventService>();

// Add DbContext
builder.Services.AddDbContext<EventServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(
            "http://localhost:5173", // Local development
            "https://ashy-bay-0b0f05003.6.azurestaticapps.net", // New Azure Static Web App
            "https://eventshram-fyc2gmgaevcza9g6.swedencentral-01.azurewebsites.net" // EventService
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();


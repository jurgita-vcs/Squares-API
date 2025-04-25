using Microsoft.EntityFrameworkCore;
using SquaresAPI.Data;
using SquaresAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register data base
builder.Services.AddDbContext<PointsContext>(options =>
    options.UseSqlite("Data Source=points.db"));

//Register the Service
builder.Services.AddScoped<SquareDetectorService>();

//Enable modify your Swagger setup
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Build app
var app = builder.Build();

//Create the SQLite database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PointsContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment() ||
    Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

// Start the app
app.Run();

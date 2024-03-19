using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using pcstore_api.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<StoredbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowApp",
            builder =>
            {
                builder.WithOrigins("http://localhost:5173") // Adjust this to match your Angular app's origin
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store APIs", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
                        options.RoutePrefix = string.Empty;
                    });
}
app.UseCors("AllowApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

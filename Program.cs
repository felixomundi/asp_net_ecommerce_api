using asp_net_ecommerce_api.DataBase;
using asp_net_ecommerce_api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Description = "Buy the products you love", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  
   if (connectionString != null)
    {
        options.UseMySQL(connectionString);
    }
    else
    {
        Console.WriteLine("Error: Connection string is null.");
    } 


});
// Add or register repositories
builder.Services.AddScoped<ProductRepository>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1");
   });
}
app.MapControllers();
app.Run();

using Microsoft.OpenApi.Models;
using asp_net_ecommerce_api.Models.Product;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Description = "Buy the products you love", Version = "v1" });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1");
   });
}
// app.MapGet("/", () => "Hello World!");
app.MapGet("/products/{id}", (int id) => ProductDB.GetProduct(id));
app.MapGet("/products", () => ProductDB.GetProducts());
app.MapPost("/products", (Product product) => ProductDB.CreateProduct(product));
app.MapPut("/products", (Product product) => ProductDB.UpdateProduct(product));
app.MapDelete("/products/{id}", (int id) => ProductDB.RemoveProduct(id));

app.Run();

using asp_net_ecommerce_api.Configurations;
using asp_net_ecommerce_api.Models;
namespace asp_net_ecommerce_api.Repository;
public class ProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddProduct(Products product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    public List<Products> GetProducts(){
        return _context.Products.ToList();
    }
    public Products GetProductById(int id){
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }
    public void UpdateProduct(Products product){
        _context.Products.Update(product);
        _context.SaveChanges();
        //return;
    }
    public bool ProductExists(int id)
    {
        return _context.Products.Any(p => p.Id == id);
    }
}
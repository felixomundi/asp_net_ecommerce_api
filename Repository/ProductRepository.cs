using asp_net_ecommerce_api.DataBase;
using asp_net_ecommerce_api.Models;
namespace asp_net_ecommerce_api.Repository;
public class ProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddShirt(Shirts shirt)
    {
        _context.Shirts.Add(shirt);
        _context.SaveChanges();
    }
    public List<Shirts> GetShirts(){
        return _context.Shirts.ToList();
    }
}
namespace asp_net_ecommerce_api.Models.Product;
public class Product {    
   public int Id {get; set;} 
   public string ? Name { get; set; }
 }


 public class ProductDB{
    private static List<Product> _products = new List<Product>()
    {
        new Product{
            Id=1,Name ="Product One"
        },
        new Product{
            Id=2, Name ="Product Two"
        }
    };
    public static List<Product> GetProducts() {
        return _products;
    }
    public static Product ? GetProduct(int Id){
        return _products.SingleOrDefault(product => product.Id == Id);
    }
    public static Product CreateProduct(Product product){
        _products.Add(product);
        return product;
    }
    public static Product UpdateProduct(Product update){
        _products = _products.Select(product=>{
           if(product.Id == update.Id){
                product.Name = update.Name;
           }
            return product;
        }).ToList();
        return update;
    }
     public static void RemoveProduct(int id)
   {
     _products = _products.FindAll(product => product.Id != id).ToList();
  
   }
 }

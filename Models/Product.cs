namespace asp_net_ecommerce_api.Models;
public class Products {    
   public int Id {get; set;} 
   public string ? Name { get; set; }
 }


 public class ProductDB{
    private static List<Products> _products = new List<Products>()
    {
        new Products{
            Id=1,Name ="Product One"
        },
        new Products{
            Id=2, Name ="Product Two"
        }
    };
    public static List<Products> GetProducts() {
        return _products;
    }
    public static Products ? GetProduct(int Id){
        return _products.SingleOrDefault(product => product.Id == Id);
    }
    public static Products CreateProduct(Products product){
        _products.Add(product);
        return product;
    }
    public static Products UpdateProduct(Products update){
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

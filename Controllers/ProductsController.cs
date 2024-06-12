using asp_net_ecommerce_api.Models;
using asp_net_ecommerce_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase{

    private readonly ProductRepository productRepository;
    public ProductsController(ProductRepository _productRepository) {
        productRepository = _productRepository;
    }
    [HttpGet]
        public IActionResult GetProducts(){
            var products = productRepository.GetProducts();        
            var count = products.Count();
            if( count>0){
                return Ok(products);
            }
            return NotFound("No products found");
        }
     [HttpGet("{id}")]
    public IActionResult GetProductById(int id){
        // Retrieve the product from your data source based on the provided ID
        var product = productRepository.GetProductById(id);
         if (product != null){
            return Ok(product);
            }
       return NotFound($"Product with ID {id} is not found");
     
       
    }
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Products product){
         productRepository.AddProduct(product);
        return Ok("Product added successfully");
    }    
    [HttpPut("{id}/{color}")]
    public string UpdateProducted(int id, string color){
        return $"Product ID:{id}, Product Color: {color}";
    }
    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, [FromBody] Products product)
    {

        // validate request body

        if (!ModelState.IsValid){
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

                return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
        if (!productRepository.ProductExists(id))
        {
           return NotFound(new { Message = $"Product with ID {id} not found." });
        }

        var existingProduct = productRepository.GetProductById(id);

        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        // Apply updates
        existingProduct.Name = product.Name;       
        // Update other properties as needed

        // Save changes
        productRepository.UpdateProduct(existingProduct);

        return Ok(existingProduct);
    }
   
}
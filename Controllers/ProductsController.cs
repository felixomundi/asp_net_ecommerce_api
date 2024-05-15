using asp_net_ecommerce_api.Models;
using asp_net_ecommerce_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase{

    private readonly ProductRepository productRepository;
    public ProductsController(ProductRepository _productRepository) {
        productRepository = _productRepository;
    }
    [HttpGet]
        public IActionResult GetShirts(){
            var shirts = productRepository.GetShirts();        
            var count = shirts.Count();
            if( count>0){
                return Ok(shirts);
            }
            return NotFound("No shirts found");
        }
     [HttpGet("{id}")]
    public IActionResult GetProduct(int id){
        var name = 1;
        if (name == id)
            return Ok(id);

        return NotFound($"This {id} is not found");
    }
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Shirts shirt){
         productRepository.AddShirt(shirt);
        return Ok("Product added successfully");
    }
    [HttpPut("{id}/{color}")]
    public string UpdateProducted(int id, string color){
        return $"Product ID:{id}, Product Color: {color}";
    }
   
}
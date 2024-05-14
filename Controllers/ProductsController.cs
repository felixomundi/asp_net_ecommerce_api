using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_api.Controllers.ProductsController;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase{
     [HttpGet("{id}")]
    public string GetProduct(int id){
        return $"{id}";
    }
}
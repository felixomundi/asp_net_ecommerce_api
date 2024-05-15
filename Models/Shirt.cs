using System.ComponentModel.DataAnnotations;

namespace asp_net_ecommerce_api.Models;
public class Shirts{
   [Key]
   public int Id {get; set;} 
   [Required]
   public string ? Name { get; set; }
   [Required]
   public double ? Price{ get; set; }
}
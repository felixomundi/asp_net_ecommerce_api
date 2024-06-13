using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_ecommerce_api.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string ? Username { get; set; }       
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]     
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid email address")]        
        public string  ? Email {
            get; set;
        }
        [Required]
        public string ? Password { get; set; }
    }
}
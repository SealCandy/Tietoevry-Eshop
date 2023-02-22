using CodeFirst2._0.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOS
{
    public class OrderLineDTO
    {
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        
        public int ProductVariantId { get; set; }



    }
}

using EShop.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.DTOS
{
    public class OrderLineDTO
    {
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        
        public int ProductVariantId { get; set; }



    }
}

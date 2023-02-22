using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOS
{
    public class ProductDTO
    {

        public string Code { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        public string? Image { get; set; }

    }
}

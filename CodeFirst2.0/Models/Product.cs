using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst2._0.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name{ get; set; }
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        public string? Image{ get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<ProductVariant> Variants { get; set; }  
    }
}

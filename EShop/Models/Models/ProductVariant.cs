using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string ParamName1 { get; set; }
        public string ParamValue1 { get; set; }
        public string ParamName2 { get; set; }
        public string ParamValue2 { get; set; }
        public string ParamName3 { get; set; }
        public string ParamValue3 { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }

    }
}

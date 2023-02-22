using EShop.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number{ get; set; }
        public State OrderState { get; set; }
        public DateTime DateTime{ get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

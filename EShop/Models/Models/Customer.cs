using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(30)]
        public string Street { get; set; }
        [MaxLength(60)]
        public string City { get; set; }

        public int Zip { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

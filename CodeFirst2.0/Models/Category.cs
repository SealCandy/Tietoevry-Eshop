using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst2._0.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public virtual List<Product> Products{ get; set; }
        public Category? ParentCategory { get; set; }
        public int? ParentId { get; set; }
        public virtual List<Category>? SubCategories { get; set; }

    }
}

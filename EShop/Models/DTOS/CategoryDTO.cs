using EShop.Models.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.DTOS
{
    public class CategoryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [MaxLength(300)]
        [JsonProperty("description")]
        public string Description { get; set; }
        public virtual List<CategoryDTO>? SubCategories { get; set; }

    }
}

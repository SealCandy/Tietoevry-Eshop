using EShop.Models.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.DTOS
{
    public class NewCategoryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty ("name")]
        public string Name { get; set; }
        [MaxLength(300)]
        [JsonProperty ("description")]
        public string Description { get; set; }
        [JsonProperty("parentCategoryId")]
        public int? ParentCategoryId { get; set; }
        [JsonProperty("parentName")]
        public string? ParentName { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}

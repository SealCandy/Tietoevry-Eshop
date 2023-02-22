using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOS
{
    public class ProductWithASingleVariant
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [MaxLength(40)]
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }

        public ProductVariantDTO ProductVariantDTO { get; set; }
    }
}

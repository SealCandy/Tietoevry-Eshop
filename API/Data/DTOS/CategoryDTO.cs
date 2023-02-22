using API.Data.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOS
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

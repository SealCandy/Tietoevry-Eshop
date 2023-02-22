using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.DTOS
{
    public class CustomerDTO
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [MaxLength(20)]
        [Required]
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [MaxLength(30)]
        [JsonProperty("street")]
        public string ? Street { get; set; }
        [MaxLength(60)]
        [JsonProperty("city")]
        public string ? City { get; set; }
        [JsonProperty("zip")]
        public int Zip { get; set; }
    }
}

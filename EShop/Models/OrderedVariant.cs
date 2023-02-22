using Newtonsoft.Json;

namespace EShop.Models
{
    public class OrderedVariant
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}

//Use only Variant ID
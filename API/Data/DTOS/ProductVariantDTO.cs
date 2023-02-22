using API.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Data.DTOS
{
    public class ProductVariantDTO
    {
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
        public string[] paramNames;
        public decimal Price { get; set; }
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public ProductVariantDTO()
        {
            paramNames = new string[2];
        }
        public ProductVariantDTO(ProductVariantDTO productVariantDTO)
        {
            Code = productVariantDTO.Code;
            ParamName1 = productVariantDTO.ParamName1;
            ParamName2 = productVariantDTO.ParamName2;
            ParamName3 = productVariantDTO.ParamName3;
            Price = productVariantDTO.Price;
            ParamValue1 = productVariantDTO.ParamValue1;
            ParamValue2 = productVariantDTO.ParamValue2;
            ParamValue3 = productVariantDTO.ParamValue3;
        }

        public void SetParamNames(string ParamName1, string ParamName2, string ParamName3)
        {
            paramNames[0] = ParamName1;
            paramNames[1] = ParamName2;
            paramNames[2] = ParamName3;
        }
    }
}

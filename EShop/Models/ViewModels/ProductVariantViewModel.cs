using EShop.Models.DTOS;

namespace EShop.Models.ViewModels
{
    public class ProductVariantViewModel
    {

        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string ParamName1 { get; set; }
        public string ParamValue1 { get; set; }
        public string ParamName2 { get; set; }
        public string ParamValue2 { get; set; }
        public string ParamName3 { get; set; }
        public string ParamValue3 { get; set; }
        public string VariantCode { get; set; }
        public void setAttributesForNewVariant(ProductWithASingleVariant ProductWithAVariant)
        {
            ProductId = ProductWithAVariant.Id;
            Name = ProductWithAVariant.Name;
            Image = ProductWithAVariant.Image;
            Description = ProductWithAVariant.Description;
            Code = ProductWithAVariant.Code;
            VariantId = ProductWithAVariant.ProductVariantDTO.Id;
            Price = ProductWithAVariant.ProductVariantDTO.Price;
            ParamName1 = ProductWithAVariant.ProductVariantDTO.ParamName1;
            ParamName2 = ProductWithAVariant.ProductVariantDTO.ParamName2;
            ParamName3 = ProductWithAVariant.ProductVariantDTO.ParamName3;
            ParamValue1 = ProductWithAVariant.ProductVariantDTO.ParamValue1;
            ParamValue2 = ProductWithAVariant.ProductVariantDTO.ParamValue2;
            ParamValue3 = ProductWithAVariant.ProductVariantDTO.ParamValue3;
            VariantCode = ProductWithAVariant.ProductVariantDTO.Code;
            Quantity = 1;
        }

        public void setAttributes(ProductWithASingleVariant ProductWithAVariant)
        {
            ProductId = ProductWithAVariant.Id;
            Name = ProductWithAVariant.Name;
            Image = ProductWithAVariant.Image;
            Description = ProductWithAVariant.Description;
            Code = ProductWithAVariant.Code;
            VariantId = ProductWithAVariant.ProductVariantDTO.Id;
            Price = ProductWithAVariant.ProductVariantDTO.Price;
            ParamName1 = ProductWithAVariant.ProductVariantDTO.ParamName1;
            ParamName2 = ProductWithAVariant.ProductVariantDTO.ParamName2;
            ParamName3 = ProductWithAVariant.ProductVariantDTO.ParamName3;
            ParamValue1 = ProductWithAVariant.ProductVariantDTO.ParamValue1;
            ParamValue2 = ProductWithAVariant.ProductVariantDTO.ParamValue2;
            ParamValue3 = ProductWithAVariant.ProductVariantDTO.ParamValue3;
            VariantCode = ProductWithAVariant.ProductVariantDTO.Code;
        }
    }
}

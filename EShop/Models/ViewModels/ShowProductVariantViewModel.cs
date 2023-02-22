using EShop.Models.DTOS;

namespace EShop.Models.ViewModels
{
    public class ShowProductVariantViewModel
    {
        public List<ProductVariantDTO> mostBoughtProductVariants { get; set; }
        public List<ProductVariantDTO> discountedProductsVariants { get; set; }
    }
}

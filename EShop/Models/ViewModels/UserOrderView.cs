using EShop.Models.DTOS;

namespace EShop.Models.ViewModels
{
    public class UserOrderView
    {
        public List<ProductVariantViewModel> ProductVariantViewModels { get; set; }
        public CustomerDTO CustomerDTO { get; set; }

        public decimal getTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (ProductVariantViewModel item in ProductVariantViewModels)
            {
                totalPrice += (item.Price * item.Quantity);
            }
            return totalPrice;
        }

        public int getTotalQuantity()
        {
            int ret = 0;
            foreach (ProductVariantViewModel item in ProductVariantViewModels)
            {
                ret += item.Quantity;
            }
            return ret;
        }
    }
}

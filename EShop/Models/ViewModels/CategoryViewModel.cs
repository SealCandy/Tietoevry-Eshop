using EShop.Models.DTOS;
using EShop.Models.Models;

namespace EShop.Models.ViewModels
{
    public class CategoryViewModel
    {
        public NewCategoryDTO newCategoryDTO { get; set; }
        public List<Category> categories { get; set; }
    }
}

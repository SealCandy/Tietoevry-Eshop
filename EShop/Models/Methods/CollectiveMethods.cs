using EShop.Models.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;
namespace EShop.Models.Methods
{
    public static class CollectiveMethods
    {
        public static List<Category> flattenCategoryList(List<Category> categories)
        {
            List<Category> ret = new List<Category>();
            foreach (var item in categories)
            {
                if (item.ParentId == null)
                {
                    ret.Add(item);
                    if (item.SubCategories != null &&item.SubCategories.Count > 0)
                    {
                        ret = gatherAllSubCategories(ret, item.SubCategories,item);
                    }
                }
            }
            return ret;
        }
        private static List<Category> gatherAllSubCategories(List<Category> primaryList, List<Category> subCat, Category parent)
        {
            List<Category> ret = primaryList;
            foreach (var item in subCat)
            {
                item.ParentCategory = parent;

                ret.Add(item);
                if (item.SubCategories != null && item.SubCategories.Count > 0)
                {
                    ret = gatherAllSubCategories(ret, item.SubCategories,item);
                }
            }
            return ret;
        }
    }
}

// add to cart = data gets to database
// cookie for add to cart expire and memory
using API.Data.DTOS;
using API.Data.Models;

namespace API.Data
{
    public class Convertor
    {
        public static OrderLine ConvertToOrderLine(OrderLineDTO orderLineDTO, ProductVariant productVariant)
        {
            return new OrderLine
            {
                Price = orderLineDTO.Price,
                ProductVariant = productVariant,
                Quantity = orderLineDTO.Quantity,
            };
        }

        public static OrderLineDTO ConvertToOrderLineDTO(OrderLine orderLine)
        {
            return new OrderLineDTO
            {
                Price = orderLine.Price,
                ProductVariantId = orderLine.ProductVariant.Id,
                Quantity = orderLine.Quantity,
            };
        }

        public static ProductVariant ConvertToProductVariant(ProductVariantDTO productVariantDTO)
        {
            return new ProductVariant
            {
                ParamValue1 = productVariantDTO.ParamValue1,
                ParamValue2 = productVariantDTO.ParamValue2,
                ParamValue3 = productVariantDTO.ParamValue3,
                Price = productVariantDTO.Price,
                ParamName1 = productVariantDTO.ParamName1,
                ParamName2 = productVariantDTO.ParamName2,
                ParamName3 = productVariantDTO.ParamName3,
                Code = productVariantDTO.Code,
            };
        }

        public static ProductVariantDTO ConvertToProductVariantDTO(ProductVariant productVariant)
        {
            return new ProductVariantDTO
            {
                Id = productVariant.Id,
                ProductId = productVariant.Product.Id,
                ProductName = productVariant.Product.Name,
                ParamValue1 = productVariant.ParamValue1,
                ParamValue2 = productVariant.ParamValue2,
                ParamValue3 = productVariant.ParamValue3,
                Price = productVariant.Price,
                ParamName1 = productVariant.ParamName1,
                ParamName2 = productVariant.ParamName2,
                ParamName3 = productVariant.ParamName3,
                Code = productVariant.Code,
            };
        }

        public static List<OrderLine> ConvertOrderLineDTOToList(List<OrderLineDTO> OrderLinesDTO, ShopDbContext shopDbContext)
        {
            List<OrderLine> ret = new List<OrderLine>();
            foreach (var item in OrderLinesDTO)
            {
                ret.Add(ConvertToOrderLine(item, shopDbContext.ProductVariants.Where(o => o.Id.Equals(item.ProductVariantId)).FirstOrDefault()));
            }
            return ret;
        }

        public static List<OrderLineDTO> ConverOrderLineToDtoList(List<OrderLine> OrderLines, ShopDbContext shopDbContext)
        {
            List<OrderLineDTO> ret = new List<OrderLineDTO>();
            foreach (var item in OrderLines)
            { 
                ret.Add(ConvertToOrderLineDTO(item));
            }
            return ret;
        }

        public static List<Category> ConvertDTOtoCategoryList(List<CategoryDTO> categoriesDTO)
        {
            List<Category> categories = new List<Category>();
            foreach (var item in categoriesDTO)
            {
                if (item.SubCategories != null && item.SubCategories.Count > 0)
                {
                    categories.Add(new Category
                    {
                        Description = item.Description,
                        Name = item.Name,
                        SubCategories = ConvertDTOtoCategoryList(item.SubCategories)
                    });
                }
                else
                {
                    categories.Add(new Category
                    {
                        Description = item.Description,
                        Name = item.Name,
                    });
                }
            }
            return categories;
        }

        public static List<CategoryDTO> ConvertCategoryToDTOList(List<Category> categories)
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                if (item.SubCategories != null && item.SubCategories.Count > 0)
                {
                    categoryDTOs.Add(new CategoryDTO
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Name = item.Name,
                        SubCategories = ConvertCategoryToDTOList(item.SubCategories)
                    });
                }
                else
                {
                    categoryDTOs.Add(new CategoryDTO
                    {
                        Id=item.Id,
                        Description = item.Description,
                        Name = item.Name,
                    });
                }
            }
            return categoryDTOs;
        }

        public static List<ProductVariantDTO> ConvertProductVariantToDTOList(List<ProductVariant> variants)
        {
            List<ProductVariantDTO> variantDTOs = new List<ProductVariantDTO>();
            foreach (var item in variants)
            {
                variantDTOs.Add(new ProductVariantDTO
                {
                    Id = item.Id,
                    Code = item.Code,
                    ParamName1 = item.ParamName1,
                    ParamName2 = item.ParamName2,
                    ParamName3 = item.ParamName3,
                    ParamValue1 = item.ParamValue1,
                    ParamValue2 = item.ParamValue2,
                    ParamValue3 = item.ParamValue3,
                    Price = item.Price,
                });
            }
            return variantDTOs;
        }
        public static ProductVariantDTO ConvertProductVariantListToDTOById(List<ProductVariant> variants,int id, int productId)
        {
            ProductVariant temp = variants.FirstOrDefault(x => x.Id == id);

            return new ProductVariantDTO
            {
                Code = temp.Code,
                ParamName1 = temp.ParamName1,
                ParamValue1 = temp.ParamValue1,
                ParamName2 = temp.ParamName2,
                ParamValue2 = temp.ParamValue2,
                ParamName3 = temp.ParamName3,
                ParamValue3 = temp.ParamValue3,
                Price = temp.Price,
                Id = id,
                ProductId = productId
            };
        }
    }
}

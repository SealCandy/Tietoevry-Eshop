using API.Data;
using API.Data.DTOS;
using API.Data.Enum;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{//System seralization
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ShopDbContext shopDbContext;
        public ProductsController(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        /// <summary>
        /// Gets products by specifications from the customer
        /// </summary>
        /// <param name="Type">Used for recognising which sorting method should be used</param>
        /// <param name="categoryId">Id of category the user wants the products from</param>
        /// <param name="Page">Which page do we want to be on from selection</param>
        /// <param name="PageSize">Number of items that each page will contain</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getProduct(OrderTypes Type, int categoryId, int Page, int PageSize)
        {
            if (Page <= 0)
            {
                Page = 1;
            }
            if (categoryId > 0 && Type != OrderTypes.MOSTBOUGHT)
            {
                var result = shopDbContext.Categories
                    .Where(o => o.Id == categoryId)
                    .Include(o => o.Products)
                    .ThenInclude(o => o.Variants)
                    .SelectMany(o => o.Products)
                    .SelectMany(o => o.Variants)
                    .Select(o => new ProductVariantDTO
                    {
                        Code = o.Code,
                        ParamName1 = o.ParamName1,
                        ParamName2 = o.ParamName2,
                        ParamName3 = o.ParamName3,
                        ParamValue1 = o.ParamValue1,
                        ParamValue2 = o.ParamValue2,
                        ParamValue3 = o.ParamValue3,
                        Price = o.Price,
                        ProductId = o.Product.Id,
                        ProductName = o.Product.Name,
                    })
                    .AsQueryable();
                switch (Type)
                {
                    case OrderTypes.ALPHABETICAL:
                        return Ok(await result
                            .OrderBy(o => o.Code)
                            .Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync());

                    case OrderTypes.PRICE:
                        return Ok(await result
                            .OrderBy(o => o.Price)
                            .Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync());

                    case OrderTypes.DISCOUNTED:
                        return Ok(await result
                            .OrderBy(o => o.ParamName1)
                            .Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync());
                    default:
                        return NotFound("Data was not found");
                }
            }
            else
            {
                var result = shopDbContext.Categories
                    .Include(o => o.Products)
                    .ThenInclude(o => o.Variants)
                    .SelectMany(o => o.Products)
                    .SelectMany(o => o.Variants)
                    .Select(o => new ProductVariantDTO
                    {
                        Code = o.Code,
                        ParamName1 = o.ParamName1,
                        ParamName2 = o.ParamName2,
                        ParamName3 = o.ParamName3,
                        ParamValue1 = o.ParamValue1,
                        ParamValue2 = o.ParamValue2,
                        ParamValue3 = o.ParamValue3,
                        Price = o.Price,
                        ProductId = o.Product.Id,
                        ProductName = o.Product.Name,
                    })
                    .AsQueryable();
                switch (Type)
                {
                    case OrderTypes.MOSTBOUGHT:
                        var mostBought = await shopDbContext.OrderLines
                            .Where(o => o.Order.OrderState == CodeFirst2._0.Enum.State.DELIVERED)
                            .GroupBy(x => x.ProductVariant.Id)
                            .Select(x => new { ProductVariantId = x.Key, Quantity = x.Sum(y => y.Quantity) })
                            .OrderByDescending(x => x.Quantity)
                            .ToListAsync();
                        var TrueResult = await shopDbContext.ProductVariants
                            .Where(o => mostBought.Select(j => j.ProductVariantId).Contains(o.Id))
                                    .Select(o => new ProductVariantDTO
                                    {
                                        Id = o.Id,
                                        ParamName1 = o.ParamName1,
                                        ParamName2 = o.ParamName2,
                                        ParamName3 = o.ParamName3,
                                        ParamValue1 = o.ParamValue1,
                                        ParamValue2 = o.ParamValue2,
                                        ParamValue3 = o.ParamValue3,
                                        Code = o.Code,
                                        Price = o.Price,
                                        ProductId = o.Product.Id,
                                        ProductName = o.Product.Name,
                                    })
                                    .Skip((Page - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();
                        return Ok(TrueResult);
                    case OrderTypes.ALPHABETICAL:
                        return Ok(await result
                            .OrderBy(o => o.Code)
                            .Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync());
                    case OrderTypes.PRICE:
                        return Ok(await result
                            .OrderBy(o => o.Price)
                            .Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToListAsync());
                    default:
                        return NotFound("Data was not found");
                }
            }
        }
        /// <summary>
        /// Gets the product with all it's variants
        /// </summary>
        /// <param name="ProductId">Id which is used in the search</param>
        /// <returns></returns>
        [HttpGet("{ProductId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getProductVariants(int ProductId)
        {
            if (shopDbContext.Products.Where(o => o.Id == ProductId).Count() == 0)
            {
                return NotFound();
            }
            return Ok(await shopDbContext.Products
                .Where(o => o.Id == ProductId)
                .Include(o => o.Variants)
                .Select(o => new ProductWithVariants
                {
                    Id = o.Id,
                    Code = o.Code,
                    Description = o.Description,
                    Image = o.Image,
                    Name = o.Name,
                    ProductVariantDTOs = Convertor.ConvertProductVariantToDTOList(o.Variants)
                })
                .FirstOrDefaultAsync());
        }
        /// <summary>
        /// Gets the product with all it's variants
        /// </summary>
        /// <param name="ProductId">Id which is used in the search</param>
        /// <param name="VariantId">Id which is used in the search</param>
        /// <returns></returns>
        [HttpGet("{ProductId:int}/{VariantId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getProductVariant(int ProductId, int VariantId)
        {
            if (shopDbContext.Products.Where(o => o.Id == ProductId).Count() == 0)
            {
                return NotFound();
            }
            return Ok(await shopDbContext.Products
                .Where(o => o.Id == ProductId)
                .Include(o => o.Variants)
                .Select(o => new ProductWithASingleVariant
                {
                    Id = o.Id,
                    Code = o.Code,
                    Description = o.Description,
                    Image = o.Image,
                    Name = o.Name,
                    ProductVariantDTO = Convertor.ConvertProductVariantListToDTOById(o.Variants,VariantId, ProductId),
                })
                .FirstOrDefaultAsync());
        }
        /// <summary>
        /// Gets the product with all it's variants
        /// </summary>
        /// <param name="VariantId">Id which is used in the search</param>
        /// <returns></returns>
        [HttpGet("VariantId{VariantId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getVariant(int VariantId)
        {
            if (shopDbContext.ProductVariants.Where(o => o.Id == VariantId).Count() == 0)
            {
                return NotFound();
            }
            return Ok(await shopDbContext.ProductVariants
                .Where(o => o.Id == VariantId)
                .Include(o => o.Product)
                .Select(o => new ProductWithASingleVariant
                {
                    Id = o.Product.Id,
                    Code = o.Product.Code,
                    Description = o.Product.Description,
                    Image = o.Product.Image,
                    Name = o.Product.Name,
                    ProductVariantDTO = Convertor.ConvertToProductVariantDTO(o),
                })
                .FirstOrDefaultAsync());
        }
    }
}

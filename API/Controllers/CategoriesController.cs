using Microsoft.AspNetCore.Mvc;
using MethodManagement.Controller;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Data.DTOS;

namespace API.Controllers
{//Fix the self referencing
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext shopDbContext;
        public CategoriesController(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        //Mapper check
        //Swagger doc create 
        /// <summary>
        /// Used for creating a new Category
        /// </summary>
        /// <param name="category">Which category we are creating</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> createCategory([FromBody] NewCategoryDTO category)
        {
            if (category != null)
            {
                var parentCategory = new Category();
                if (shopDbContext.Categories.Select(o => o.Id).Contains(category.ParentCategoryId ?? 0))
                {
                    parentCategory = shopDbContext.Categories
                        .Where(o => o.Id.Equals(category.ParentCategoryId))
                        .FirstOrDefault();
                    shopDbContext.Categories.Add(new Category { Name = category.Name, Description = category.Description, ParentCategory = parentCategory });
                }
                else
                {
                    shopDbContext.Categories.Add(new Category { Name = category.Name, Description = category.Description });
                }
                await shopDbContext.SaveChangesAsync();
                return Ok("Category was sucessfully created");
            }
            else
            {
                return BadRequest("Wrong category format");
            }
        }
        /// <summary>
        /// Gets all existing categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCategories()
        {
            if (shopDbContext.Categories.ToList().Count == 0)
            {
                return NotFound("Category was not found");
            }
            return Ok(await shopDbContext.Categories
                .Where(o => o.ParentId == null)
                .Include(o => o.SubCategories)
                .Select(o => new CategoryDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    SubCategories = Convertor.ConvertCategoryToDTOList(o.SubCategories),
                })
                .OrderBy(o => o.Name)
                .ToListAsync());
        }
        /// <summary>
        /// Gets a specific category that is related to the id
        /// </summary>
        /// <param name="CategoryId">Input for searched category</param>
        /// <returns></returns>
        [HttpGet("{CategoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCategory(int CategoryId)
        {
            if (shopDbContext.Categories.Where(o => o.Id == CategoryId) == null)
            {
                return NotFound();
            }
            var result = await shopDbContext.Categories
                .Where(o => o.Id == CategoryId)
                .Include(o => o.Products)
                .ThenInclude(o => o.Variants)
                .Select(x => new NewCategoryDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    ParentCategoryId = x.ParentId,
                    ParentName = x.ParentCategory.Name,
                    Products = x.Products
                })
                .FirstAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Category was not found");
        }
        /// <summary>
        /// Used for updating a category based on it's id
        /// </summary>
        /// <param name="id">Id of the category we want to update</param>
        /// <param name="category">The edited data we want the categiry to have</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> updateCategoryById(int id, [FromBody] NewCategoryDTO category)
        {
            var Category = await shopDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (Category != null)
            {
                Category.Name = category.Name;
                Category.Description = category.Description;
                if (category.ParentCategoryId > 0)
                {
                    Category.ParentCategory = shopDbContext.Categories
                        .Where(o => o.Id == category.ParentCategoryId)
                        .FirstOrDefault();
                }
                else
                {
                    Category.ParentId = null;
                    Category.ParentCategory = null;
                }
                await shopDbContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("Category was not found");
            }
            return Ok(Category);
        }
        /// <summary>
        /// Used for removing a category based on it's id
        /// </summary>
        /// <param name="id">Id of the category we want to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategoryById(int id)
        {
            var Category = await shopDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (Category != null)
            {
                shopDbContext.Categories.Remove(Category);
                await shopDbContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("Category was not found");
            }
            return Ok(Category.Name + " Was removed");
        }
    }
}

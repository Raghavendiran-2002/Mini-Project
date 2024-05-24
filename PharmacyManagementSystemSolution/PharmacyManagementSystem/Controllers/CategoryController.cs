using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Models.DTOs;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.CategoryDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {        
            _categoryService = categoryService;
            _logger = logger;
        }
        [Authorize]
        [HttpGet("ById")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> GetCategoryById(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.GetCategoryById(Id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Category Id : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPost()]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> AddCategory(AddCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.AddCategory(category);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Category Name : {category.CategoryName} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.GetAllCategories();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Category Get All : Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpDelete()]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> DeleteCategory(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.DeleteCategory(Id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Category Delete : Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPut()]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.UpdateCategory(category);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Category Name : {category.CategoryName} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

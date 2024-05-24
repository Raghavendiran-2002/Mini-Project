using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.DiscountDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService, ILogger<DiscountController> logger)
        {
            _logger = logger;
            _discountService = discountService;
        }
        [Authorize]
        [HttpGet("ById")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Discount>> GetDiscountById(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _discountService.GetDiscountById(Id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Discount Id : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Discount>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable< Discount>>> GetDiscount()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _discountService.GetDiscounts();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Discount All :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPut("updateDiscount")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Discount>> UpdateDiscount(UpdateDiscountDTO updateDiscountDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _discountService.UpdateDiscount(updateDiscountDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Discount Id : {updateDiscountDTO.DiscountID} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPost("AddDiscount")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Discount>> AddDiscount(AddDiscountDTO addDiscountDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _discountService.AddDiscount(addDiscountDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Discount Name : {addDiscountDTO.DiscountName} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Discount>> DeleteDiscountById(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _discountService.DeleteDiscount(Id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Discount Id : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Exceptions.Review;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;
using PharmacyManagementSystem.Models.DTOs.ReviewDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            _logger = logger;
            _reviewService = reviewService;

        }
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForProduct(int productId)
        {
            
       
            if (ModelState.IsValid)
            {
                try
                {
                    var reviews = await _reviewService.GetReviewsForProduct(productId);
                    return Ok(reviews);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Get product review : {productId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");

        }
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(int userId, [FromBody] ReviewCreationDto reviewDto)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    await _reviewService.AddReview(userId, reviewDto);
                    var result = (nameof(GetReviewsForProduct), new { productId = reviewDto.ProductID }, reviewDto);
                    return Created("User Created", result);
                }
                catch (ProductNotFound ex)
                {
                    _logger.LogError($"No Product Found : {reviewDto.ProductID}");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (UserNotPurchasedProduct ex)
                {
                    _logger.LogError($"user not purchased product : {reviewDto.ProductID}");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Get product review user Id : {userId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");

        }
    }

}

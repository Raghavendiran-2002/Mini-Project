using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Interfaces.Repositories;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ShoppingCartDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,ILogger<ShoppingCartController> logger )
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }
        [Authorize]
        [HttpGet("GetCartByUserId")]
        [ProducesResponseType(typeof(IEnumerable< ShoppingCart>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult <IEnumerable<ShoppingCart>>> GetAllCartByUserId(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _shoppingCartService.GetCartByUserId(Id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Shopping cart User Id : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCartItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ShoppingCartItem>> AddItemToCart(AddShoppingCartItemDTO addShoppingCartItemDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _shoppingCartService.AddItemToCart(addShoppingCartItemDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"User Id : {addShoppingCartItemDTO.UserID} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(ShoppingCartItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ShoppingCartItem>> RemoveItemFromCart(int cartId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _shoppingCartService.RemoveItemFromCart(cartId);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"cart Id : {cartId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCartItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ShoppingCartItem>> UpdateItemInCart(UpdateItemInCartDTO updateItemInCartDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _shoppingCartService.UpdateItemInCart(updateItemInCartDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"cart Id : {updateItemInCartDTO.CartID} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

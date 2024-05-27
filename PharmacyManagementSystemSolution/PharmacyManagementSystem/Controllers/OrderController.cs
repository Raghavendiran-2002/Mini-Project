using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Order>> PlaceOrder(int userId, [FromBody] OrderCreationDto orderCreationDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _orderService.PlaceOrder(userId, orderCreationDto);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Place Order : {userId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("GetOrdersBy")]        
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _orderService.GetOrderById(orderId);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Order Id : {orderId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("GetUserOrders")]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrder(int userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _orderService.GetUserOrders(userId);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Place Order : {userId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId, string status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _orderService.UpdateOrderStatus(orderId, status);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Order Id : {orderId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }   

}

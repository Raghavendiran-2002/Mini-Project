using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Exceptions.Payments;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.OrderDTOs;
using PharmacyManagementSystem.Models.DTOs.PaymentDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]       
        public async Task<ActionResult<Payment>> CreatePayment(int userId, [FromBody] PaymentCreationDto paymentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = await _paymentService.CreatePayment(userId, paymentDto);
                    return Created("Payment Added", payment);
                }
                catch (OrderNotFound ex)
                {
                    _logger.LogError($"Order not found");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Payment : {userId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetPaymentsByOrder(int userId, int orderId)
        {
            if (ModelState.IsValid)
            { 
                try
                {                
                var payments = await _paymentService.GetPaymentsByOrder(userId, orderId);
                return Ok(payments);
                }
                catch (OrderNotFound ex)
                {
                    _logger.LogError($"Order not found");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Payment  : {userId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("AllPayments")]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetAllPayments()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payments = await _paymentService.GetAllPayments();
                    return Ok(payments);
                }
                catch (OrderNotFound ex)
                {
                    _logger.LogError($"Order not found");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Payment  :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("PaymentByUser")]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetPaymentsByUser(int userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payments = await _paymentService.GetPaymentsByUser(userId);
                    return Ok(payments);
                }
                catch (OrderNotFound ex)
                {
                    _logger.LogError($"Order not found");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"user Id Payment  :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

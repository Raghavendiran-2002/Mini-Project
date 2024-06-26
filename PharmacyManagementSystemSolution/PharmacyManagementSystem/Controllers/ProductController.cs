﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Exceptions.Order;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs.ProductDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("ById")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProduct(Id);
                    return Ok(result);
                }
                catch (ProductNotFound ex)
                {
                    _logger.LogError($"No Product Found : {Id}");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product Id : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable< Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable< Product>>> GetProduct()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProducts();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product All :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("FilterCategoryId")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryId(int categoryId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProductByCategoryId(categoryId);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product category Id :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("Availability")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductAvailability()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProductBasedOnAvailability();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product category Id :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductName(string productName)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProductByName(productName);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product category Id :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("GetProductByPriceRange")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductPriceRange(int startPrice, int endPrice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.GetProductsByPriceRange(startPrice, endPrice);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product category Id :  Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Product>> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.UpdateProduct(updateProductDTO);
                    return Ok(result);
                }
                catch (ProductNotFound ex)
                {
                    _logger.LogError($"No Product Found : {updateProductDTO.ProductID}");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product update : {updateProductDTO.ProductID} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Product>> AddProduct(AddProductDTO addProductDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.AddProduct(addProductDTO);
                    return Created("Product Created", result);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product add : {addProductDTO.ProductName} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Product>> DeleteProduct(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.DeleteProduct(Id);
                    return Ok(result);
                }
                catch (ProductNotFound ex)
                {
                    _logger.LogError($"No Product Found : {Id}");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Product Delete : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

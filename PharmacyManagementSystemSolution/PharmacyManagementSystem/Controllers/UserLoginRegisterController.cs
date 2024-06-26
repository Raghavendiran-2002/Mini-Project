﻿using log4net.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Exceptions.Auth;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserLoginRegisterController : ControllerBase
    {
        private readonly ILogger<UserLoginRegisterController> _logger;
        private readonly IUserService _userService;
        public UserLoginRegisterController(IUserService userService, ILogger<UserLoginRegisterController> logger)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginReturnDTO>> Login(LoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.Login(userLoginDTO);
                    return Ok(result);
                }
                catch (UnauthorizedAccessException ex) {
                    _logger.LogCritical("User not authenticated");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("User not authenticated ");
                    return Unauthorized(new ErrorModel(404, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check teh object");
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterReturnDTO>> Register(RegisterDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RegisterReturnDTO result = await _userService.Register(userDTO);
                    return Created("User Created", result);
                }
                catch (UserNotRegistered ex)
                {
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.LogCritical("User not authenticated");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return BadRequest(new ErrorModel(501, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check teh object");
        }
    }
}

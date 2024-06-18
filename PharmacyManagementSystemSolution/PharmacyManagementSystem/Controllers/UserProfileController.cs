using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Exceptions.UserProfile;
using PharmacyManagementSystem.Interfaces.Services;
using PharmacyManagementSystem.Models.DBModels;
using PharmacyManagementSystem.Models.DTOs;
using PharmacyManagementSystem.Models.DTOs.UserProfileDTOs;
using PharmacyManagementSystem.Services;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userService;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IUserProfileService userService, ILogger<UserProfileController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("UpdateProfile")]
        [ProducesResponseType(typeof(UserProfileReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginReturnDTO>> UpdateProfile(UpdateUserDTO updateUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.UpdateProfile(updateUser);
                    return Ok(result);
                }
                catch (NoUserFound ex)
                {
                    _logger.LogError($"Update Profile - No user Found Profile Id : {updateUser.UserId} Access Denied");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Update Profile : {updateUser.UserId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpGet("ViewProfile")]
        [ProducesResponseType(typeof(UserProfileReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileReturnDTO>> ViewProfile(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.ViewProfile(Id);
                    return Ok(result);
                }
                catch (NoUserFound ex)
                {
                    _logger.LogError($"View Profile - No user Found Profile Id : {Id} Access Denied");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"View Profile : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(UserProfileReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginReturnDTO>> ResetPassword(ResetPasswordDTO resetPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.ResetPassword(resetPassword);
                    return Ok(result);
                }
                catch(NoUserFound ex)
                {
                    _logger.LogError($"Reset Password - No user Found Profile Id : {resetPassword.UserId} Access Denied");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch(PasswordIncorrect ex)
                {
                    _logger.LogError($"Reset Password - Incorrect Password Profile Id : {resetPassword.UserId} Access Denied");
                    return BadRequest(new ErrorModel(404, ex.Message));

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Reset Password - Profile Id : {resetPassword.UserId} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
        [Authorize]
        [HttpDelete("DeleteProfile")]
        [ProducesResponseType(typeof(UserProfileReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileReturnDTO>> DeleteProfile(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.DeleteProfile(Id);
                    return Ok(result);
                }
                catch (NoUserFound ex)
                {
                    _logger.LogError($"Delete Profile - No user Found Profile Id : {Id} Access Denied");
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Delete Profile : {Id} Access Denied");
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            return BadRequest("All details are not provided. Please check the object");
        }
    }
}

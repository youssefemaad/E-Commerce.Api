using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityModuleDto;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiControllerBase
    {

        #region Login

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
            
        #endregion

        #region Register

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        #endregion

        #region Check Email

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }

        #endregion

        #region Get Current User

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken());
            return Ok(user);
        }


        #endregion

        #region Get Current User Address

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken());
            return Ok(address);
        }

        #endregion

        #region Update Current User Address

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var address = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(GetEmailFromToken(), addressDto);
            return Ok(address);
        }

        #endregion

    }
}

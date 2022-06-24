using Microsoft.AspNetCore.Mvc;
using Console.ApplicationCore.Dtos;
using Console.ApplicationCore.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Console.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;   
        private readonly IAuthenticationService _authenticationService;
        
        public AccountController(
            IUserService userService, 
            ITokenService tokenService,
            IAuthenticationService authenticationService)
        {
            _userService = userService;
            _tokenService = tokenService;   
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/account/login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            var result = await _userService.VerifyUserSignInAsync(dto); 

            if(!result.Success)
                return BadRequest(result.Message);

            var response = await _authenticationService.SigninAsync(result.Data);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/account/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);   

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}

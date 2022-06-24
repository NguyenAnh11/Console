using Console.Module.Localization.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Console.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/api/home")]
        public IActionResult Get()
        {
            var value = _localizer[AccountConstrant.Account_Error_EmailAlreadyUsed];

            return Ok(value);
        }
    }
}

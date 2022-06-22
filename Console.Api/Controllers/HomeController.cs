using Microsoft.AspNetCore.Mvc;
using Console.Module.Email.Services;
using System.Threading.Tasks;

namespace Console.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEmailService _emailService;   
        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("/api")]
        public async Task<IActionResult> Send([FromBody] MessageDto dto)
        {
            await _emailService.SendAsync(dto.To, dto.Subject, dto.Body);

            return Ok();
        }
    }

    public class MessageDto
    {
        public string To { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; }    
    }
}

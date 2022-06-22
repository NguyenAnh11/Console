using System.Threading.Tasks;

namespace Console.Module.Email.Services.SendGrid
{
    public class SendGridEmailService : IEmailService
    {
        public Task SendAsync(string to, string subject, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}

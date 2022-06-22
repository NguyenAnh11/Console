using System.Threading.Tasks;

namespace Console.Module.Email.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}

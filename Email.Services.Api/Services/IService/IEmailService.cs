using Email.Services.Api.Model;

namespace Email.Services.Api.Services.IService
{
    public interface IEmailService
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}

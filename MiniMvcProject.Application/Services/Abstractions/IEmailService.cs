using System.Net.Mail;
using System.Net;

namespace MiniMvcProject.Application.Services.Abstractions;

public interface IEmailService
{
    void SendEmail(string email, string subject, string body);
}

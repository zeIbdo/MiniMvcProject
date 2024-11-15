using MiniMvcProject.Application.Services.Abstractions;
using System.Net.Mail;
using System.Net;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class EmailManager : IEmailService
    {
        public void SendEmail(string email, string subject, string body)
        {
            NetworkCredential credential = new NetworkCredential("imedetzade5@gmail.com", "sdvd tmbx bxwa oozv");
            MailMessage message = new MailMessage();
            message.From = new MailAddress("imedetzade5@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {

                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Send(message);
            }
        }
    }
}

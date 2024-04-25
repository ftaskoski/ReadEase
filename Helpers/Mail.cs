using System.Net.Mail;
using System.Net;

namespace ReadEase_C_.Helpers
{
    public class Mail
    {
        private readonly IConfiguration _configuration;

        public Mail(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SendEmail(string recipientEmail)
        {
            try
            {
                string senderEmail = _configuration["Mail:e-mail"]; 
                string senderPassword = _configuration["Mail:pass"]; 

                MailMessage mail = new(senderEmail, recipientEmail);
                SmtpClient client = new()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderPassword)
                };


                mail.Subject = "Registration Successful";
                mail.Body = "Congratulations! You have successfully registered.";

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Bad Email" + ex.Message);

            }
        }


    }
}

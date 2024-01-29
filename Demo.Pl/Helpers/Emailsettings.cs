using Demo.DAL.Models;
using Demo.Pl.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace Demo.Pl.Helpers
{
    public class Emailsettings : IEmailSettings
    {
        private readonly MailSettings options;

        public Emailsettings(IOptions<MailSettings> options)
        {
            this.options = options.Value;
        }




        public void SendEmail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(options.Email),
                Subject = email.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(email.To));
            var builder = new BodyBuilder();
            builder.HtmlBody = email.Body;
            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(options.DisplayName, options.Email));
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(options.Host,options.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Email, options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }











        //        public  void SendEmail(Email email)
        //{










        //	//var client= new SmtpClient("linkdev.com",222);
        //	//client.EnableSsl = true;
        //	//client.Credentials = new NetworkCredential("ahmed.nasr@linkdev.com", "AHMED1234$");
        //	////client.Send("ahmed.nasr@linkdev.com", email.To, email.Subject, email.Body);
        //	//var client = new SmtpClient("smtp.gmail.com", 587);
        //	//client.EnableSsl = true;
        //	//client.Credentials = new NetworkCredential("marinamagdywalliam@gmail.com", "msfebsmlkcxshpsv");
        //	//client.Send("marinamagdywalliam@gmail.com", email.To, email.Subject, email.Body);
        //}

    }
}

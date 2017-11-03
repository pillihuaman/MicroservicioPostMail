using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Mail.Infraestructure.Messenger
{
    public class Message
    {

        private readonly string _server;
        private readonly int _port;
         public MailAddress from { get; set; }
         public List<MailAddress> To { get; set; }
         public List<MailAddress> Cc { get; set; }
         public List<MailAddress> Bcc { get; set; }
         public string Subject { get; set; }
         public BodyMessage Body { get; set; }
        public Message()
        {
            _server = Environment.GetEnvironmentVariable("SERVIDOR_SMTP_IP");
            _port = int.Parse(Environment.GetEnvironmentVariable("SERVIDOR_SMTP_PORT"));
        }

        public void Send()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from.Name, from.Address));
            foreach (var email in To)
            {
                message.To.Add(new MailboxAddress(email.Name, email.Address));
                
            }
            if (Cc != null)
            {
                foreach (var email in Cc)
                {
                    message.Cc.Add(new MailboxAddress(email.Name
                        , email.Address));
                }
            }
            if (Bcc != null)
            {
                foreach (var email in Bcc)
                {
                    message.Bcc.Add(new MailboxAddress(email.Name, email.Address));                 }
            }
            message.Subject = Subject;
            message.Body = Body.Type == TypeBody.Text ? new TextPart(Body.Content) :
               (new BodyBuilder() { HtmlBody = Body.Content }).ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Timeout = 5000;
                client.Connect(_server, _port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Send(message);
                client.Disconnect(true);
            }


        }

    }
}

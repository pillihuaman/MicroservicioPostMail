using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain = Pagoefectivo.Mail.Domain;
using Mail.Infraestructure.Messenger;
using System.Text.RegularExpressions;

namespace Mail.Application
{
    public class ServiceMessenger
    {
        public static void Send(Domain.Mail messageMail)
        {


            var message = new Message
            {
                from = new MailAddress() { Name = messageMail.From.Name, Address = messageMail.From.Address },
                Subject = messageMail.Subject,
                Body = new BodyMessage() { Content = messageMail.Body, Type = TypeBody.Html },
                To = new List<MailAddress>()


            };
            foreach (var email in messageMail.To)
            {
                message.To.Add(new MailAddress() { Name = email.Name, Address = email.Address });
            }
            if (messageMail.CC != null)
            {
                message.Cc = new List<MailAddress>();
                foreach (var email in messageMail.CC)
                {
                    message.Cc.Add(new MailAddress() { Name = email.Name, Address = email.Address });
                }
            }
            if (messageMail.DCC != null)
            {
                message.Bcc = new List<MailAddress>();
                foreach (var email in messageMail.DCC)
                {
                    message.Bcc.Add(new MailAddress() { Name = email.Name, Address = email.Address });
                }
            }

            message.Send();

        }

        public static bool IsMailValid(string mailtoValided)
        {
            string[] mails = mailtoValided.Split(',');
            const string expresion =   @"\A(?:[a - z0 - 9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isEmail = false;
            foreach (var mail in mails)
            {
                isEmail = Regex.IsMatch(mail, expresion, RegexOptions.IgnoreCase);
                if (!isEmail) break;

            }
          
            return isEmail; ;
        }


    }
}

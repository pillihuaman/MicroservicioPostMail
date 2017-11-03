using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagoefectivo.Mail.Domain;
using Newtonsoft.Json;
using Mail.Infraestructure.Notification;

namespace Mail.Application
{
    public class ServicePublication
    {
        public static void SendMessage(Notification notificacion)
        {

            var message = JsonConvert.SerializeObject(notificacion);
            var topiccrn = Environment.GetEnvironmentVariable("TOPIC_ARN");
            var publication = new BaseNotification(topiccrn);
            publication.SendMessage("Notificaction", message);
        }


    }
}

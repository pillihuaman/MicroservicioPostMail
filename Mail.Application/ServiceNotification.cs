using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo = Mail.Infraestructure.DynamoRepository;
using Domain = Pagoefectivo.Mail.Domain;
using Newtonsoft.Json;
using Amazon.DynamoDBv2.Model;
namespace Mail.Application
{
    public class ServiceNotification
    {

        public static void Save(Domain.Notification notification)
        {
            var obj = new Dynamo.Notification
            {
                IdNotification = notification.IdNotification,
                IdTemplate = notification.TemplateSend.IdTemplate,
                Path = notification.TemplateSend.path,
                Fields = JsonConvert.SerializeObject(notification.TemplateSend.Field),
                Mail = JsonConvert.SerializeObject(notification.MailSend),
                Reminder = notification.ReminderSend.IsRemainder,
                Begin = notification.ReminderSend.Begin,
                End = notification.ReminderSend.End,
                Timer = notification.ReminderSend.Timer,
                NextTime = notification.ReminderSend.NetTime,
                State = notification.State,
                Origin = notification.Origin,
                Language = notification.Language,
                Try = notification.Try,
                CreationDate = DateTime.Now

            };

            var baserepositorio = new Dynamo.BaseRepository();
            baserepositorio.InsertOrupdate(obj);
        }
        public static void Delete(Domain.Notification notification)
        {

            var obj = new Dynamo.Notification
            {

                IdNotification = notification.IdNotification,
                IdTemplate = notification.TemplateSend.IdTemplate,
                Fields = JsonConvert.SerializeObject(notification.TemplateSend.Field),
                Mail = JsonConvert.SerializeObject(notification.MailSend),
                Reminder = notification.ReminderSend.IsRemainder,
                Begin = notification.ReminderSend.Begin,
                End = notification.ReminderSend.End,
                Timer = notification.ReminderSend.Timer,
                NextTime = notification.ReminderSend.NetTime,
                State = notification.State,
                CreationDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            var baseRepositorio = new Dynamo.BaseRepository();
            baseRepositorio.Delete(obj);
        }
       public static Domain.Notification GetNotification(string id)
        {
            var notificationSend = new Dynamo.BaseRepository().GetItem<Dynamo.Notification>(id);
            return Parse(notificationSend);

        }
        public static Domain.Notification Parse(Dynamo.Notification notificacionsend)
        {

            var notification = new Domain.Notification
            {
                IdNotification = notificacionsend.IdNotification,
                Origin = notificacionsend.Origin,
                Language = notificacionsend.Language,
                TemplateSend = ServiceTemplate.GetTemplate(notificacionsend.IdTemplate)

            };
            notification.TemplateSend.Field = JsonConvert.DeserializeObject<Domain.FieldTemplate[]>(notificacionsend.Fields);
            notification.TemplateSend.path = notificacionsend.Path;
            notification.TemplateSend.Language = notification.Language;
            notification.MailSend = JsonConvert.DeserializeObject<Domain.Mail>(notificacionsend.Mail);
            notification.ReminderSend = new Domain.Remainder()
            {
                IsRemainder = notificacionsend.Reminder,
                Begin = notificacionsend.Begin,
                End = notificacionsend.End,
                NetTime = notificacionsend.NextTime,
                Timer = notificacionsend.Timer,

            };
            notificacionsend.State = notificacionsend.State;
            notificacionsend.CreationDate = notificacionsend.CreationDate;
            notification.Try = notificacionsend.Try;
            return notification;
        }
        public static Domain.Notification Parse(StreamRecord record)
        {
            var notificationSend = new Dynamo.NotificationRepository().Parse(record.NewImage);
            return Parse(notificationSend);
        }
      
    }
    public enum StateNotification
    {

        Success = 0,
        Send = 1,
        Fail = 2
    }

}


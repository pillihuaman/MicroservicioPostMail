using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagoefectivo.Mail.Domain
{
    public class Notification
    {
        [JsonIgnore]
        public string IdNotification { get; set; }
        [JsonProperty(PropertyName = "template_send", Required = Required.Always)]
        public Template TemplateSend { get; set; }
        [JsonProperty(PropertyName = "mail_send", Required = Required.Always)]
        public Mail MailSend { get; set; }
        [JsonProperty(PropertyName = "reminder_send", Required = Required.Always)]
        public Remainder ReminderSend { get; set; }
        [JsonProperty(PropertyName = "origin_send", Required = Required.Default)]
        public int Origin { get; set; }
        [JsonProperty(PropertyName = "language_send", Required = Required.Default)]
        public string Language { get; set; }
        [JsonIgnore]
        public int State { get; set; }
        [JsonIgnore]
        public int Try { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public DateTime ModifyDate { get; set; }
    }

    public enum NotificationOrigin
    {
        Charges = 1,
        Cip = 2,
        Recharges = 3,
        Payments = 4
    }
}
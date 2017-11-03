using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MiocroServicioPagoefectivoMailPost.Model
{
    public class Notification
    {
        [JsonIgnore]
        public int IdNotificacion { get; set; }
        [JsonProperty(PropertyName = "template_send", Required = Required.Always)]
        public Template TemplateSend { get; set; }
        [JsonProperty(PropertyName = "mail_send", Required = Required.Always)]
        public Mail MailSend { get; set; }
        [JsonProperty(PropertyName = "remainder_send", Required = Required.Always)]
        public Remainder RemainderSend { get; set; }
        [JsonProperty(PropertyName = "origin_send", Required = Required.Always)]
        public int Origin { get; set; }
        [JsonIgnore]
        public string Language { set; get; }
        [JsonIgnore]
        public int State { get; set; }
        [JsonIgnore]
        public int Try { get; set; }
        [JsonIgnore]
        public  DateTime CreationDate {get;set;}
        [JsonIgnore]
        public DateTime ModifyDate { get; set; }
    }

    public enum NotificationOrigin

    {
         Charges=1,  //cargo o cobro
         Cip=2,
         Recharges=3,//recargo recobro
         Payments=4 //pago


    }
}

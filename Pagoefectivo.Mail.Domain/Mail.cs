using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagoefectivo.Mail.Domain
{
    public class Mail
    {
      
        [JsonProperty(PropertyName = "from", Required = Required.Always)]
        public MailBox From { get; set; }
        [JsonProperty(PropertyName = "to", Required = Required.Always)]
        public MailBox[] To { get; set; }

        [JsonProperty(PropertyName = "subject", Required = Required.Always)]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "cc", Required = Required.Always)]
        public MailBox[] CC { get; set; }

        [JsonProperty(PropertyName = "dcc", Required = Required.Default)]
        public MailBox[] DCC { get; set; }

        public string Body { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiocroServicioPagoefectivoMailPost.Model
{
    public class Mail
    {

        [JsonProperty(PropertyName = "to", Required = Required.Always)]
        public string To { get; set; }
        [JsonProperty(PropertyName = "from", Required = Required.Always)]
        public string From { get; set; }
        [JsonProperty(PropertyName = "subject", Required = Required.Always)]
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "cc", Required = Required.Always)]
        public string CC { get; set; }
        [JsonProperty(PropertyName ="dcc",Required=Required.Default)]
         public string DCC { get; set; }
         public string Body { get; set; }
                        
    }
}

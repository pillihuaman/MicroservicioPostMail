using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagoefectivo.Mail.Domain
{
    public class Remainder
    {
        [JsonProperty(PropertyName = "isRemainder")]
        public bool IsRemainder { get; set; }
        [JsonProperty(PropertyName = "begin", Required = Required.Always)]
        public DateTime Begin { get; set; }
        [JsonProperty(PropertyName = "end", Required = Required.Always)]
        public DateTime End { get; set; }
        [JsonProperty(PropertyName ="timer",Required =Required.Always)]
        public int Timer { get; set; }
        [JsonIgnore]
        public DateTime NetTime { get; set; }


    }
}

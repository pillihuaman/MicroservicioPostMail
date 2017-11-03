using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagoefectivo.Mail.Domain
{
    public class FieldTemplate
    {
        [JsonProperty(PropertyName ="field",Required =Required.Always)]
      public string Field { get; set; }
        [JsonProperty(PropertyName ="value",Required =Required.Always)]
     public string Value { get; set; }
    
    }
}

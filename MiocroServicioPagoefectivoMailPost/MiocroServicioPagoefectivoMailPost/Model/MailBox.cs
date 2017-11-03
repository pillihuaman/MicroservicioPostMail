using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiocroServicioPagoefectivoMailPost.Model
{
    public class MailBox
    {
        [JsonProperty(PropertyName= "name", Required= Required.Always )]
         public string Name { get; set; }
        [JsonProperty(PropertyName= "addres", Required=Required.Always)]
         public string Addres { get; set; }
    }
}

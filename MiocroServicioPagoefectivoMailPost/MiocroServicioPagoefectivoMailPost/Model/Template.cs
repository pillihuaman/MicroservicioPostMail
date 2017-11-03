using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiocroServicioPagoefectivoMailPost.Model
{
    public class Template
    {
        [JsonProperty(PropertyName= "id_template",Required=Required.Always)]
        public int IdTemplate { get; set; }
        [JsonIgnore]
        public string Bucket { set; get; }
        [JsonProperty(PropertyName ="path",Required =Required.Always)]
        public string Path { get; set; }
        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public string Languaje { get; set; }
        [JsonProperty(PropertyName = "field",Required =Required.Always)]
        public FieldTemplate[] field { get; set; }

    }
}

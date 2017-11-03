using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagoefectivo.Mail.Domain
{
    public class Template
    {

        public int IdTemplate { get; set; }
        public string Bucket { get; set; }
        public string path { get; set; }
        public string FileName { get; set; }
        public string Language { get; set; }
        public FieldTemplate[] Field { get; set; }


    }
}

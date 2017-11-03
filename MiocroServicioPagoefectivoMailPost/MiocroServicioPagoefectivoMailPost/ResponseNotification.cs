using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiocroServicioPagoefectivoMailPost
{
    public class ResponseNotification
    {
        public bool Error { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }

           
    }
}

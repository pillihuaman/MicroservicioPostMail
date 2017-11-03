using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiocroServicioPagoefectivoMailPost.Model
{
    public class ChargeDetail
    {
        public int OrderId { get; set; }
        public int Status { get; set; }

    }
    public enum ChargeDetailStatus
    {
         pending=24,
         Generated,
         Paiddout,
         Expired,
         Canceled,
         Deleted,
         Inprocess

    }
}

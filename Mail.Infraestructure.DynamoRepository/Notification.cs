using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Infraestructure.DynamoRepository
{
    [DynamoDBTable("mail-sending-pagoefectivo-pe")]
    public class Notification
    {
        public string IdNotification { get; set; }
        public int IdTemplate { get; set; }
        public string Path { get; set; }
        public string Fields { get; set; }
        public string Mail { get; set; }
        public bool Reminder { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int Timer { get; set; }
        public DateTime NextTime { get; set; }
        public int State { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Origin { get; set; }
        public string Language { get; set; }
        public int Try { get; set; }
    }
}

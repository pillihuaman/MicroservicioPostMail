using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Mail.Infraestructure.DynamoRepository
{
    [DynamoDBTable("mail-template-pagoefectivo-pe")]
    public class Template
    {
        [DynamoDBHashKey]
        public int IdTemplate { get; set; }
        public string path { get; set; }
        public string FileName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Infraestructure.Messenger
{
    public class BodyMessage
    {
         public string Content { get; set; }
        public TypeBody Type { get; set; }



    }

    public enum TypeBody
    {

        Text,
        Html
    }
}

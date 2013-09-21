using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulApp.Bussiness
{
    public class PersonEntity
    {
        public long UID { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public decimal Property{get;set;}
        public string Description { get; set; }

    }
}

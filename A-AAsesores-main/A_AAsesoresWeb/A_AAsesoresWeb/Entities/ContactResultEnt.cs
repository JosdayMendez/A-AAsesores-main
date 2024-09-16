using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresWeb.Entities
{
    public class ContactResultEnt
    {
        public int Id { get; set; }
        public string UserIdentification { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }
}
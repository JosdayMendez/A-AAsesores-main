using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresWeb.Entities
{
    public class ContactEnt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public StatusEnt Status1 { get; set; }
        public virtual UserEnt Users { get; set; }
    }
}
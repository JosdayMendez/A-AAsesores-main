using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresAPI.Entities
{
    public class ContactEnt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual Users Users { get; set; }
    }
}
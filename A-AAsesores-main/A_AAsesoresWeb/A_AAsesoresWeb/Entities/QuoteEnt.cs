using System;
using System.Collections.Generic;
using System.Web;

namespace A_AAsesoresWeb.Entities
{
    public class QuoteEnt
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int QuoteDetails { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public string DocUrl { get; set; }
        public string Details { get; set; }
        public List<HttpPostedFileBase> Docs { get; set; }
        public List<string> StringList { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresAPI.Entities
{
    public class CurrencyEnt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public string Symbol { get; set; }
    }
}
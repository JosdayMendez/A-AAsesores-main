using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresAPI.Entities
{
    public class DistrictEnt
    {
        public int Id { get; set; }
        public int Canton { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
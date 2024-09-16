using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_AAsesoresWeb.Entities
{
    public class MaintenanceEnt
    {
        public List<RoleEnt> Roles { get; set; }
        public List<ProvinceEnt> Province { get; set; }
        public List<TransactionTypeEnt> TransactionType { get; set; }
        public List<DocumentTypeEnt> DocumentType { get; set; }
        public List<PropertyTypeEnt> PropertyType { get; set; }
        public List<StatusEnt> Status { get; set; }
        public List<DistrictEnt> District { get; set; }
        public List<CantonEnt> Canton { get; set; }
        public List<QuoteRoomEnt> Room { get; set; }
    }
}
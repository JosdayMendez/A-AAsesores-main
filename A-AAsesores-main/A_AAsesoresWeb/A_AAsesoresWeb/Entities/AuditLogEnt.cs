using System;

namespace A_AAsesoresWeb.Entities
{
    public class AuditLogEnt
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string CurrentAudit { get; set; }
        public DateTime CreationDateAudit { get; set; }
    }
}
using System;

namespace A_AAsesoresWeb.Entities
{
    public class AppointmentEnt
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        //Relationships with other entities
        public string StatusName { get; set; }
        public string PropertyName { get; set; }
        public string EmployeeName { get; set; }
        public string IdentificationEmployee { get; set; }
        public string UserName { get; set; }
        public string IdentificationUser { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
    }
}
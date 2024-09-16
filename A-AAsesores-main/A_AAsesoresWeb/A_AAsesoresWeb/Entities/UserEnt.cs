using System;

namespace A_AAsesoresWeb.Entities
{
    public class UserEnt
    {
        public int Id { get; set; }
        public int DocumentType { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string FullName { get; set; }
    }
}
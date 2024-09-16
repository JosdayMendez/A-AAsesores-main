using System;

namespace A_AAsesoresWeb.Entities
{
    public class EmployeeEnt
    {

        public int Id { get; set; }
        public int IdTEmployee { get; set; } //Creo que esta de mas revisar
        public int UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public string ImageProfile { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string Identification { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public int DocumentType { get; set; }
        //Esto es para el cambio de contraseña
        public bool IsTemporary { get; set; }
        public string PasswordTemp { get; set; }

        public string Token { get; set; }

    }
}
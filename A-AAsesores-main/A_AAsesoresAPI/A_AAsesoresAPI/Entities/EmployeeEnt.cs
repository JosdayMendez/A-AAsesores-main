using System;

namespace A_AAsesoresAPI.Entities
{
    public class EmployeeEnt
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int IdTEmployee { get; set; } //Creo que esta de mas revisarF
        public int UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string ImageProfile { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string Identification { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public int DocumentType { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        //Esti es un campo para saber si la contraseña es temporal
        //Esto es para el cambio de contraseña
        public bool IsTemporary { get; set; }
        public string PasswordTemp { get; set; }
        public int FailedAttempts { get; set; }
        public int IdRole { get; set; }
        public string Token { get; set; }
    }
}
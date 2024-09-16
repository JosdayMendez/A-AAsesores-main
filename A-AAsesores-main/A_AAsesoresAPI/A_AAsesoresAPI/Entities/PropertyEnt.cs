using System;

namespace A_AAsesoresAPI.Entities
{
    public class PropertyEnt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EmployeeId { get; set; }
        public int Currency { get; set; }
        public decimal Price { get; set; }
        public decimal AreaLand { get; set; }
        public decimal AreaBuild { get; set; }
        public string Description { get; set; }
        public int PropertyType { get; set; }
        public int TransactionType { get; set; }
        public int PropertyStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime SoldDate { get; set; }
    }
}
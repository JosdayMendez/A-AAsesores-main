using System;

namespace A_AAsesoresAPI.Entities
{
    public class NewsEnt
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}

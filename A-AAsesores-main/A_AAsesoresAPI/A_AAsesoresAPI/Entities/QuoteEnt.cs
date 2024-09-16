using System;

namespace A_AAsesoresAPI.Entities
{
    public class QuoteEnt
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int QuoteDetails { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public string DocUrl { get; set; }
        public string Details { get; set; }
    }
}
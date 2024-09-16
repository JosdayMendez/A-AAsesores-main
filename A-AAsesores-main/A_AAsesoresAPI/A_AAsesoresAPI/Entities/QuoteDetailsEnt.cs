using System;

namespace A_AAsesoresAPI.Entities
{
    public class QuoteDetailsEnt
    {
        public int Id { get; set; }
        public int WalkInClosetQuantity { get; set; }
        public int SecondaryRooms { get; set; }
        public int SecondaryBathrooms { get; set; }
        public String Details { get; set; }
    }
}
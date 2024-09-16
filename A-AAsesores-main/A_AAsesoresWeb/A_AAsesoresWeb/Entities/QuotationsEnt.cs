using System.Collections.Generic;

namespace A_AAsesoresWeb.Entities
{
    public class QuotationsEnt
    {
        public QuoteEnt QuoteEnt { get; set; }
        public QuoteDetailsEnt QuoteDetailsEnt { get; set; }
        public QuoteDetailsPerRoomEnt QuoteDetailsPerRoomEnt { get; set; }
        public List<QuoteRoomEnt> QuoteRoomEnt { get; set; }
        public List<int> Rooms { get; set; }
        public List<int> RoomQuantity { get; set; }
        public UserEnt UserEnt { get; set; }
        public List<string> RoomNames { get; set; }
    }
}
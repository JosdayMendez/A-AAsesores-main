using System.Collections.Generic;

namespace A_AAsesoresWeb.Entities
{
    public class QuoteRoomEnt
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public List<int> Rooms { get; set; }
        public bool IsActive { get; set; }

    }
}
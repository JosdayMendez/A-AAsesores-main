namespace A_AAsesoresWeb.Entities
{
    public class QuoteResultEnt
    {
        public int QuoteId { get; set; }
        public string ClientFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ClientIdentificationNumber { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public string DocUrl { get; set; }
        public string Details { get; set; }
        public string RoomDetails { get; set; }
    }
}
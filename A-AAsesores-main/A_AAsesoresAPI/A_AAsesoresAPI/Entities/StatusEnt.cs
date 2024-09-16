namespace A_AAsesoresAPI.Entities
{
    public class StatusEnt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RelatedTable { get; set; }
        public bool IsActive { get; set; }
    }
}
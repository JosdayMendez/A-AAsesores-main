namespace A_AAsesoresWeb.Entities
{
    public class PropertyAddressEnt
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int DistrictId { get; set; }
        public string OtherSigns { get; set; }
        //A continuación los parámetros definidos para trabajar según lo requerido
        public string FullDirection { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
        public int CantonId { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }

    }
}
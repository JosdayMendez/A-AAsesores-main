using System;
using System.Collections.Generic;
using System.Web;

namespace A_AAsesoresWeb.Entities
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
        public DateTime? SoldDate { get; set; }
        //A partir de aquí son los agregados para las tablas adicionales
        //Campos para direccion
        public string FullAddress { get; set; }
        public int Province { get; set; }
        public string ProvinceName { get; set; }
        public int Canton { get; set; }
        public string CantonName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
        public string OtherSigns { get; set; }
        //Campos para imagenes
        //Este es para traer la ubicación de las BD
        public List<PropertyImgEnt> ImagesInfo { get; set; }
        public List<PropertyDocEnt> DocsInfo { get; set; }
        //Este es para tratar las imágenes en el controlador
        public List<HttpPostedFileBase> Images { get; set; }
        //Campos para documentos
        public List<HttpPostedFileBase> Docs { get; set; }
        //Esto es para traer el nombre del estado, tipo de propiedad y tipo de transacción
        public string PropertyStatusName { get; set; }
        public string PropertyTypeName { get; set; }
        public string TransactionTypeName { get; set; }
        //Información del empleado
        public string EmployeeName { get; set; }
        public string EmployeeMail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeImage { get; set; }
        public string Symbol { get; set; }

    }
}
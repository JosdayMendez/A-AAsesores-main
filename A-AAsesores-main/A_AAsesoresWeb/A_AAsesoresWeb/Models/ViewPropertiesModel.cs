using A_AAsesoresWeb.Entities;
using System.Collections.Generic;

namespace A_AAsesoresWeb.Models
{
    public class ViewPropertiesModel
    {
        public List<PropertyEnt> Properties { get; set; }
        public FiltroEnt Filter { get; set; }
        public List<ProvinceViewModel> ProvinceViewModels { get; set; }
        public List<PropertyTypeViewModel> PropertyTypeViewModels { get; set; }
        public List<TransactionTypeViewModel> TransactionTypeViewModels { get; set; }

        public class ProvinceViewModel
        {
            public int ProvinceId { get; set; }
            public string ProvinceName { get; set; }
        }

        public class PropertyTypeViewModel
        {
            public int PropertyType { get; set; }
            public string PropertyTypeName { get; set; }
        }

        public class TransactionTypeViewModel
        {
            public int TransactionType { get; set; }
            public string TransactionTypeName { get; set; }
        }
    }
}
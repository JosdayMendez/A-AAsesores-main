using System.Collections.Generic;

namespace A_AAsesoresWeb.Entities
{
    public class FiltroEnt
    {
        public string SearchText { get; set; } // Para el campo de búsqueda
        public decimal? MinPrice { get; set; } // Para el rango de precios mínimo
        public decimal? MaxPrice { get; set; } // Para el rango de precios máximo
        public string RangePrice { get; set; } // Para el rango de precios
        public List<int> PropertyTypes { get; set; } // Para los tipos de propiedad seleccionados
        public List<int> TransactionTypes { get; set; } // Para los tipos de transacción seleccionados
        public List<int> Provinces { get; set; } // Para las provincias seleccionadas
    }
}
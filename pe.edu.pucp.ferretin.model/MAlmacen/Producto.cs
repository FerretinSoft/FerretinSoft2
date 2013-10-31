using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    partial class Producto
    {
        public Categoria listaCategoria{get;set;}
        public String cadenaCategoria { get; set; }
        public String almacen { get; set; }
        public int stock { get; set; }
        public int stockMinimo { get; set; }
        public String unidadMedida { get; set; }
        public String materialBase1 { get; set; }
        public String materialBase2 { get; set; }

        public int descuento { get; set; }
        public int puntos { get; set; }
        
    }
}

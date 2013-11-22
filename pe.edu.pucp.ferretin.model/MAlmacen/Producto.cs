using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    partial class Producto
    {
        public IEnumerable<Categoria> listaCategoria{get;set;}

        public String _cadenaCategoria;
        public String cadenaCategoria
        {
            get
            {
                String cad = "";
                foreach (ProductoCategoria pc in ProductoCategoria)
                {
                    if (cad != "") cad += ", ";
                    cad += pc.Categoria.nombre;
                }

                if (cad!="")
                    _cadenaCategoria = cad;
                return _cadenaCategoria;
            }
            set{

                _cadenaCategoria = value;
            }
        }
        public String almacen { get; set; }
        public int stock { get; set; }
        public int stockMinimo { get; set; }
        public String unidadMedida { get; set; }
        public String materialBase1 { get; set; }
        public String materialBase2 { get; set; }
        public int descuento { get; set; }
        public int puntos { get; set; }

        public string monedaString
        {
            get
            {
                return moneda == 0 ? "Soles" : "Dolares";
            }
        }


        public string precioMonedaString
        {
            get
            {
                if (moneda == null || precioLista == null || precioLista <= 0) return "No asignado";
                return (moneda == 0 ? "S/." : "$  ") + precioLista.ToString();
            }
        }

    }
}

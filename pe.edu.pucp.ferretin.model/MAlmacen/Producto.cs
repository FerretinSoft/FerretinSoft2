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
        public String cadenaCategoria { get; set; }
    }
}

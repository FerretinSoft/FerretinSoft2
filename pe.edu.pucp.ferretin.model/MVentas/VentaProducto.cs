using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class VentaProducto
    {
        partial void OncantidadChanged()
        {
            montoParcial = cantidad * Producto.precioLista * (Producto.descuento/100);
        }
    }
}

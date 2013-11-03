using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class VentaProducto : INotifyPropertyChanged
    {
        partial void OncantidadChanged()
        {
            montoParcial = cantidad * Producto.precioLista * ((100-Producto.descuento)/100);
            Venta.total = (from vp in Venta.VentaProducto select vp.montoParcial).Sum();
        }
    }
}

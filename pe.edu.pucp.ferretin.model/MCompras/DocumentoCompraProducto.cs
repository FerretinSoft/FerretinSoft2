using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class DocumentoCompraProducto : INotifyPropertyChanged
    {
        partial void OncantidadChanged()
        {
            if (cantidad <= 0)
            {
                cantidad = 1;
                return;
            }

            if (Producto != null)
            {
                montoParcial =  Decimal.Round((cantidad * precioUnit).Value,2);
            }
        }

        partial void OnprecioUnitChanged()
        {
            if (precioUnit <= 0)
            {
                precioUnit = 1;
                return;
            }

            if (Producto != null)
            {
                montoParcial = Decimal.Round((cantidad * precioUnit).Value, 2);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class DevolucionProducto
    {
   
        partial void OncantidadChanged()
        {
            if (moneda != null)
            {
                decimal? productoPrecioLista = precioUnitario * (moneda == 0? 1 : (Devolucion.Venta.tipoCambio != null && Devolucion.Venta.tipoCambio <= 0) ? 1 : Devolucion.Venta.tipoCambio);
                decimal?  montoParcial = cantidad * productoPrecioLista;
                monto = Decimal.Round(montoParcial.Value, 2);
            }
            if (Devolucion != null){
                Devolucion.subTotal = (from dp in Devolucion.DevolucionProducto select dp.monto).Sum();
            }
        }

        public string monedaString
        {
            get
            {
                return moneda == 0 ? "Soles" : "Dolares";
            }
        }

        partial void OnmotivoChanged()
        {
            
        }
    }
}

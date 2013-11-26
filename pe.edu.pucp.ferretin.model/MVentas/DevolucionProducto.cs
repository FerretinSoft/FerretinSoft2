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
            if (moneda != null && canjeado == false)
            {
                decimal? productoPrecioLista = precioUnitario * (moneda == 0? 1 : (Devolucion.Venta.tipoCambio != null && Devolucion.Venta.tipoCambio <= 0) ? 1 : Devolucion.Venta.tipoCambio);
                decimal?  montoParcial = cantidad * productoPrecioLista;
                monto = Decimal.Round(montoParcial.Value, 2);
            }
            else if (canjeado == true)
            {
                puntosParciales = precioPuntos * cantidad;
            }
            if (Devolucion != null){
                Devolucion.total = (from dp in Devolucion.DevolucionProducto select dp.monto).Sum();
                Devolucion.puntosDevueltos = (from dp in Devolucion.DevolucionProducto select dp.puntosParciales).Sum();
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

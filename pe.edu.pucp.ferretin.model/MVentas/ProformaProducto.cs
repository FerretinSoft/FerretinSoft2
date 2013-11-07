using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class ProformaProducto
    {
        partial void OncantidadChanged()
        {
            //Cantidad no puede ser negativa
            if (cantidad < 0) cantidad = 0;

            

            //Busco si tiene promocion, aplico el descuento
            if (PromocionActual != null)
            {
                if (cantidad >= PromocionActual.cantMulUnidades)
                {
                    descuentoPorcentaje = PromocionActual.descuento;
                }
                else
                {
                    descuentoPorcentaje = 0;
                }
            }
            else
            {
                descuentoPorcentaje = 0;
            }

            //Actualizo monto parcial
            if (Producto != null)
            {
                decimal? desc = ((100 - (descuentoPorcentaje == null ? 0 : descuentoPorcentaje)) / 100);
                //Calcular la cantidad de productos que tienen descuento
                int? prodConDesc = ((PromocionActual != null && cantidad >= PromocionActual.cantMulUnidades) ? (cantidad / PromocionActual.cantMulUnidades) * PromocionActual.cantMulUnidades : 0);
                //Calcular Si se ha superamo el maximo permitido por venta
                prodConDesc = prodConDesc > 0 && prodConDesc > (PromocionActual.maxPromVenta * PromocionActual.cantMulUnidades) ? (PromocionActual.maxPromVenta * PromocionActual.cantMulUnidades) : prodConDesc;
                //Calculo la cantidad de productos sin descuento
                int? prodSinDesc = cantidad - prodConDesc;
                //Hallo el resultado de los productos con descuento y sin descuento
                descuento = (prodConDesc * Producto.precioLista * (1 - desc));
                montoParcial = Decimal.Round((prodConDesc * Producto.precioLista * desc + prodSinDesc * Producto.precioLista).Value, 2);
                montoReal = cantidad * Producto.precioLista;
            }
        }


        public PromocionProducto PromocionActual { get; set; }
    }
}

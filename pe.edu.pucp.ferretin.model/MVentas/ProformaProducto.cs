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
                    descuentoPorcentaje = 1/Decimal.Round(1/PromocionActual.descuentoPorcentaje.Value,2);
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

                decimal? productoPrecioLista = Producto.precioLista * (Producto.moneda == 0/*Soles*/ ? 1 : (tipoCambio != null && tipoCambio <= 0) ? 1 : tipoCambio);

                //Hallo el resultado de los productos con descuento y sin descuento
                descuento = (prodConDesc * productoPrecioLista * (1 - desc));
                montoParcial = Decimal.Round((prodConDesc * productoPrecioLista * desc + prodSinDesc * productoPrecioLista).Value, 2);
                montoReal = cantidad * productoPrecioLista;
            }
        }


        public PromocionProducto PromocionActual { get; set; }

        public decimal tipoCambio { get; set; }
    }
}

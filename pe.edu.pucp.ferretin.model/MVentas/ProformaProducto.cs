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
            if (cantidad <= 0)
            {
                cantidad = 1;
                return;
            }

            if (cantidad > stockDisponible)
            {
                //("No se tiene stock de la cantidad proporcionada");
                cantidad = stockDisponible;
                return;
            }

            stockRestante = stockDisponible - cantidad;

            //Busco si tiene promocion, aplico el descuento
            if (PromocionProducto != null)
            {
                if (cantidad >= PromocionProducto.cantMulUnidades)
                {
                    descuentoPorcentaje = 1 / Decimal.Round(1 / PromocionProducto.descuentoPorcentaje.Value, 2);
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
                prodConDesc = ((PromocionProducto != null && cantidad >= PromocionProducto.cantMulUnidades) ? (cantidad / PromocionProducto.cantMulUnidades) * PromocionProducto.cantMulUnidades : 0);
                //Calcular Si se ha superamo el maximo permitido por venta
                prodConDesc = prodConDesc > 0 && prodConDesc > (PromocionProducto.maxPromVenta * PromocionProducto.cantMulUnidades) ? (PromocionProducto.maxPromVenta * PromocionProducto.cantMulUnidades) : prodConDesc;
                //Calculo la cantidad de productos sin descuento
                int? prodSinDesc = cantidad - prodConDesc;
                //Hallo el resultado de los productos con descuento y sin descuento

                decimal? productoPrecioLista = Producto.precioLista * (Producto.moneda == 0/*soles*/ ? 1 : (tipoCambio != null && tipoCambio <= 0) ? 1 : tipoCambio);

                descuento = Decimal.Round(false ? 0 : (prodConDesc * (Producto.moneda == 0/*soles*/ ? productoPrecioLista : Producto.precioLista) * (1 - desc)).Value, 2);
                montoParcial = false ? 0 : Decimal.Round((prodConDesc * productoPrecioLista * desc + prodSinDesc * productoPrecioLista).Value, 2);
                montoParcial = Decimal.Round(montoParcial.Value, 2);
                montoReal = cantidad * productoPrecioLista;
                montoReal = Decimal.Round(montoReal.Value, 2);
                precioPuntos = Producto.precioPuntos??0;
                precioPuntosParcial = (Producto.precioPuntos??0) * cantidad;
                puntosGanado = (puntosGanar ?? 0) * cantidad;
            }

            SendPropertyChanged("descuentoPrecioString");
            SendPropertyChanged("precioPuntosParcialString");
        }

        public string descuentoPrecioString
        {
            get
            {
                if (descuentoPorcentaje > 0)
                {
                    return (moneda == 0 ? "S/." : "$  ") + descuento.ToString() + " (" + Decimal.Round(descuentoPorcentaje.Value, 2).ToString() + "%)";
                }
                else
                {
                    if (this.PromocionProducto != null)
                    {
                        return this.PromocionProducto.cantMulUnidades.ToString() + "un. mín.";
                    }
                    else
                    {
                        return "-";
                    }
                }
            }
        }

        /// <summary>
        /// Cantidad de productos que tienen descuento
        /// </summary>
        public int? prodConDesc
        {
            get;
            set;
        }

        

        public decimal tipoCambio { get; set; }
    }
}

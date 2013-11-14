﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class VentaProducto : INotifyPropertyChanged
    {

        public string cantidadNombreProducto
        {
            get
            {
                return cantidad.ToString() + " X " + Producto.nombre.ToUpper() + " ";
            }
        }

        public string montoParcialString
        {
            get
            {
                return "S/. " + montoParcial.ToString();
            }
        }

        partial void OncantidadChanged()
        {
            //Cantidad no puede ser negativa
            if (cantidad <= 0) cantidad = 1;

            //Si supera el límite de puntos del usuario quito el canjeado
            if (canjeado.Value && this.Venta != null && this.Venta.Cliente != null && this.Venta.Cliente.puntosActual!=null && this.Venta.Cliente.puntosActual < (this.Venta.puntosCanjeados))
            {
                canjeado = false;
            }

            //Busco si tiene promocion, aplico el descuento
            if (PromocionActual != null)
            {
                if (cantidad >= PromocionActual.cantMulUnidades)
                {
                    descuentoPorcentaje = Decimal.Round(PromocionActual.descuento.Value,2);
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

                decimal? productoPrecioLista = Producto.precioLista * (Producto.moneda == 0/*soles*/ ? 1 : (tipoCambio != null && tipoCambio <= 0) ? 1 : tipoCambio);

                descuento = Decimal.Round(canjeado.Value ? 0 : (prodConDesc * productoPrecioLista * (1-desc) ).Value,2);
                montoParcial = canjeado.Value ? 0 : Decimal.Round((prodConDesc*productoPrecioLista*desc + prodSinDesc*productoPrecioLista).Value,2) ;
                montoParcial = Decimal.Round(montoParcial.Value);
                montoReal = cantidad * productoPrecioLista;
                montoReal = Decimal.Round(montoReal.Value, 2);
            }
        }

        partial void OncanjeadoChanged()
        {
            OncantidadChanged();
            if (this.Venta == null 
                || this.Producto == null
                || this.Producto.precioPuntos == 0
                || this.Venta.Cliente == null 
                || this.Venta.Cliente.puntosActual == null
                || this.Venta.Cliente.puntosActual < (this.Venta.puntosCanjeados)
                )
            {
                canjeado = false;
            }
        }


        public PromocionProducto PromocionActual { get; set; }

        public decimal tipoCambio { get; set; }
    }
}

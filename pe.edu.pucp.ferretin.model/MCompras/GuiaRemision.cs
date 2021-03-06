﻿using System.Linq;

namespace pe.edu.pucp.ferretin.model
{
    public partial class GuiaRemision
    {
        int i;
        decimal? aux1 = 0, aux2 = 0;
        public decimal? resumenCantidadTotal
        {      
            get
            {
                if (aux1 == 0)
                {
                    for (i = 0; i < this.GuiaRemisionProducto.Count(); i++)
                    {
                        aux1 = aux1 + this.GuiaRemisionProducto[i].DocumentoCompraProducto.cantidad;
                    }
                }                
                return aux1;
            }
        }

        public decimal? resumenCantidadRecibida
        {
            get
            {
                if (aux2 == 0)
                {
                    for (i = 0; i < this.GuiaRemisionProducto.Count(); i++)
                    {
                        aux2 = aux2 + this.GuiaRemisionProducto[i].cantidadRecibida;
                    }
                }             
                return aux2;
            }
        }
    }
}

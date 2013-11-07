using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                for (i = 0; i < this.GuiaRemisionProducto.Count(); i++)
                {
                    aux1 = aux1 + this.GuiaRemisionProducto[i].DocumentoCompraProducto.cantidad;
                }
                return aux1;
            }
        }

        public decimal? resumenCantidadRecibida
        {
            get
            {
                for (i = 0; i < this.GuiaRemisionProducto.Count(); i++)
                {
                    aux2 = aux2 + this.GuiaRemisionProducto[i].cantidadRecibida;
                }
                return aux2;
            }
        }
    }
}

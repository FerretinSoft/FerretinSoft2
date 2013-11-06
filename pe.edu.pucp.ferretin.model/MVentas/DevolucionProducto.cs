﻿using System;
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
            monto = cantidad * Producto.precioLista;
            if (Devolucion != null){
                Devolucion.subTotal = (from dp in Devolucion.DevolucionProducto select dp.monto).Sum();
            }
        }
    }
}

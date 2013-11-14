using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_ProductoAlmacenService : MA_ComunService
    {
        public static bool updateStock(Tienda tienda, Producto producto, decimal cantidad, bool plus)
        {
            ProductoAlmacen pa = ObtenerProductoAlmacenPorTiendaProducto(tienda, producto);
            if (plus) pa.stock += cantidad;
            else
            {
                if (pa.stock - cantidad < 0) return false; // stock insuficiente
                pa.stock -= cantidad;
            }
            return true;            
        }

        public static ProductoAlmacen ObtenerProductoAlmacenPorTiendaProducto(Tienda t, Producto p)
        {
            var pa = (from m in db.ProductoAlmacen
                                     where t != null && m.Tienda.Equals(t) && p != null && m.Producto.Equals(p)
                                     select m);

            return pa.Count() > 0 ? pa.First() : null;
        }        
    }
}

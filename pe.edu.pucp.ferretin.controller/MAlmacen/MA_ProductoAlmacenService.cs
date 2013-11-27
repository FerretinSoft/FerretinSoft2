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
        private static IEnumerable<ProductoAlmacen> _listaProductoAlmacen;
        public static IEnumerable<ProductoAlmacen> listaProductoAlmacen
        {
            get
            {
                if (_listaProductoAlmacen == null)
                {
                    _listaProductoAlmacen = db.ProductoAlmacen;
                }
                return _listaProductoAlmacen;
            }
            set
            {
                _listaProductoAlmacen = value;
            }
        }

        public static bool updateStock(Tienda tienda, Producto producto, decimal cantidad, bool plus)
        {
            ProductoAlmacen pa = ObtenerProductoAlmacenPorTiendaProducto(tienda, producto);
            if (pa == null) return false;
            if (plus) pa.stock += cantidad;
            else
            {
                if (pa.stock - cantidad < 0) return false; // stock insuficiente
                pa.stock -= cantidad;
            }
            return true;            
        }

        public static void inicializarProductosNuevaTIenda(Tienda t)
        {
            IEnumerable<Producto> listaProd = MA_ProductoService.obtenerTodosProductos();
            
            foreach (Producto p in listaProd)
            {
                ProductoAlmacen pa = new ProductoAlmacen();
                pa.id_almacen = t.id;
                pa.id_producto = p.id;
                pa.stock = 0;
                pa.stockMin = 0;
                pa.descuento = 0;
                pa.puntos=0;
                pa.estado = 1;
                db.ProductoAlmacen.InsertOnSubmit(pa);
            }

            db.SubmitChanges();
        }

        public static ProductoAlmacen ObtenerProductoAlmacenPorTiendaProducto(Tienda t, Producto p)
        {
            var pa = (from m in listaProductoAlmacen
                                     where t != null && m.Tienda.Equals(t) && p != null && m.Producto.Equals(p)
                                     select m);

            return pa.Count() > 0 ? pa.First() : null;
        }        
    }
}

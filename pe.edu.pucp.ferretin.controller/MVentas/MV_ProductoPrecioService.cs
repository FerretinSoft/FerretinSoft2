using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_ProductoPrecioService : MV_ComunService
    {
        #region Private Zone
        #endregion

        private static IEnumerable<Producto> _listaProductos;
        public static IEnumerable<Producto> listaproductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    _listaProductos = db.Producto;
                }
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
            }
        }

        private static IEnumerable<ProductoPrecio> _listaHistorialPrecios;
        public static IEnumerable<ProductoPrecio> listaHistorialPrecios
        {
            get
            {
                if (_listaHistorialPrecios == null)
                {
                    _listaHistorialPrecios = db.ProductoPrecio;
                }
                return _listaHistorialPrecios;
            }
            set
            {
                _listaHistorialPrecios = value;
            }
        }

        public static IEnumerable<Producto> buscarProductos(string nombreProducto)
        {
            return from c in listaproductos
                   where
                   (c.nombre.ToLower().Contains(nombreProducto.ToLower().Trim())
                  
                    )
                   orderby c.id
                   select c;
        }

        public static IEnumerable<ProductoPrecio> obtenerHistorialbyProd(Producto producto)
        {
            return from c in listaHistorialPrecios
                   where
                   (c.Producto != null && c.Producto.Equals(producto)
                    )
                   orderby c.id
                   select c;
        }

        public static ProductoPrecio obtenerPrecioActualbyProd(Producto producto)
        {
            var lista = (from c in listaHistorialPrecios
                   where
                   (c.Producto != null && c.Producto.Equals(producto)
                   && c.estado.Equals(true)
                    )
                   orderby c.id
                   select c);
            return lista.Count() > 0 ? lista.First() : null;
        }


        public static bool insertarPrecio(ProductoPrecio prodPrecio)
        {
            if (!db.ProductoPrecio.Contains(prodPrecio))
            {
                db.ProductoPrecio.InsertOnSubmit(prodPrecio);                
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        public static void actualizarPrecio(ProductoPrecio prodPrecio)
        {

            db.SubmitChanges();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_InventarioService
    {

        //todas las operaciones se basan en esta lista de producto
        private static IEnumerable<Producto> _listaProducto;
        public static IEnumerable<Producto> listaProducto
        {
            get
            {
                if (_listaProducto == null)
                {
                    _listaProducto = db.Producto;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProducto);
                return _listaProducto;
            }
            set
            {
                _listaProducto = value;
            }
        }


        //todas las opearciones se basan en esta lista de productoAlmacen
        private static IEnumerable<ProductoAlmacen> _listaProductoAlmacen;
        public static IEnumerable<ProductoAlmacen> listaProductoAlmacen
        {
            get
            {
                if (_listaProductoAlmacen == null)
                {
                    _listaProductoAlmacen = db.ProductoAlmacen;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos en almacen se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductoAlmacen);
                return _listaProductoAlmacen;
            }
            set
            {
                _listaProductoAlmacen = value;
            }
        }



        public static IEnumerable<Producto> obtenerProductosPorAlmacenCategoriaNombre(String nombre1, String searchTienda)
        {

            listaProducto =
                from p in db.Producto
                where
              (p.nombre != null && p.nombre.Contains(nombre1))

                orderby p.id
                select p;




            List<Producto> pList = listaProducto.ToList();

            foreach (Producto p in pList)
            {
                String cad = "";
                foreach (ProductoCategoria pc in p.ProductoCategoria)
                {
                    if (cad != "") cad += ", ";
                    cad += pc.Categoria.nombre;
                }
                p.cadenaCategoria = cad;


                foreach (ProductoAlmacen pa in p.ProductoAlmacen)
                {

                    if (pa.Tienda.nombre.Equals(searchTienda))
                    {
                        p.almacen = pa.Tienda.nombre;
                        p.stock = (int)pa.stock;
                        p.stockMinimo = (int)pa.stockMin;
                        p.unidadMedida = p.UnidadMedida.nombre;
                        p.materialBase1 = p.Material.nombre;
                        p.materialBase2 = p.Material.nombre;
                        p.precioLista = (int)p.precioLista;
                        p.descuento = (int)pa.descuento;
                        p.puntos = (int)pa.puntos;
                    }

                }
            }

            return listaProducto;
        }
    }
}

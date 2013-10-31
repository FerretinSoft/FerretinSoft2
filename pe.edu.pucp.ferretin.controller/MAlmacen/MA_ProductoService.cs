using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_ProductoService : MA_ComunService
    {
        
        private static IEnumerable<Producto> listaProductos = null;

        public static IEnumerable<Producto> obtenerProductosPorNombre(String search, bool chkActivo, bool chkInactivo, Int16 idcategoria)
        {
            int intActivo = chkActivo == true ? 1 : 0;
            int intInactivo = chkInactivo == true ? 0 : 1;

            if (idcategoria == 0)
            {
                listaProductos =
                    from p in db.Producto
                    where
                    p.nombre.Contains(search)
                    select p;
            }
            else
            {
                listaProductos =
                    from p in db.Producto
                    from pc in db.ProductoCategoria
                    where
                    p.nombre.Contains(search) &&
                    (pc.id_categoria == idcategoria) &&
                    pc.id_producto == p.id
                    select p;
            }


            List<Producto> pList = listaProductos.ToList();

            foreach (Producto p in pList)
            {
                String cad = "";
                foreach (ProductoCategoria pc in p.ProductoCategoria)
                {
                    if (cad != "") cad += ", ";
                    cad += pc.Categoria.nombre;
                }
                p.cadenaCategoria = cad;
            }

            return pList;
        }

        public static ProductoAlmacen obtenerProdxTienda(int idProd,int idTienda)
        {
            ProductoAlmacen t = (from pt in db.ProductoAlmacen
                        where pt.id_almacen == idTienda &&
                        pt.id_producto == idProd
                        select pt).SingleOrDefault();

            return t;
        }

        public static void agregarNuevoProducto(Producto prod)
        {
            db.Producto.InsertOnSubmit(prod);
            db.SubmitChanges();

        }


        public static void actualizarProducto()
        {
                db.SubmitChanges();
        }




    }
}

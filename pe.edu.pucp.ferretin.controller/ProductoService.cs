using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller
{
    public class ProductoService
    {
        private static FerretinDataContext db = new FerretinDataContext();
        private static IEnumerable<Producto> listaProductos = null;
        
        public static IEnumerable<Producto> obtenerTodosProductos()
        {
            listaProductos =
                from p in db.Producto
                select p;

            List<Producto> pList=listaProductos.ToList();

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

            return listaProductos;
        }

        public static IEnumerable<Producto> obtenerProductosPorNombre(Producto producto, int flagAll)
        {
            if (flagAll == 0)
            {
                listaProductos =
                    from p in db.Producto
                    where
                    p.nombre.Contains(producto.nombre) &&
                    p.estado == producto.estado
                    select p;
            }
            else
            {
                listaProductos =
                    from p in db.Producto
                    where
                    p.nombre.Contains(producto.nombre) &&
                    p.estado==true || p.estado==false
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

            return listaProductos;
        }

        public static void agregarProducto(Producto prod,ProductoAlmacen prodAlm)
        {
            db.Producto.InsertOnSubmit(prod);
            db.SubmitChanges();

            prodAlm.id_producto = prod.id;
            db.ProductoAlmacen.InsertOnSubmit(prodAlm);
            db.SubmitChanges();

        }




    }
}

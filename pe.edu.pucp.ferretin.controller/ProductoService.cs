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
        private static FerretinDataContextDataContext db=new FerretinDataContextDataContext();
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
                foreach (Producto_Categoria pc in p.Producto_Categoria)
                {
                    if (cad != "") cad += ", ";
                    cad += pc.Categoria.nombre;
                }
                p.cadenaCategoria = cad;
            }

            return listaProductos;
        }

        public static IEnumerable<Producto> obtenerProductosPorNombre(String searchNombre)
        {
            listaProductos=
                from p in db.Producto
                where (p.nombre.Contains(searchNombre))
                select p;

            List<Producto> pList = listaProductos.ToList();

            foreach (Producto p in pList)
            {
                String cad = "";
                foreach (Producto_Categoria pc in p.Producto_Categoria)
                {
                    if (cad != "") cad += ", ";
                    cad += pc.Categoria.nombre;
                }
                p.cadenaCategoria = cad;
            }

            return listaProductos;
        }


    }
}

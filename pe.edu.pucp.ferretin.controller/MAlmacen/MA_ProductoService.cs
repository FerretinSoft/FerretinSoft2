using pe.edu.pucp.ferretin.controller.MSeguridad;
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

        public static IEnumerable<Producto> obtenerTodosProductos()
        {
            IEnumerable<Producto> listaProd=from p in db.Producto
                                            select p;
            return listaProd;
        }

        public static void crearStockProductoAlmacen(int idProd)
        {
            IEnumerable<Tienda> listaTiendas = MS_TiendaService.listaTiendas;
            foreach (Tienda t in listaTiendas)
            {
                ProductoAlmacen pa = new ProductoAlmacen();
                pa.id_almacen = t.id;
                pa.id_producto = idProd;
                pa.stock = 0;
                pa.stockMin = 0;
                db.ProductoAlmacen.InsertOnSubmit(pa);
                db.SubmitChanges();
            }
        }

        public static void guardarStockProductoAlmacen()
        {
            db.SubmitChanges();
        }

        public static IEnumerable<ProductoAlmacen> obtenerStockProductoAlmacen(int idProd)
        {
            IEnumerable<ProductoAlmacen> pa = from dpa in db.ProductoAlmacen
                                              where (dpa.id_producto == idProd)
                                              select dpa;

            return pa;
        }

        public static IEnumerable<ProductoAlmacen> obtenerProductosPorTienda(int idTienda)
        {
            IEnumerable<ProductoAlmacen> listaProd = from pa in db.ProductoAlmacen
                                                  where (pa.id_almacen == idTienda) &&
                                                  (pa.estado==1)
                                                  select pa;
            return listaProd;
        }

        public static bool agregarColorProducto(ProductoColor pc)
        {
            if (!db.ProductoColor.Contains(pc))
            {
                db.ProductoColor.InsertOnSubmit(pc);
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public static Int16 obtenerIDProducto(String codProd)
        {
            int idProd = (from p in db.Producto
                           where p.codigo == codProd
                           select p.id).SingleOrDefault();

            return (Int16)idProd;

        }

        public static IEnumerable<Color> obtenerColoresPorProducto(int idProd)
        {
            IEnumerable<Color> listaColores = from c in db.Color
                                              from cp in db.ProductoColor
                                              where (cp.id_color == c.id) &&
                                              (cp.id_producto==idProd)
                                              select c;

            return listaColores;
        }

        public static IEnumerable<Color> obtenerTodosColores()
        {
            IEnumerable<Color> listaColores = from p in db.Color
                                              select p;
            return listaColores;

        }


        public static IEnumerable<Producto> obtenerProductosPorNombre(String search, bool chkActivo, bool chkInactivo, Categoria idCategoria)
        {
            int intActivo = chkActivo == true ? 1 : 0;
            int intInactivo = chkInactivo == true ? 0 : 1;
            if ((idCategoria ==null && search =="") || (idCategoria.nombre=="Todos" && search=="")) {
                listaProductos = from p in db.Producto
                                 orderby p.id
                                 select p;
                       
            }
            else if (idCategoria.id == 0)
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
                    (pc.id_categoria == idCategoria.id) &&
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



        public static bool agregarNuevoProducto(Producto prod)
        {
            if (obtenerProductoxCodigo(prod.codigo)==null)
            {
                db.Producto.InsertOnSubmit(prod);
                db.SubmitChanges();
                return true;
            }

            return false;

        }


        public static void actualizarProducto()
        {
                db.SubmitChanges();
        }




    }
}

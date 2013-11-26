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
        
        private static IEnumerable<Producto> _listaProductos;
        public static IEnumerable<Producto> listaProductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    _listaProductos = from p in db.Producto select p;
                }
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
            }
        }

        public static IEnumerable<Producto> obtenerTodosProductos()
        {
            return listaProductos;
        }

        public static String obtenerUltimoCodigo()
        {
            IEnumerable<Producto> listaProd = from p in listaProductos
                                              orderby p.codigo descending
                                              select p;

            return listaProd.ElementAt(0).codigo;
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
                pa.descuento = 0;
                pa.puntos = 0;
                pa.estado = 1;
                db.ProductoAlmacen.InsertOnSubmit(pa);
            }
            db.SubmitChanges();
        }

        public static void guardarStockProductoAlmacen()
        {
            db.SubmitChanges();
        }

        public static IEnumerable<ProductoAlmacen> obtenerStockProductoAlmacen(int idProd)
        {
            IEnumerable<ProductoAlmacen> pa = from dpa in MA_ProductoAlmacenService.listaProductoAlmacen
                                              where (dpa.id_producto == idProd)
                                              select dpa;

            return pa;
        }

        public static IEnumerable<ProductoAlmacen> obtenerProductosPorTienda(int idTienda)
        {
            IEnumerable<ProductoAlmacen> listaProd = from pa in MA_ProductoAlmacenService.listaProductoAlmacen
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


        public static IEnumerable<Producto> obtenerProductosPorNombre(String search, bool chkActivo, bool chkInactivo, Categoria idCategoria,String searchCod)
        {
            int intActivo = chkActivo == true ? 1 : 0;
            int intInactivo = chkInactivo == true ? 0 : 1;

            IEnumerable<Producto> result;

            if (searchCod != "") //se da prioridad a búsqueda por codigo
            {
                result = from p in listaProductos
                                 where p.codigo==searchCod
                                 select p;

            }
            else
            {


                if (idCategoria == null)
                {
                    idCategoria = new Categoria();
                    idCategoria.id = 0;
                    idCategoria.nombre = "Todos";
                }


                if ((idCategoria.nombre == "Todos" && search == ""))
                {
                    //if ((idCategoria ==null && search =="")) {
                    result = from p in listaProductos
                                     orderby p.id
                                     select p;

                }
                else if (idCategoria.id == 0)
                {
                    result =
                        from p in listaProductos
                        where
                        p.nombre.Contains(search)
                        select p;
                }
                else
                {
                    result =
                        from p in listaProductos
                        from pc in db.ProductoCategoria
                        where
                        p.nombre.Contains(search) &&
                        (pc.id_categoria == idCategoria.id) &&
                        pc.id_producto == p.id
                        select p;
                }
            }


            //List<Producto> pList = listaProductos.ToList();

            //foreach (Producto p in pList)
            //{
            //    String cad = "";
            //    foreach (ProductoCategoria pc in p.ProductoCategoria)
            //    {
            //        if (cad != "") cad += ", ";
            //        cad += pc.Categoria.nombre;
            //    }
            //    p.cadenaCategoria = cad;
            //}

            //return pList;
            return result;
        }

        public static ProductoAlmacen obtenerProdxTienda(int idProd,int idTienda)
        {
            ProductoAlmacen t = (from pt in MA_ProductoAlmacenService.listaProductoAlmacen
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


        public static void actualizarProducto(IEnumerable <Categoria> listaCategoriasMod,Producto prod)
        {
            //Comparamos categorias antes y despues
            List<ProductoCategoria> tpc = (MA_CategoriaService.obtenerTablaCategoriaProducto  
                                    (prod.id)).ToList<ProductoCategoria>();

            foreach (Categoria c in listaCategoriasMod)
            {
                ProductoCategoria pc = new ProductoCategoria();
                pc.id_producto = prod.id;
                pc.id_categoria = c.id;

                if (c.isChecked)
                {
                    //Si no existe, lo agregamos
                    if (tpc.Where(t => (t.id_categoria == pc.id_categoria) && (t.id_producto == pc.id_producto)).Count() == 0)
                    {
                        //tpc.Add(pc);
                        db.ProductoCategoria.InsertOnSubmit(pc);
                    }
                }
                else
                {  
                    //Si existe, pero esta deseleccionado entonces lo eliminamos
                    if (tpc.Where(t=> (t.id_categoria==pc.id_categoria) && (t.id_producto==pc.id_producto)).Count()==1)
                    {
                        //tpc.Remove(pc);
                        ProductoCategoria pctemp = tpc.Single(t => (t.id_categoria == pc.id_categoria) &&
                                                    (t.id_producto == pc.id_producto));
                        db.ProductoCategoria.DeleteOnSubmit(pctemp);
                    }
                }

                //Para cada subproducto

                foreach (Categoria c2 in c.Categoria2)
                {
                    ProductoCategoria subpc = new ProductoCategoria();
                    subpc.id_producto = prod.id;
                    subpc.id_categoria = c2.id;

                    if (c2.isChecked)
                    {
                        //Si no existe, lo agregamos
                        if (tpc.Where(t => (t.id_categoria == subpc.id_categoria) &&
                                (t.id_producto == subpc.id_producto)).Count() == 0)
                        {
                            //tpc.Add(pc);
                            db.ProductoCategoria.InsertOnSubmit(subpc);
                        }
                    }
                    else
                    {
                        //Si existe, pero esta deseleccionado entonces lo eliminamos
                        if (tpc.Where(t => (t.id_categoria == subpc.id_categoria) &&
                                (t.id_producto == subpc.id_producto)).Count() == 1)
                        {
                            //tpc.Remove(pc);
                            ProductoCategoria pctemp = tpc.Single(t => (t.id_categoria == subpc.id_categoria) &&
                                                        (t.id_producto == subpc.id_producto));
                            db.ProductoCategoria.DeleteOnSubmit(pctemp);
                        }
                    }
                }
            }
            db.SubmitChanges();
        }




    }
}

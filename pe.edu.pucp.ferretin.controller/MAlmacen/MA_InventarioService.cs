using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_InventarioService: MA_ComunService
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


        public static Producto obtenerProductoNombre(String nombre)
        {
            Producto producto = (from c in listaProducto
                                 where c.nombre != null && c.nombre.Equals(nombre)
                               select c).First();
            return producto;
        }

        private static IEnumerable<Tienda> _listaAlmacen;
        public static IEnumerable<Tienda> listaAlmacen
        {
            get
            {
                if (_listaAlmacen == null)
                {
                    _listaAlmacen = db.Tienda;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaAlmacen);
                return _listaAlmacen;
            }
            set
            {
                _listaAlmacen = value;
            }
        }

       private static IEnumerable<Categoria> _listaCategoria;
        public static IEnumerable<Categoria> listaCategoria
        {
            get
            {
                if (_listaCategoria == null)
                {
                    _listaCategoria = db.Categoria;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaCategoria);
                return _listaCategoria;
            }
            set
            {
                _listaCategoria = value;
            }
        }


        private static IEnumerable<Material> _listaMaterial;
        public static IEnumerable<Material> listaMaterial
        {
            get
            {
                if (_listaMaterial == null)
                {
                    _listaMaterial = db.Material;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaMaterial);
                return _listaMaterial;
            }
            set
            {
                _listaMaterial = value;
            }
        }

        private static IEnumerable<UnidadMedida> _listaUnidadMedida;
        public static IEnumerable<UnidadMedida> listaUnidadMedida
        {
            get
            {
                if (_listaUnidadMedida == null)
                {
                    _listaUnidadMedida = db.UnidadMedida;
                }
                //Usando concurrencia pesimista:
                ///La lista de productos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaUnidadMedida);
                return _listaUnidadMedida;
            }
            set
            {
                _listaUnidadMedida = value;
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



        public static IEnumerable<ProductoAlmacen> obtenerProductosPorAlmacenCategoriaNombre(String nombre1, Tienda searchAlmacen, Categoria searchCategoria)
        {
            IEnumerable<ProductoAlmacen> listaProdAlm=null;
            //Caso: Iniciando pantalla o con Almacen=Todos,Categoria=Todos y nombre=vacío
            if ((nombre1 == "") && ((searchAlmacen == null) || (searchAlmacen.nombre=="Todos")) && ((searchCategoria == null) || (searchCategoria.nombre=="Todos")))
            {

                listaProdAlm = from pa in db.ProductoAlmacen
                                select pa;

            }
            else
            {
                //Caso: Almacen y Categoria seleccionados
                if ((searchAlmacen.nombre != "Todos") && (searchCategoria.nombre!="Todos"))
                {
                    listaProdAlm = from pa in db.ProductoAlmacen
                                from p in db.Producto
                                from pc in db.ProductoCategoria
                                where (p.nombre.Contains(nombre1)) &&
                                (pa.id_producto == p.id) &&
                                (pa.id_almacen == searchAlmacen.id) &&
                                (pc.id_categoria == searchCategoria.id) &&
                                (pc.id_producto == p.id)
                                select pa;
                }
                else
                {
                    //Caso: Almacén seleccionado
                    if (searchAlmacen.nombre!="Todos")
                    {
                        listaProdAlm = from pa in db.ProductoAlmacen
                                        from p in db.Producto
                                        where (p.nombre.Contains(nombre1)) &&
                                        (pa.id_producto == p.id) &&
                                        (pa.id_almacen == searchAlmacen.id)
                                        select pa;

                    }
                    else
                    {
                        //Caso: Categoría seleccionada
                        if (searchCategoria.nombre != "Todos")
                        {
                            listaProducto = from p in db.Producto
                                            from pc in db.ProductoCategoria
                                            from pa in db.ProductoAlmacen //+
                                            where (p.nombre.Contains(nombre1)) &&
                                            (pc.id_categoria == searchCategoria.id) &&
                                            (pc.id_producto == p.id) &&
                                            (pa.id_producto==p.id) //*
                                            select p;

                        }
                        else//Búsqueda solo por nombre
                        {
                            listaProdAlm = from p in db.Producto
                                            from pa in db.ProductoAlmacen //*
                                            where (p.nombre.Contains(nombre1)) &&
                                            (p.id==pa.id_producto)
                                            select pa;
                        }
                    }
                }
            }

            //if (searchAlmacen != null && searchAlmacen.nombre != "Todos")
            //{
            //    listaAlmacen =
            //        from t in db.Tienda
            //        where
            //        (t.nombre != null && t.id.Equals(searchAlmacen.id))
            //        orderby t.id
            //        select t;
            //}


            //if (searchCategoria != null && searchCategoria.nombre != "Todos")
            //{
            //    listaCategoria =
            //        from c in db.Categoria
            //        where
            //        (c.nombre != null && c.id.Equals(searchCategoria.id))
            //        orderby c.id
            //        select c;
            //}
            
            //List<Producto> pList = listaProducto.ToList();
            //List<Tienda> pListA = listaAlmacen.ToList();
            //List<Categoria> pListC = listaCategoria.ToList();
            
            //foreach (Producto p in pList)
            //{
            //    String cad = "";
            //    foreach (ProductoCategoria pc in p.ProductoCategoria)
            //    {
            //        if (cad != "") cad += ", ";
            //        cad += pc.Categoria.nombre;
            //    }
            //    p.cadenaCategoria = cad;


            //    foreach (ProductoAlmacen pa in p.ProductoAlmacen)
            //    {
            //            //p.almacen = pa.Tienda.nombre;
            //            p.stock = (int)pa.stock;
            //            p.stockMinimo = (int)pa.stockMin;
            //            p.unidadMedida = p.UnidadMedida.nombre;
            //            //p.materialBase1 = p.Material.nombre;
            //            ///p.materialBase2 = p.Material.nombre;
            //            p.precioLista = (int)p.precioLista;
            //            p.descuento = (int)pa.descuento;
            //            p.puntos = (int)pa.puntos;
                    
            //            foreach (Tienda ti in pListA)
            //            {

            //                if (pa.id_almacen.Equals(ti.id))
            //                {
            //                    p.almacen=ti.nombre;
            //                }

            //            }
            //    }

            //    foreach (UnidadMedida ume in listaUnidadMedida) 
            //    {
            //        if (p.id_unidad_medida.Equals(ume.id)) {
            //            p.unidadMedida = ume.nombre;
            //        }
            //    }
                    
            //    foreach (Material ma in listaMaterial)
            //    {
            //        if (p.id_material_base.Equals(ma.id)) {
            //            p.materialBase1 = ma.nombre;
            //            p.materialBase2 = ma.nombre;
            //        }
            //    }
            //}
            
            return listaProdAlm;
        }
    }
}

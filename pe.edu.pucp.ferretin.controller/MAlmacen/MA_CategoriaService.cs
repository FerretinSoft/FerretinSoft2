using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_CategoriaService : MA_ComunService
    {
        
        private static IEnumerable<Categoria> _listaCategoria=null;
        private static IEnumerable<ProductoCategoria> _listaProductoCategoria = null;

        public static IEnumerable<Categoria> categorias
        {
            get
            {
                if (_listaCategoria == null) _listaCategoria = db.Categoria;

                return _listaCategoria;
            }
        }

        public static void agregarCategoriaProductos(IEnumerable<ProductoCategoria> pc)
        {
            foreach (ProductoCategoria prodcat in pc)
                    db.ProductoCategoria.InsertOnSubmit(prodcat);

        }


        public static void actualizarCategoriasProductos(IEnumerable<Categoria> listaCategoriaProducto)
        {



            ProductoCategoria pc;

            foreach (Categoria c in listaCategoriaProducto)
            {
                pc = new ProductoCategoria();




            }



        }

        public static IEnumerable<Categoria> obtenerCategoriasPadres()
        {
            _listaCategoria = from c in db.Categoria
                              where c.id_padre==null
                              select c;
            return _listaCategoria;
        }


        public static IEnumerable<ProductoCategoria> obtenerTablaCategoriaProducto(int idProd)
        {
            IEnumerable<ProductoCategoria> prodCat = from c in db.Categoria
                                                from pc in db.ProductoCategoria
                                                where (pc.id_producto == idProd) &&
                                                (c.id == pc.id_categoria)
                                                select pc;
            return prodCat;

        }

        public static IEnumerable<Categoria> obtenerCategoriasxProducto(int idProd)
        {
            _listaCategoria = from c in db.Categoria
                              from pc in db.ProductoCategoria
                              where (pc.id_producto==idProd) &&
                              (c.id==pc.id_categoria)
                              select c;
            return _listaCategoria;
        }


        public static int eliminarCategoria(Categoria categoria) 
        {
            //verificamos que la Categoria no este en uso
            _listaCategoria = from cat in db.Categoria
                                  where cat.id.Equals(categoria.id)
                                  select cat;

            _listaProductoCategoria = from catProducto in db.ProductoCategoria
                            where catProducto.id_categoria.Equals(categoria.id)
                            select catProducto;

            if (_listaProductoCategoria == null)
            {
                foreach (var cat in _listaCategoria)
                {
                    db.Categoria.DeleteOnSubmit(categoria);
                }
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return 1;
            }
            else {
                return 0;
            }
        }



        public static bool insertarCategoria(Categoria categoria)
        {
            if (!db.Categoria.Contains(categoria))
            {
                db.Categoria.InsertOnSubmit(categoria);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }


        public static Categoria categoriaPadre
        {
            get
            {
                try
                {
                    return db.Categoria.Single(m => m.id_padre == null || m.id_padre <= 0);
                }
                catch (Exception){
                    return null;
                }
            }
        }    

    }
}

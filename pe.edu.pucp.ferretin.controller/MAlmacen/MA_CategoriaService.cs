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

        public static IEnumerable<Categoria> categorias
        {
            get
            {
                if (_listaCategoria == null) _listaCategoria = db.Categoria;

                return _listaCategoria;
            }
        }


        public static IEnumerable<Categoria> obtenerCategoriasPadres()
        {
            _listaCategoria = from c in db.Categoria
                              where c.id_padre==null
                              select c;
            return _listaCategoria;
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

    }
}

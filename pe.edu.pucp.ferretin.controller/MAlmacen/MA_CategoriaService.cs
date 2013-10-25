using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_CategoriaService : MA_ComunService
    {
        
        private static IEnumerable<Categoria> listaCategoria=null;

        public static IEnumerable<Categoria> obtenerTodasCategorias()
        {
            listaCategoria =
                from c in db.Categoria
                select c;

            List<Categoria> listCat = listaCategoria.ToList();
            return listCat;
        }    
    }
}

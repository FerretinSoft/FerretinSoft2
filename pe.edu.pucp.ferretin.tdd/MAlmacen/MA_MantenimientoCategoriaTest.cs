using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;

namespace pe.edu.pucp.ferretin.tdd.MAlmacen
{
    class MA_MantenimientoCategoriaTest
    {

        /***********Test nombre unico de categoria ************************/

        [TestCase]
        public void Nombre_De_Categoria_Unico()
        {
            //var creo el entorno

            foreach (Categoria b in MA_CategoriaService.db.Categoria){
                IEnumerable<Categoria> cat = MA_CategoriaService.db.Categoria.Where(p => p.nombre==b.nombre);
                //Assert - verificar condicion o criterio de aceptacion
                int cantidadRegistros = cat.Count();
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }
        
        /****************Cantidad minima de categoria por tienda **********************************************/

        [TestCase]
        public void Cantidad_minima_Categoria()
        {
            //var  creo el entorno

            int cantidadCategoria = MA_CategoriaService.db.Categoria.Count();
            //Assert - verificar condicion o criterio de aceptacion
            Assert.GreaterOrEqual(cantidadCategoria, 5);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using System.Threading.Tasks;

using NUnit.Framework;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MSeguridad;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;

namespace pe.edu.pucp.ferretin.tdd.MSeguridad
{
    [TestFixture]
    public class MS_AdministrarTiendasTest
    {
        /******************** Test Tienda nulo *************************/
        [TestCase]
        public void tiendas_no_nulos()
        {            
            IEnumerable<Tienda> tiend = MS_TiendaService.db.Tienda.Where(tie => tie.nombre == null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (tiend == null) cantidadRegistros = 0; // significa que no hay tiendas con numero de codumento o nombre nulos, o estan los 2 o ninguno
            else cantidadRegistros = tiend.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);            
        }
        /******************* Test Nombre de Tienda *************************/
        [TestCase]
        public void cantidad_de_tiendas_igual_a_16()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantTiendas = MS_TiendaService.db.Tienda.Count();
            
            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(16, cantTiendas);
        }
    }
}

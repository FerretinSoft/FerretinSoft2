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
    public class MS_AdministrarPerfilesTest
    {
        /******************** Test Perfiles nulo *************************/
        [TestCase]
        public void perfiles_no_nulos()
        {
            IEnumerable<Perfil> perfi = MS_PerfilService.db.Perfil.Where(perf => perf.nombre == null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (perfi == null) cantidadRegistros = 0; // significa que no hay perfiles con nombre nulo
            else cantidadRegistros = perfi.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);
        }
        /******************** Test Cantidad de Usuarios *************************/
        [TestCase]
        public void cantidad_de_perfiles_igual_a_12()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantPerfiles = MS_PerfilService.db.Perfil.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(12, cantPerfiles);
        }
    }
}

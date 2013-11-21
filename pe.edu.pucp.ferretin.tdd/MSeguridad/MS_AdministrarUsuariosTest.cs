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
    public class MS_AdministrarUsuariosTest
    {
        /******************** Test Cliente nulo *************************/
        [TestCase]
        public void usuarios_no_nulos()
        {          
            IEnumerable<Usuario> usuar = MS_UsuarioService.db.Usuario.Where(usu => usu.nombre == null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (usuar == null) cantidadRegistros = 0; // significa que no hay usuarios con numero de codumento o nombre nulos, o estan los 2 o ninguno
            else cantidadRegistros = usuar.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);            
        }
        /******************** Test Cantidad de Usuarios *************************/
        [TestCase]
        public void cantidad_de_usuarios_igual_a_16()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantUsuarios = MS_UsuarioService.db.Usuario.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(16, cantUsuarios);
        } 
    }
}

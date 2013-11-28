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
        /******************** Test Usuario nulo *************************/
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

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantUsuarios = MS_UsuarioService.db.Usuario.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(16, cantUsuarios);
        }
        /******************** Test Usuario unico con nombre *************************/
        [TestCase]
        public void nombre_de_usuarios_unico()
        {
            //var creo el entorno

            foreach (Usuario u in MS_UsuarioService.db.Usuario)
            {
                IEnumerable<Usuario> listUser = MS_UsuarioService.db.Usuario.Where(us => us.nombre.Contains(u.nombre));

                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantidadRegistros;
                if (listUser == null) cantidadRegistros = 0;
                else cantidadRegistros = listUser.Count();

                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(1, cantidadRegistros);
            }
        }
        /******************** Test Usuario con contraseña actualizada, no por defecto *************************/
        [TestCase]
        public void contrasena_usuarios_actualizada_por_primera_vez()
        {
            //var creo el entorno                                   
            IEnumerable<Usuario> listUser = MS_UsuarioService.db.Usuario.Where(us => us.nombre.Equals("FWFg8CrX1VhB4uXMR/AFmQ=="));

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (listUser == null) cantidadRegistros = 0;
            else cantidadRegistros = listUser.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);            
        }
        /******************** Test Usuario con intentos permitidos *************************/
        [TestCase]
        public void peridod_de_clave_valida_menor_a_lo_permitido()
        {
            //var creo el entorno           
            DateTime dt = DateTime.Now;
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("clave"));

            //se obtiene la cantidad de dias totales entre 2 fechas dadas y si son mayores al periodo de clave permitida sin cambiar - 10 dias
            IEnumerable<Usuario> listUser = MS_UsuarioService.db.Usuario.Where(us => ((dt.Year-us.ultimoCambioContrasena.Value.Year)*365 + 
                                                                                        (dt.Month-us.ultimoCambioContrasena.Value.Month)*30 +
                                                                                            (dt.Day-us.ultimoCambioContrasena.Value.Day))
                                                                                                > Convert.ToInt32(par.valor));

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (listUser == null) cantidadRegistros = 0;
            else cantidadRegistros = listUser.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);
        }

        /******************** Test Usuario con intentos permitidos *************************/
        [TestCase]
        public void intentos_contrasena_usuarios_mayor_a_cero()
        {
            //var creo el entorno           
            DateTime dt = DateTime.Now;
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("clave"));

            //se obtiene la cantidad de dias totales entre 2 fechas dadas y si son mayores al periodo de clave permitida sin cambiar - 10 dias
            IEnumerable<Usuario> listUser = MS_UsuarioService.db.Usuario.Where(us => us.intentosCon==0);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (listUser == null) cantidadRegistros = 0;
            else cantidadRegistros = listUser.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);
        }

    }
}

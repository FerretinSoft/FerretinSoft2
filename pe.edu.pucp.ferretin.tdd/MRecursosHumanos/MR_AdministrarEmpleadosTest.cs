using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using System.Threading.Tasks;

using NUnit.Framework;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;

namespace pe.edu.pucp.ferretin.tdd.MRecursosHumanos
{ 
    [TestFixture]
    public class MR_AdministrarEmpleadosTest
    {
        /******************** Test Empleados nulo *************************/
        [TestCase]
        public void empleados_no_nulos()
        {
            IEnumerable<Empleado> emplea = MR_EmpleadoService.db.Empleado.Where(emp => emp.dni==null || emp.nombre == null 
                                                                                     || emp.apMaterno==null || emp.apMaterno==null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (emplea == null) cantidadRegistros = 0; // significa que no hay empleados con dni, nombre, apellido paterno y materno nulo
            else cantidadRegistros = emplea.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);
        }
        /******************** Test Cantidad de Usuarios *************************/
        [TestCase]
        public void cantidad_de_empleados_igual_a_22()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantEmpleados = MR_EmpleadoService.db.Empleado.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.GreaterOrEqual(22, cantEmpleados);
        }
        
        /*************************Prueba si un empleado no ha sido registrado dos veces**************************************/

        [TestCase]
        public void Empleado_Unico_En_Registro()
        {
            foreach (Empleado e in MR_EmpleadoService.db.Empleado) {

                IEnumerable<Empleado> lemp = MR_EmpleadoService.db.Empleado.Where(em => em.nombre == e.nombre && em.apPaterno == e.apPaterno && em.apMaterno == e.apMaterno);

                int cantidadRegistros = (int)lemp.Count();

                //Assert- verificar que el empleado no ha sido registrdo dos veces en la Base de datos
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }

    }
}

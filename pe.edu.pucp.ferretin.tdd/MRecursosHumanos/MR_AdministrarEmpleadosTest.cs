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
        /******************** Test Cantidad de Usuarios *************************/
        [TestCase]
        public void cantidad_de_empleados_igual_a_22()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantEmpleados = MR_EmpleadoService.db.Empleado.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(22, cantEmpleados);
        }
    }
}

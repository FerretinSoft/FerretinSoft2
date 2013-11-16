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
        /******************* Test Nombre de Tienda *************************/
        [TestCase]
        public void nuevo_usuario_creado()
        {
            //Arrange = Creo el entorno            
            var ventana = new MS_AdministrarTiendasViewModel();
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            
            
            //Assert - Verificar la condicion o criterio de aceptacion
            
        }


    }

}

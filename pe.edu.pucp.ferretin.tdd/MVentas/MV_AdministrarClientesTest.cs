using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using NUnit.Framework;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MVentas;

namespace pe.edu.pucp.ferretin.tdd.MVentas
{
    [TestFixture]
    public class MV_AdministrarClientesTest
    {
        /******************** Test Cantidad de Clientes *************************/
        [TestCase]
        public void cantidad_de_clientes_igual_a_6()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantClientes = MV_VentaService.db.Cliente.Count();

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(6, cantClientes);
        }

        /******************** Test Cliente unico con nroDocumento *************************/
        [TestCase]
        public void nroDocumento_de_cliente_unico()
        {
            //var creo el entorno

            foreach (Cliente c in MV_ClienteService.db.Cliente)
            {
                IEnumerable<Cliente> client = MV_ClienteService.db.Cliente.Where(cli => cli.nroDoc == c.nroDoc);
                                
                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantidadRegistros = client.Count();
                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }
        /******************** Test Cliente unico con nombre *************************/
        [TestCase]
        public void nombre_de_cliente_unico()
        {
            //var creo el entorno

            foreach (Cliente c in MV_ClienteService.db.Cliente)
            {
                IEnumerable<Cliente> client = MV_ClienteService.db.Cliente.Where(cli => cli.nombre == c.nombre);

                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantidadRegistros = client.Count();
                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(cantidadRegistros, 1);
            }
        }
        /******************** Test Puntos Ganados contra Puntos Usados *************************/
        [TestCase]
        public void puntos_ganados_menos_puntos_usados_igual_puntos_disponibles()
        {
     
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            foreach (Cliente c in MV_ClienteService.db.Cliente)
            {
                int puntDisp = (int)c.puntosActual;
                int puntUsa = (int)c.puntosUsados;
                int puntGan = (int)c.puntosGanados;
                
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.AreEqual(puntGan-puntUsa, puntDisp);                
            }
        }



    }
}

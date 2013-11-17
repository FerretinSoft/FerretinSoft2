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
    public class MS_ParametrosTest
    {
        /******************** Test numero de intentos *************************/
        [TestCase]
        public void numero_intentos_maximo_igual_a_5_intentos()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("intentos"));
            int valorNMaxIntentos = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(5, valorNMaxIntentos);
        }
        /******************** Test tiempo maximo de sesion *************************/
        [TestCase]
        public void tiempo_maximo_de_sesion_activa_igual_30_minutos()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("sesion"));
            int valorTMaxSesion = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.GreaterOrEqual(30, valorTMaxSesion);
        }
        /******************** Test tiempo maximo de duracion de clave *************************/
        [TestCase]
        public void tiempo_maximo_de_duracion_de_clave_igual_10_dias()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("clave"));
            int valorTMaxClave = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.GreaterOrEqual(10, valorTMaxClave);
        }
        /******************** Test tipo de cambio *************************/
        [TestCase]
        public void tipo_de_cambio_igual_2_83_soles()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("cambio"));
            string valorTCambio = par.valor;

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual("2.83", valorTCambio);
        }
        /******************** Test IGV *************************/
        [TestCase]
        public void valor_IGV_igual_18_porCiento()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("igv"));
            int valorIGV = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(18, valorIGV);
        }
        /******************** Test vigencia proforma *************************/
        [TestCase]
        public void vigencia_de_proforma_igual_10_dias()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("proforma"));
            int valorVProforma = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(10, valorVProforma);
        }
        /******************** Test vigencia notas de credito *************************/
        [TestCase]
        public void vigencia_de_notas_de_credito_igual_10_dias()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("credito"));
            int valorNCredito = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(10, valorNCredito);
        }
        /******************** Test cantidad de soles por punto *************************/
        [TestCase]
        public void cantidad_de_soles_por_punto_igual_10_soles()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("punto"));
            int valorSPunto = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(10, valorSPunto);
        }
        /******************** Test vigencia vales *************************/
        [TestCase]
        public void vigencia_de_vales_igual_20_dias()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("vale"));
            int valorVVale = Convert.ToInt32(par.valor);

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.AreEqual(20, valorVVale);
        }

    }

}

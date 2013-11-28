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
    class MA_SolicitudAbastecimientoTest
    {

        /***********************Estado de solicitud de abastecimiento Unico**********************/
        [TestCase]
        public void Estado_SolicitudAbastecimiento_Unico()
        {
            foreach (SolicitudAbastecimientoEstado sae in MA_SolicitudAbastecimientoService.db.SolicitudAbastecimientoEstado) 
            {
                IEnumerable<SolicitudAbastecimientoEstado> LSoliEst = MA_SolicitudAbastecimientoService.db.SolicitudAbastecimientoEstado.Where(s => s.nombre == sae.nombre);

                int cantidad = LSoliEst.Count();
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.AreEqual(cantidad, 1);


            }
        }


        /********************Socilitud de abastecimiento de productos campos obligatorios**********************************************************************/
        [TestCase]
        public void SolicitudAbastecimiento_Producto_CamposObligatorios() 
        {
            foreach (SolicitudAbastecimientoProducto sap in MA_SolicitudAbastecimientoService.db.SolicitudAbastecimientoProducto)
            { 
                //Assert para verificar que las solicitudes estan con sus campos obligatorios
                Assert.IsNotNull(sap.id);
                Assert.IsNotNull(sap.id_producto);
                Assert.IsNotNull(sap.cantidad);
                /****************************************************/
                //Assert.IsNotNull(sap.id_solicitud_abastecimiento);
                //Assert.IsNotNull(sap.cantidadAtendida);
                //Assert.IsNotNull(sap.cantidadRestante);
            }
        }

        /// <summary>
        /// Probar que para toda solicitud de abastecimiento la cantidad total sea igual a 
        /// la cantidad atendida más la cantidad restante
        /// </summary>
        [TestCase]
        public void VerificarCantidades()
        {
            foreach (SolicitudAbastecimientoProducto sap in MA_SolicitudAbastecimientoService.db.SolicitudAbastecimientoProducto)
            {
                if (sap.id_solicitud_abastecimiento > 0)
                    Assert.AreEqual(sap.cantidad, sap.cantidadAtendida + sap.cantidadRestante);
            }
        }

        
    }
}

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
    public class MV_AdministrarVentasTest
    {
        /**** Test Cantidad de venta: subtotal + igv = total ************/
        [TestCase]
        public void cantidad_de_venta_subtotal_mas_igv_igual_total()
        {
            //var creo el entorno
            foreach (Venta v in MV_VentaService.db.Venta)
            {
                double subTotMonto = (double)v.subTotal;
                double igvMonto = (double)v.igv;
                double totalMonto = (double)v.total;

                double valor = Math.Abs(totalMonto-(igvMonto+subTotMonto));

                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.GreaterOrEqual(valor,0.00);
                Assert.LessOrEqual(valor,0.01);
            }
        }

    }
}

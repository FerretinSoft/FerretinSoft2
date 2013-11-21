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
        /******************** Test Venta nulo *************************/
        [TestCase]
        public void ventas_no_nulos()
        {
            IEnumerable<Venta> vent = MV_VentaService.db.Venta.Where(cli => cli.nroDocumento == null || cli.id_usuario == null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma
            int cantidadRegistros;
            if (vent == null) cantidadRegistros = 0; // significa que no hay ventas con numero de documento o usuario nulos, o estan los 2 o ninguno
            else cantidadRegistros = vent.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantidadRegistros);
        }
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
        /**** Test Puntos obtenidos por venta ************/
        [TestCase]
        public void puntos_ganados_por_venta_igual_cantidadProducto_por_puntosProducto()
        {
            //var creo el entorno
            foreach (Venta v in MV_VentaService.db.Venta)
            {
                int puntGan = (int)v.puntosGanados;
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            
                VentaProducto ventProd = MV_VentaService.db.VentaProducto.Single(vp => vp.id_venta==v.id);
                int cantProd = (int)ventProd.cantidad;
                Producto prod = MV_VentaService.db.Producto.Single(p => p.id==ventProd.id_producto);
                int ganPunt = (int)prod.ganarPuntos;
                
                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.AreEqual(puntGan,cantProd*ganPunt);                
            }
        }
    }
}

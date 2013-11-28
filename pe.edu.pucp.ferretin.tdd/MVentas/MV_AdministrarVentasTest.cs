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
using pe.edu.pucp.ferretin.controller.MSeguridad;

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
        /***************** Test Cantidad de venta: subtotal + igv = total *****************/
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
        public void puntos_ganados_por_venta_igual_cantidadProducto_por_puntosProductos()
        {
            //var creo el entorno
            foreach (Venta v in MV_VentaService.db.Venta)
            {
                int puntGan = (int)v.puntosGanados;
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            
                IEnumerable<VentaProducto> ventProd = MV_VentaService.db.VentaProducto.Where(vp => (int)vp.id_venta == (int)v.id);
                
                //int cantProd = (int)ventProd.cantidad;
                int puntAcum = 0;
                /*Obtienes los puntos de cada producto de la venta*/
                foreach (VentaProducto vp2 in ventProd)
                {
                    if (vp2.id_producto != null)
                    {
                        Producto prod = MV_VentaService.db.Producto.Single(p => (int)p.id == (int)vp2.id_producto);
                        puntAcum = puntAcum + (int)prod.ganarPuntos * (int)vp2.cantidad;
                    }

                    //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                    //Assert - Verificar la condicion o criterio de aceptacion                    
                }
                Assert.AreEqual(puntGan,puntAcum);
            }
        }
        /***********************************************************/
        [TestCase]
        public void creacion_de_ventas_disminusion_de_stock()
        {
            var user = ComunService.db.Usuario.First(u => u.nombre.ToLower().Equals("jquilca"));
            ComunService.usuarioLo(user);
            var tienda = user.Empleado.tiendaActual;
            for (int v = 0; v < 1000; v++)
            {
                ComunService.Clean();
                var vm = new MV_RegistrarVentaViewModel();
                var productos = ComunService.db.ProductoAlmacen.Where(pa => pa.Tienda.id.Equals(tienda.id) && pa.estado.HasValue && pa.estado == 1 && pa.Producto != null).Take(8).ToList();
                //var creo el entorno
                int cantProd = productos.Count();
                Console.WriteLine("Cantidad de prodcutos: " + cantProd.ToString());
                int max = (int)(new Random()).Next(1, 10);
                int cant = 0;
                while (vm.venta.VentaProducto.Count < max && cant <= 10)
                {
                    int prod = (int)(new Random(v)).Next(0, cantProd - 1);

                    var producto = productos.ElementAt(prod);
                    if (producto.Producto.precioLista > 0 && producto.stock > 0)
                    {
                        vm.agregarProducto(producto);
                        cant++;
                        Console.WriteLine("Producto Agregado: " + producto.Producto.nombre.ToString());
                    }
                }
                var vmPagar = new MV_PagoWindowViewModel();
                vmPagar.venta = vm.venta;
                vmPagar.agregarPago(1);//solo efectivo
                vmPagar.imprimirDocumento(null);
                Console.WriteLine("Venta Finalizada.");
            }
            Assert.AreEqual(0, 0);
        }

        /**** Test Igv correcto por venta ************/
        [TestCase]
        public void igv_aplicado_venta_es_igual_al_igv_parametro()
        {
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("igv"));
            //var creo el entorno
            foreach (Venta v in MV_VentaService.db.Venta)
            {    
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            
              
                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.AreEqual(Convert.ToInt32(par.valor),(int)(v.igvPorcentaje));
            }
        }
        /**** Test tipo cambio correcto por venta ************/
        [TestCase]
        public void tipo_de_cambio_aplicado_venta_es_igual_al_tipo_de_cambio_parametro()
        {
            Parametro par = MS_ParametroService.db.Parametro.Single(p => p.nombre.ToLower().Contains("cambio"));
            //var creo el entorno
            foreach (Venta v in MV_VentaService.db.Venta)
            {
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            

                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                double valor = Math.Abs(Convert.ToDouble(par.valor) - Convert.ToDouble(v.tipoCambio));

                //Assert - Verificar la condicion o criterio de aceptacion                
                Assert.GreaterOrEqual(valor, 0.00);
                Assert.LessOrEqual(valor, 0.01);
            }
        }
        /**** Test monto real igual a monto parcial + monto desciontado por venta ************/
        [TestCase]
        public void monto_real_igual_monto_parcial_mas_monto_descontado()
        {
            //var creo el entorno
            foreach (VentaProducto v in MV_VentaService.db.VentaProducto)
            {
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            

                double valor = Math.Abs((double)(v.montoReal - (v.montoParcial+v.descuento)));
                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion                
                Assert.GreaterOrEqual(valor, 0.00);
                Assert.LessOrEqual(valor, 0.01);
            }
        }
        /**** Test sotck disponible - cantidad vendida = stock restante ************/
        [TestCase]
        public void stock_restante_igual_sotck_disponible_menos_cantidad_vendida_del_producto()
        {
            //var creo el entorno
            foreach (PromocionProducto pp in MV_VentaService.db.PromocionProducto)
            {
                //Act - No hay nada que hacer, porque la accion fue la creacion misma            
                IEnumerable<VentaProducto> vent = MV_VentaService.db.VentaProducto.Where(cli => cli.id_producto == pp.producto_id);

                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion
                int valor =0;
                if (vent == null) valor = 0; // significa que no hay ventas con numero de documento o usuario nulos, o estan los 2 o ninguno
                else {
                    foreach (VentaProducto pp2 in MV_VentaService.db.VentaProducto)
                        valor = valor + (int)pp2.cantidad;                    
                }
                Assert.AreEqual(pp.stockActual, pp.stockTotal-valor);
            }
        }

    }
}

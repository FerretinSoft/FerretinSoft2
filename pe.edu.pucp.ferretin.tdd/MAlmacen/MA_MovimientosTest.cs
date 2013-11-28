using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using System.Threading.Tasks;

using NUnit.Framework;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;

namespace pe.edu.pucp.ferretin.tdd.MAlmacen
{    
    [TestFixture]
    public class MA_MovimientosTest
    {
        /******************* Test Nombre de Tienda *************************/
        [TestCase]
        public void stock_LamparaGrande_TiendaLince1_entre_200_y_2000()
        {
            //Arrange = Creo el entorno            
            //var ventana = new MS_AdministrarTiendasViewModel();
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma            
            Tienda tnd = MS_TiendaService.db.Tienda.Single(t => t.nombre.ToLower().Contains("tienda lince 1"));
            Producto prod = MA_ProductoService.db.Producto.Single(p => p.nombre.ToLower().Contains("sanitario celima"));
            ProductoAlmacen prodAlm = MA_ProductoAlmacenService.db.ProductoAlmacen.Single(pa => (pa.id_almacen == tnd.id && pa.id_producto == prod.id));

            int stock = (int)(prodAlm.stock);            

            //Assert - Verificar la condicion o criterio de aceptacion
            Assert.GreaterOrEqual(2000, stock);
            Assert.LessOrEqual(200, stock);
        }

        /************ tipo de movimiento unico*******************************************/
        [TestCase]
        public void Nombre_De_TipoMovimiento_Unico()
        {
            //var creo el entorno

            foreach (MovimientoTipo mt in MA_MovimientoProductoService.db.MovimientoTipo)
            {
                IEnumerable<MovimientoTipo> movTipo = MA_CategoriaService.db.MovimientoTipo.Where(p => p.nombre == mt.nombre);

                //Assert - verificar condicion o criterio de aceptacion

                int cantidadRegistros = movTipo.Count();

                Assert.AreEqual(cantidadRegistros, 1);
            }
        }


        [TestCase]
        public void Movimiento_Con_Campos_Obligatorios()
        {
            //var creo el entorno

            foreach (MovimientoTipo moviTipo in MA_MovimientosService.db.MovimientoTipo)
            {
                MovimientoTipo mt = MA_MovimientosService.db.MovimientoTipo.Single(t => t.nombre.ToLower() ==moviTipo.nombre.ToLower()); ;
                IEnumerable<Movimiento> movi = MA_MovimientosService.db.Movimiento.Where(mov => mov.id_tipo == mt.id);

                int i = 0;
                //Assert - verificar condicion o criterio de aceptacion
                foreach (Movimiento m in movi)
                {
                    Assert.IsNotNull(m.id_estado);
                    Assert.IsNotNull(m.codigo);
                    Assert.IsNotNull(m.fecha);


                }
            }
        }

        /// <summary>
        /// Prueba de insertar movimiento de entrada (verificar stock antes y después)
        /// </summary>
        [TestCase]
        public void InsertarMovimientoEntrada()
        {
            Movimiento mov = new Movimiento();
            DateTime today = DateTime.Today;
            mov.fecha = today;
            mov.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            mov.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Compra"); // un movimiento de entrada
            mov.Tienda1 = MS_TiendaService.obtenerTiendaByCodigo("1"); // tienda destino
            mov.MovimientoProducto = new EntitySet<MovimientoProducto>();
            Producto p = MA_ProductoService.obtenerProductoxCodigo("0000000001");
            ProductoAlmacen pa = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(mov.Tienda1, p);
            MovimientoProducto mp = new MovimientoProducto();
            mp.Producto = p;
            mp.Movimiento = mov;
            mp.cantidad = 10;
            mov.MovimientoProducto.Add(mp);

            // guardo el stock antes
            decimal stockAntes = (decimal)pa.stock;

            ComunService.usuarioL = MS_UsuarioService.obtenerListaUsuarios().First();
            // inserto el movimiento
            MA_MovimientosService.InsertarMovimiento(mov);

            decimal stockDespues = (decimal)pa.stock;

            Assert.AreEqual(stockAntes + 10, stockDespues);
        }

        /// <summary>
        /// Prueba de insertar movimiento de salida (verificar stock antes y después)
        /// </summary>
        [TestCase]
        public void InsertarMovimientoSalida()
        {
            Movimiento mov = new Movimiento();
            DateTime today = DateTime.Today;
            mov.fecha = today;
            mov.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            mov.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Venta"); // un movimiento de salida
            mov.Tienda = MS_TiendaService.obtenerTiendaByCodigo("1"); // tienda destino
            mov.MovimientoProducto = new EntitySet<MovimientoProducto>();
            Producto p = MA_ProductoService.obtenerProductoxCodigo("0000000001");
            ProductoAlmacen pa = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(mov.Tienda, p);
            MovimientoProducto mp = new MovimientoProducto();
            mp.Producto = p;
            mp.Movimiento = mov;
            mp.cantidad = 10;
            mov.MovimientoProducto.Add(mp);

            // guardo el stock antes
            decimal stockAntes = (decimal)pa.stock;

            ComunService.usuarioL = MS_UsuarioService.obtenerListaUsuarios().First();
            // inserto el movimiento
            MA_MovimientosService.InsertarMovimiento(mov);

            decimal stockDespues = (decimal)pa.stock;

            Assert.AreEqual(stockAntes - 10, stockDespues);
        }

        /// <summary>
        /// Prueba de insertar movimiento de transacción (verificar stock antes y después)
        /// </summary>
        [TestCase]
        public void InsertarMovimientoTransaccional()
        {
            Movimiento mov = new Movimiento();
            DateTime today = DateTime.Today;
            mov.fecha = today;
            mov.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            mov.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Transferencia"); // un movimiento de transferencia
            Tienda desde = MS_TiendaService.obtenerTiendaByCodigo("2"); // tienda desde
            Tienda hasta = MS_TiendaService.obtenerTiendaByCodigo("1"); // tienda desde
            mov.Tienda = desde;
            mov.Tienda1 = hasta;
            mov.MovimientoProducto = new EntitySet<MovimientoProducto>();
            Producto p = MA_ProductoService.obtenerProductoxCodigo("0000000001");
            ProductoAlmacen paDesde = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(mov.Tienda, p);
            ProductoAlmacen paHasta = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(mov.Tienda1, p);
            MovimientoProducto mp = new MovimientoProducto();
            mp.Producto = p;
            mp.Movimiento = mov;
            mp.cantidad = 10;
            mov.MovimientoProducto.Add(mp);

            // guardo el stock antes
            decimal stockAntesDesde = (decimal)paDesde.stock;
            decimal stockAntesHasta = (decimal)paHasta.stock;

            ComunService.usuarioL = MS_UsuarioService.obtenerListaUsuarios().First();
            // inserto el movimiento
            MA_MovimientosService.InsertarMovimiento(mov);

            decimal stockDespuesDesde = (decimal)paDesde.stock;
            decimal stockDespuesHasta = (decimal)paHasta.stock;

            Assert.AreEqual(stockAntesDesde - 10, stockDespuesDesde);
            Assert.AreEqual(stockAntesHasta + 10, stockDespuesHasta);
        }

        /// <summary>
        /// Prueba de insertar movimiento de entrada (verificar stock antes y después)
        /// </summary>
        [TestCase]
        public void CodigoMovimientoUnico()
        {
            Movimiento mov = new Movimiento();
            DateTime today = DateTime.Today;
            mov.fecha = today;
            mov.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            mov.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Ajuste"); 
            mov.Tienda1 = MS_TiendaService.obtenerTiendaByCodigo("1"); // tienda destino
            mov.MovimientoProducto = new EntitySet<MovimientoProducto>();
            Producto p = MA_ProductoService.obtenerProductoxCodigo("0000000001");
            MovimientoProducto mp = new MovimientoProducto();
            mp.Producto = p;
            mp.Movimiento = mov;
            mp.cantidad = 10;
            mov.MovimientoProducto.Add(mp);

            ComunService.usuarioL = MS_UsuarioService.obtenerListaUsuarios().First();
            
            // Guardo los codigos de movimientos antes de insertar
            List<String> codigosAntes = new List<string>();
            foreach (var item in MA_MovimientosService.listaMovimientos)
            {
                if (item.id > 0)
                    codigosAntes.Add(item.codigo);
            }
            // inserto el movimiento
            MA_MovimientosService.InsertarMovimiento(mov);

            String codigo = mov.codigo;

            Assert.IsFalse(codigosAntes.Contains(codigo));
        }




    }

}

using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    /// <summary>
    /// Esta Clase contiene todos los Servicios que serán ofrecidos a otros Módulos
    /// </summary>
    public class MA_SharedService : ComunService
    {


        public static Producto obtenerProductoxCodigo(String codigo)
        {
            Producto prod = (from p in db.Producto
                             where p.codigo == codigo
                             select p).SingleOrDefault();
            return prod;
        }

        private static List<MovimientoEstado> _estadosMovimiento;
        public static List<MovimientoEstado> estadosMovimiento
        {
            get
            {
                if (_estadosMovimiento == null) _estadosMovimiento = db.MovimientoEstado.ToList();
                return _estadosMovimiento;
            }
            set
            {
                _estadosMovimiento = value;
            }
        }

        private static IEnumerable<MovimientoTipo> _tiposMovimientos;

        public static IEnumerable<MovimientoTipo> tiposMovimientos
        {
            get
            {
                if (_tiposMovimientos == null)
                {
                    _tiposMovimientos = db.MovimientoTipo;
                }
                //pesimista
                db.Refresh(RefreshMode.OverwriteCurrentValues, _tiposMovimientos);
                return _tiposMovimientos;
            }
            set
            {
                _tiposMovimientos = value;
            }
        }

        private static IEnumerable<SolicitudAbastecimientoEstado> _estadosSolicitud;
        public static IEnumerable<SolicitudAbastecimientoEstado> estadosSolicitud
        {
            get
            {
                if (_estadosSolicitud == null) _estadosSolicitud = db.SolicitudAbastecimientoEstado.ToList();
                return _estadosSolicitud;
            }
            set
            {
                _estadosSolicitud = value;
            }
        }

        //Servicios compartidos para movimientos
        /// <summary>
        /// Registrar venta
        /// </summary>
        /// <param name="tienda">Tienda desde la que se realiza la venta</param>
        /// <param name="items">Listado de VentaProductos para la venta</param>
        /// <returns>Devuelve la cadena vacia si se registró el movimiento correctamente, en caso contrario devuelve el error ocurrido</returns>
        public static String registrarVenta(Tienda tienda, EntitySet<VentaProducto> items)
        {
            Movimiento movimiento = new Movimiento();
            movimiento.codigo = Movimiento.generateCode();
            movimiento.fecha = DateTime.Today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Venta");
            movimiento.Tienda = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidad;
                current.Movimiento = movimiento;
                current.Producto = items[i].Producto;
                movimiento.MovimientoProducto.Add(current);                
            }
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }

        /// <summary>
        /// Registrar devolución
        /// </summary>
        /// <param name="tienda">Tienda desde la que se realiza la venta</param>
        /// <param name="items">Listado de DevolucionProducto para la venta</param>
        /// <returns>Devuelve la cadena vacia si se registró el movimiento correctamente, en caso contrario devuelve el error ocurrido</returns>
        public static String registrarDevolucion(Tienda tienda, EntitySet<DevolucionProducto> items)
        {
            Movimiento movimiento = new Movimiento();
            DateTime today = DateTime.Today;
            movimiento.codigo = Movimiento.generateCode();
            movimiento.fecha = today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Devolución");
            movimiento.Tienda1 = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidad;
                current.Movimiento = movimiento;
                current.Producto = items[i].Producto;
                movimiento.MovimientoProducto.Add(current);
            }
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }

        /// <summary>
        /// Registrar entrada de productos por Compra
        /// </summary>
        /// <param name="tienda">Almacen distribuidor que recepciona los productos</param>
        /// <param name="items">Listado de productos</param>
        /// <returns>Devuelve la cadena vacia si se registró el movimiento correctamente, en caso contrario devuelve el error ocurrido</returns>
        public static String registrarCompra(Tienda tienda, EntitySet<DocumentoCompraProducto> items)
        {
            Movimiento movimiento = new Movimiento();
            DateTime today = DateTime.Today;
            movimiento.codigo = Movimiento.generateCode();
            movimiento.fecha = today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Compra");
            movimiento.Tienda = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidad;
                current.Movimiento = movimiento;
                current.Producto = items[i].Producto;
                movimiento.MovimientoProducto.Add(current);
            }
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }



    }
}

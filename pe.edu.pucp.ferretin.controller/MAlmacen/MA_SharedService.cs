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
            Producto prod = (from p in MA_ProductoService.listaProductos
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

        private static List<MovimientoTipo> _tiposMovimientos;

        public static List<MovimientoTipo> tiposMovimientos
        {
            get
            {
                if (_tiposMovimientos == null)
                {
                    _tiposMovimientos = db.MovimientoTipo.ToList();
                }
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
        public static String registrarVenta(Tienda tienda, IEnumerable<VentaProducto> items)
        {
            if (items.Count() <= 0) return "Debe haber al menos un producto para realizar el movimiento.";
            
            Movimiento movimiento = new Movimiento();
            movimiento.fecha = DateTime.Now;//Fecha y hora actual
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Venta");
            movimiento.Tienda = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count(); i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items.ElementAt(i).cantidad;
                current.Movimiento = movimiento;
                current.Producto = items.ElementAt(i).Producto;
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
            if (items.Count <= 0) return "Debe haber al menos un producto para realizar el movimiento.";
            
            Movimiento movimiento = new Movimiento();
            DateTime today = DateTime.Today;
            movimiento.fecha = today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Devolución");
            movimiento.Tienda1 = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].dardebaja != null && (bool)items[i].dardebaja) continue;
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidad;
                current.Movimiento = movimiento;
                current.Producto = items[i].Producto;
                movimiento.MovimientoProducto.Add(current);
            }
            if (movimiento.MovimientoProducto.Count <= 0) return "";
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }

        /// <summary>
        /// Registrar entrada de productos por Compra
        /// </summary>
        /// <param name="tienda">Almacen distribuidor que recepciona los productos</param>
        /// <param name="items">Listado de productos</param>
        /// <returns>Devuelve la cadena vacia si se registró el movimiento correctamente, en caso contrario devuelve el error ocurrido</returns>
        public static String registrarCompra(Tienda tienda, EntitySet<GuiaRemisionProducto> items)
        {
            if (items.Count <= 0) return "Debe haber al menos un producto para realizar el movimiento.";
            
            Movimiento movimiento = new Movimiento();
            DateTime today = DateTime.Today;
            movimiento.fecha = today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Compra");
            movimiento.Tienda1 = tienda;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidadRecibida;
                current.Movimiento = movimiento;
                current.Producto = items[i].DocumentoCompraProducto.Producto;
                movimiento.MovimientoProducto.Add(current);
            }
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }

        /// <summary>
        /// Registrar transferencia de abastecimiento entre almacenes
        /// </summary>
        /// <param name="origen">Tienda origen (abastecedora)</param>
        /// <param name="destino">Tienda destino (abastecida)</param>
        /// <param name="items">Productos abastecidos</param>
        /// <returns></returns>
        public static String registrarTransferenciaAbastecimiento(Tienda origen, Tienda destino, List<MA_SolicitudAbastecimientoService.AtencionSolicitudProducto> items)
        {
            if (items.Count <= 0) return "Debe haber al menos un producto para realizar el movimiento.";
            Movimiento movimiento = new Movimiento();
            DateTime today = DateTime.Today;
            movimiento.fecha = today;
            movimiento.MovimientoEstado = MA_EstadoMovimientoService.getMovimientoEstadoByName("Finalizado");
            movimiento.MovimientoTipo = MA_TipoMovimientoService.getMovimientoTipoByName("Transferencia");
            movimiento.Tienda = origen;
            movimiento.Tienda1 = destino;
            movimiento.MovimientoProducto = new EntitySet<MovimientoProducto>();
            MovimientoProducto current;
            for (int i = 0; i < items.Count; i++)
            {
                current = new MovimientoProducto();
                current.cantidad = items[i].cantidad;
                current.Movimiento = movimiento;
                current.Producto = items[i].producto.Producto;
                movimiento.MovimientoProducto.Add(current);
                items[i].producto.cantidadAtendida += items[i].cantidad;
                items[i].producto.cantidadRestante -= items[i].cantidad;
            }
            bool ok = MA_MovimientosService.InsertarMovimiento(movimiento);
            return (ok) ? "" : "Error al registrar el movimiento";
        }

        /// <summary>
        /// Devuelve un listado de productos que necesitan ser abastecidos para un almacén determinado
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        public static Dictionary<ProductoAlmacen, decimal> obtenerProductosPorAbastecer(Tienda almacen)
        {
            Dictionary<ProductoAlmacen, decimal> result = new Dictionary<ProductoAlmacen, decimal>();
            ProductoAlmacen current; decimal diferencia;
            int count = almacen.ProductoAlmacen.Count;
            for (int i = 0; i < count; i++)
			{
                current = almacen.ProductoAlmacen[i];
                diferencia = (decimal)current.stock - (decimal)current.stockMin;
                if(diferencia < 0)
                    result.Add(current, diferencia * -1);
			}
            //foreach (var item in MA_SolicitudAbastecimientoService.buscarSolicitudesPendientesPorTienda(almacen))
            //{
            //    foreach (var prod in item.SolicitudAbastecimientoProducto)
            //    {
            //        current = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(almacen, prod.Producto);
            //        if (prod.cantidadRestante > current.stock - current.stockMin)
            //        {
            //            if (!result.ContainsKey(current))
            //                result.Add(current, (decimal)prod.cantidadRestante - (decimal)current.stock);
            //            else
            //                result[current] = result[current] + (decimal)(prod.cantidadRestante == null ? 0 : prod.cantidadRestante); 
            //        }
            //    }                
            //}
            return result;
        }

    }
}

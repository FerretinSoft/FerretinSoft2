using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_MovimientosService : MA_ComunService
    {
        public class MovimientoProductoTienda
        {
            public MovimientoProductoTienda(MovimientoProducto movimientoProducto, ProductoAlmacen productoAlmacen)
            {
                this.movimientoProducto = movimientoProducto;
                this.productoAlmacen = productoAlmacen;
            }

            public MovimientoProducto movimientoProducto { get; set; }
            public ProductoAlmacen productoAlmacen { get; set; }
        }

        private static IEnumerable<Movimiento> _listaMovimientos;

        public static IEnumerable<Movimiento> listaMovimientos
        {
            get
            {
                if (_listaMovimientos == null)
                {
                    _listaMovimientos = db.Movimiento;
                }
                
                //Usando concurrencia pesimista:
                ///La lista de movimientos se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaMovimientos);
                return _listaMovimientos;
            }
            set
            {
                _listaMovimientos = value;
            }
        }

        public static Movimiento ObtenerMovimientoPorId(int id)
        {
            Movimiento movimiento = (from m in listaMovimientos
                               where m.id.Equals(id)
                               select m).Single();

            return movimiento;
        }

        public static IEnumerable<Movimiento> obtenerMovimientoPorProducto(int idProd,int idTienda)
        {
            IEnumerable<Movimiento> lMov = from m in db.Movimiento
                                           from mp in db.MovimientoProducto
                                           where (mp.id_producto == idProd) &&
                                           (mp.id_movimiento == m.id) &&
                                           ((m.id_almacen_desde==idTienda)||
                                           (m.id_almacen_hasta==idTienda))
                                           select m;
            foreach (Movimiento m in lMov)
            {
                MovimientoProducto mpr = (from mp in db.MovimientoProducto
                                          where (mp.id_producto == idProd) &&
                                          (mp.id_movimiento == m.id)
                                          select mp).Single();
                m.mp = mpr;
            }

            return lMov;
        }

        public static IEnumerable<MovimientoProductoTienda> buscarProductosPorMovimientoTienda(Movimiento movimiento)
        {
            Tienda almacen = null;
            if (movimiento.Tienda != null) almacen = movimiento.Tienda;
            List<MovimientoProductoTienda> result = new List<MovimientoProductoTienda>();
            if (movimiento == null) return result;
            for (int i = 0; i < movimiento.MovimientoProducto.Count; i++)
            {
                ProductoAlmacen pa = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(almacen,
                                                                    movimiento.MovimientoProducto[i].Producto);
                result.Add(new MovimientoProductoTienda(movimiento.MovimientoProducto[i], pa));
            }

            return result;
        }

        public static List<MovimientoProducto> buscarEntradasPorTiendaPeriodo(Tienda almacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            var movimientos = (from m in listaMovimientos
                               where ((m.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.ENTRADA ||
                                       m.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA)
                                       && m.Tienda1 == almacen && m.fecha >= fechaDesde && m.fecha <= fechaHasta)
                               select m);

            List<MovimientoProducto> result = new List<MovimientoProducto>();
            foreach (var mov in movimientos)
            {
                foreach (var item in mov.MovimientoProducto)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static List<MovimientoProducto> buscarSalidasPorTiendaPeriodo(Tienda almacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            var movimientos = (from m in listaMovimientos
                               where ((m.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.SALIDA ||
                                       m.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA)
                                       && m.Tienda == almacen && m.fecha >= fechaDesde && m.fecha <= fechaHasta)
                               select m);

            List<MovimientoProducto> result = new List<MovimientoProducto>();
            foreach (var mov in movimientos)
            {
                foreach (var item in mov.MovimientoProducto)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<Movimiento> buscarMovimientos(Tienda searchAlmacen, MovimientoEstado searchEstado, DateTime searchFechaDesde, DateTime searchFechaHasta)
        {
            return listaMovimientos
                .Where(m => (searchAlmacen == null) || (searchAlmacen.id <= 0) || (m.Tienda == searchAlmacen || m.Tienda1 == searchAlmacen))
                .Where(m => (searchEstado == null) || (searchEstado.id <= 0) || (m.MovimientoEstado == searchEstado))
                .Where(m => (m.fecha >= searchFechaDesde) && (m.fecha <= searchFechaHasta))
                .OrderBy(m => m.fecha);
        }

        public static bool ActualizarMovimiento(Movimiento movimiento)
        {
            if (movimiento.MovimientoEstado.nombre == "Finalizado")
            {
                if (!finalizarMovimiento(movimiento)) return false;

            }
            return enviarCambios();
        }

        private static bool validarMovimiento(Movimiento movimiento)
        {
            if (movimiento.MovimientoTipo.categoriaEnum == MovimientoTipo.CategoriaMovimiento.ENTRADA) 
                return true; //No hay restricciones para entrada a almacén
            MovimientoProducto current;
            for (int i = 0; i < movimiento.MovimientoProducto.Count; i++)
            {
                current = movimiento.MovimientoProducto[i];
                ProductoAlmacen pa = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(movimiento.Tienda, current.Producto);
                if (pa.stock - current.cantidad < 0) return false;
            }
            return true;
        }

        private static bool finalizarMovimiento(Movimiento movimiento)
        {
            Dictionary<Producto, decimal> productos = new Dictionary<Producto, decimal>();
            for (int i = 0; i < movimiento.MovimientoProducto.Count; i++)
            {
                productos.Add(movimiento.MovimientoProducto[i].Producto,
                    (decimal)(movimiento.MovimientoProducto[i].cantidad != null ? movimiento.MovimientoProducto[i].cantidad : 0));
            }
            //actualizar stock
            bool noErrors = true;
            switch (movimiento.MovimientoTipo.categoriaEnum)
            {
                case MovimientoTipo.CategoriaMovimiento.ENTRADA:
                    noErrors = noErrors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                    break;
                case MovimientoTipo.CategoriaMovimiento.SALIDA:
                    noErrors = noErrors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                    break;
                case MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA:
                    noErrors = noErrors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                    noErrors = noErrors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                    break;
                default:
                    return false; // categoria de movimiento no valida
            }
            return noErrors; 
        }

        public static bool InsertarMovimiento(Movimiento movimiento)
         {
            if (!listaMovimientos.Contains(movimiento))
            {
                if (!validarMovimiento(movimiento)) return false; //movimiento no válido
                
                //Obtiene el ultimo codigo de la tienda y le suma 1 para que se el codigo de la tienda a agregar
                String baseCodigo = DateTime.Now.ToString("yyyyMMddHHmmss");
                IEnumerable<Movimiento> anteriores = listaMovimientos.Where(t => t.codigo.StartsWith(baseCodigo)).OrderBy(t => t.id);
                String ultimoCodigo = anteriores.Count() <= 0 ? "" : anteriores.Last().codigo;
                String proxCodigo = (ultimoCodigo.Length > 0)?(Int32.Parse(ultimoCodigo.Substring(ultimoCodigo.Length - 2)) + 1).ToString() : "";
                if (proxCodigo.Length == 2)
                    movimiento.codigo = baseCodigo + proxCodigo;
                else if (proxCodigo.Length == 1)
                    movimiento.codigo = baseCodigo + "0" + proxCodigo;
                else // cadena vacia
                    movimiento.codigo = baseCodigo + "01";
                    
                if (movimiento.MovimientoEstado.nombre == "Finalizado")
                {
                    if (!finalizarMovimiento(movimiento)) return false;
                }
                
                if (movimiento.id <= 0) db.Movimiento.InsertOnSubmit(movimiento);
                return enviarCambios();
                
            }
            else
            {
                return false;
            }
        }

        public static bool actualizarStockMovimiento(Tienda almacen, char tipo, Dictionary<Producto,decimal> productos)
        {
            bool result = true; bool mode = true;
            for (int i = 0; i < productos.Count; i++)
            {
                if (tipo == 'E') mode = true; //plus
                else if (tipo == 'S') mode = false; //minus
                
                result = result && MA_ProductoAlmacenService.updateStock(almacen, productos.Keys.ElementAt(i), 
                                                            productos.Values.ElementAt(i), mode);
            }
            return result;
        }


    }
}

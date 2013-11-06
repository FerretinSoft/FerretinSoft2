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
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaMovimientos);
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

        public static IEnumerable<Movimiento> obtenerMovimientoPorProducto(int idProd)
        {
            IEnumerable<Movimiento> lMov = from m in db.Movimiento
                                           from mp in db.MovimientoProducto
                                           where (mp.id_producto == idProd) &&
                                           (mp.id_movimiento == m.id)
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

        private static bool finalizarMovimiento(Movimiento movimiento)
        {
            Dictionary<Producto, decimal> productos = new Dictionary<Producto, decimal>();
            for (int i = 0; i < movimiento.MovimientoProducto.Count; i++)
            {
                productos.Add(movimiento.MovimientoProducto[i].Producto,
                    (decimal)(movimiento.MovimientoProducto[i].cantidad != null ? movimiento.MovimientoProducto[i].cantidad : 0));
            }
            //actualizar stock
            bool errors = true;
            switch (movimiento.MovimientoTipo.categoriaEnum)
            {
                case MovimientoTipo.CategoriaMovimiento.ENTRADA:
                    errors = errors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                    break;
                case MovimientoTipo.CategoriaMovimiento.SALIDA:
                    errors = errors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                    break;
                case MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA:
                    errors = errors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                    errors = errors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                    break;
                default:
                    return false; // categoria de movimiento no valida
            }

            return errors; 
        }

        public static bool InsertarMovimiento(Movimiento movimiento)
         {
            if (!db.Movimiento.Contains(movimiento))
            {
                if (movimiento.MovimientoEstado.nombre == "Finalizado")
                {
                    Dictionary<Producto, decimal> productos = new Dictionary<Producto, decimal>();
                    for (int i = 0; i < movimiento.MovimientoProducto.Count; i++)
                    {
                        productos.Add(movimiento.MovimientoProducto[i].Producto, 
                            (decimal)(movimiento.MovimientoProducto[i].cantidad != null ? movimiento.MovimientoProducto[i].cantidad : 0));
                    }
                    //actualizar stock
                    bool errors = true;
                    switch (movimiento.MovimientoTipo.categoriaEnum)
                    {
                        case MovimientoTipo.CategoriaMovimiento.ENTRADA:
                            errors = errors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                            break;
                        case MovimientoTipo.CategoriaMovimiento.SALIDA:
                            errors = errors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                            break;
                        case MovimientoTipo.CategoriaMovimiento.TRANSFERENCIA:
                            errors = errors && actualizarStockMovimiento(movimiento.Tienda, 'S', productos);
                            errors = errors && actualizarStockMovimiento(movimiento.Tienda1, 'E', productos);
                            break;
                        default:
                            return false; // categoria de movimiento no valida
                    }
                    if (!errors)
                        return false; //error actualizando los stocks de movimiento
                }
                if (movimiento.id <= 0) db.Movimiento.InsertOnSubmit(movimiento);
                //db.SubmitChanges(); return true;
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

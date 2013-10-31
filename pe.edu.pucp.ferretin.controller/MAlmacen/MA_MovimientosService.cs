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

        public static IEnumerable<Movimiento> buscarMovimientos(Tienda searchAlmacen, MovimientoEstado searchEstado, DateTime searchFechaDesde, DateTime searchFechaHasta)
        {
            return listaMovimientos
                .Where(m => (searchAlmacen == null) || (searchAlmacen.id <= 0) || (m.Tienda == searchAlmacen || m.Tienda1 == searchAlmacen))
                .Where(m => (searchEstado == null) || (searchEstado.id <= 0) || (m.MovimientoEstado == searchEstado))
                .Where(m => (m.fecha >= searchFechaDesde) && (m.fecha <= searchFechaHasta))
                .OrderBy(m => m.fecha);
        }

        public static void ActualizarMovimiento(Movimiento mov)
        {
            //dc.Movimiento.InsertOnSubmit(mov);
            //db.SubmitChanges();
        }

        public static bool InsertarMovimiento(Movimiento movimiento)
        {
            if (!db.Movimiento.Contains(movimiento))
            {
                db.Movimiento.InsertOnSubmit(movimiento);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }


    }
}

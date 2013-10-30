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

        private static IEnumerable<Movimiento> listaMovimientos
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

        public static IEnumerable<Movimiento> ObtenerListaMovimientos(Dictionary<string, object> parametros)
        {
            return from m in listaMovimientos
                   where
                   ((!parametros.ContainsKey("tienda") || m.Tienda == null || m.id_almacen_desde == (int)parametros["tienda"]) &&
                    (!parametros.ContainsKey("fechaDesde") || m.fecha >= (DateTime)parametros["fechaDesde"])  && 
                    (!parametros.ContainsKey("fechaDesde") || m.fecha <= (DateTime)parametros["fechaHasta"]))
                   orderby m.fecha
                   select m;
        }

        public static void ActualizarMovimiento(Movimiento mov)
        {
            //dc.Movimiento.InsertOnSubmit(mov);
            //db.SubmitChanges();
        }

        public static bool insertarMovimiento(Movimiento movimiento)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;

namespace pe.edu.pucp.ferretin.controller
{
    public class MovimientosService
    {
        static FerretinDataContext dc = new FerretinDataContext();

        private static IEnumerable<Movimiento> _listaMovimientos = null;

        private static IEnumerable<Movimiento> listaMovimientos
        {
            get
            {
                if (_listaMovimientos == null)
                {
                    ObtenerListaMovimientos();
                }
                return _listaMovimientos;
            }
            set
            {
                _listaMovimientos = value;
            }
        }

        public static IEnumerable<Movimiento> ObtenerListaMovimientos()
        {
            listaMovimientos = from mov in dc.Movimiento
                            orderby mov.fecha
                            select mov;
            return listaMovimientos;
        }

        


        public static IEnumerable<Movimiento> ObtenerListaMovimientos(Dictionary<string, object> parametros)
        {
            return from m in listaMovimientos
                   where
                   ((!parametros.ContainsKey("almacen") || m.id_almacen_desde == null || m.id_almacen_desde.Equals(((Tienda)parametros["almacen"]).codigo)) &&
                    (!parametros.ContainsKey("fechaDesde") || m.fecha >= (DateTime)parametros["fechaDesde"])  && 
                    (!parametros.ContainsKey("fechaDesde") || m.fecha <= (DateTime)parametros["fechaHasta"]))
                   orderby m.fecha
                   select m;
        }

        public static void ActualizarMovimiento(Movimiento mov)
        {
            dc.Movimiento.InsertOnSubmit(mov);
            dc.SubmitChanges();
        }

        public static void InsertarMovimiento(Movimiento mov)
        {
            //mov.;
            dc.SubmitChanges();
        }
    }
}

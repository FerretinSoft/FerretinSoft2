using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_EstadoMovimientoService : MA_ComunService
    {
        /*static FerretinDataContext dc = new FerretinDataContext();

        private static List<MovimientoEstado> _listaEstadoMovimientos = null;

        private static List<MovimientoEstado> listaEstadoMovimientos
        {
            get
            {
                if (_listaEstadoMovimientos == null)
                {
                    ObtenerListaEstadoMovimientos();
                }
                return _listaEstadoMovimientos;
            }
            set
            {
                _listaEstadoMovimientos = value;
            }
        }

        public static List<MovimientoEstado> ObtenerListaEstadoMovimientos()
        {
            var result = from estado in db.MovimientoEstado
                                     orderby estado.nombre
                                     select estado;
            
            return result.ToList();
        }*/


    }
}

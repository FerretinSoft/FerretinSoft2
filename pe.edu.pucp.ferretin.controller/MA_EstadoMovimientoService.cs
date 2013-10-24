using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public class MA_EstadoMovimientoService
    {
        static FerretinDataContext dc = new FerretinDataContext();

        private static IEnumerable<Movimiento_Estado> _listaEstadoMovimientos = null;

        private static IEnumerable<Movimiento_Estado> listaEstadoMovimientos
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

        public static IEnumerable<Movimiento_Estado> ObtenerListaEstadoMovimientos()
        {
            listaEstadoMovimientos = from estado in dc.Movimiento_Estado
                                     orderby estado.nombre
                                     select estado;
            return listaEstadoMovimientos;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public class MA_TipoMovimientoService
    {
        static FerretinDataContext dc = new FerretinDataContext();

        private static IEnumerable<MovimientoTipo> _listaTipoMovimientos = null;

        private static IEnumerable<MovimientoTipo> listaTipoMovimientos
        {
            get
            {
                if (_listaTipoMovimientos == null)
                {
                    ObtenerListaTipoMovimientos();
                }
                return _listaTipoMovimientos;
            }
            set
            {
                _listaTipoMovimientos = value;
            }
        }

        public static IEnumerable<MovimientoTipo> ObtenerListaTipoMovimientos()
        {
            listaTipoMovimientos = from tipo in dc.MovimientoTipo
                                   orderby tipo.nombre
                                   select tipo;
            return listaTipoMovimientos;
        }
        
    }
}

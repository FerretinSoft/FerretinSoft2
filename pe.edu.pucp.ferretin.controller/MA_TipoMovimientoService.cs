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

        private static IEnumerable<Movimiento_Tipo> _listaTipoMovimientos = null;

        private static IEnumerable<Movimiento_Tipo> listaTipoMovimientos
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

        public static IEnumerable<Movimiento_Tipo> ObtenerListaTipoMovimientos()
        {
            listaTipoMovimientos = from tipo in dc.Movimiento_Tipo
                                   orderby tipo.nombre
                                   select tipo;
            return listaTipoMovimientos;
        }
        
    }
}

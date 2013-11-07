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
        public static MovimientoEstado getMovimientoEstadoByName(String name)
        {
            var respuesta = (from t in db.MovimientoEstado
                             where t.nombre.ToLower().Equals(name.ToUpper())
                             select t);
            return respuesta.Count() >= 1 ? respuesta.Single() : null;
        }


    }
}

using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MAlmacen
{
    public class MA_UnidadMedidaServiceMateriales : MA_ComunService
    {

        public static IEnumerable<UnidadMedida> obtenerUnidadesMedida()
        {
            return (from um in db.UnidadMedida orderby um.nombre select um);
        }

        public static IEnumerable<Material> obtenerMaterialesPrimario()
        {
            return (from m in db.Material orderby m.nombre select m);
        }

    }
}

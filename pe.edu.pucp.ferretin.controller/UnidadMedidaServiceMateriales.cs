using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller
{
    public class UnidadMedidaServiceMateriales
    {
        private static FerretinDataContext db = new FerretinDataContext();

        public static IEnumerable<Tbl_Unidad_Medida> obtenerUnidadesMedida()
        {
            return (from um in db.Tbl_Unidad_Medida orderby um.nombre select um);
        }

        public static IEnumerable<Tbl_Material> obtenerMaterialesPrimario()
        {
            return (from m in db.Tbl_Material orderby m.nombre select m);
        }

    }
}

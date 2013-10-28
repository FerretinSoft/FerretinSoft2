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

        public static IEnumerable<Unidad_Medida> obtenerUnidadesMedida()
        {
            return (from um in db.Unidad_Medida orderby um.nombre select um);
        }

        public static IEnumerable<Material> obtenerMaterialesPrimario()
        {
            return (from m in db.Material orderby m.nombre select m);
        }

    }
}

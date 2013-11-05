using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MCompras
{
    public class MC_RubroService : MR_ComunService
    {
        private static IEnumerable<Rubro> _rubro;
        public static IEnumerable<Rubro> rubro
        {
            get
            {
                if (_rubro == null)
                    _rubro = from r in db.Rubro select r;
                return _rubro;
            }
            set
            {
                _rubro = value;
            }
        }


        internal static IEnumerable<Rubro> obtenerRubros(Rubro selectedRubro)
        {
            return from r in rubro
                   where r.id== selectedRubro.id
                   select r;
        }

    }
}

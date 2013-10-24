using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MR_CargTieService
    {
        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Cargo> _cargos;
        public static IEnumerable<Cargo> cargos
        {
            get
            {
                if (_cargos == null)
                    _cargos = from c in db.Cargo select c;
                return _cargos;
            }
            set
            {
                _cargos = value;
            }
        }

        private static IEnumerable<Tienda> _tiendas;
        public static IEnumerable<Tienda> tiendas
        {
            get
            {
                if (_tiendas == null)
                    _tiendas = from p in db.Tiendas select p;
                return _tiendas;
            }
            set
            {
                _tiendas = value;
            }
        }


       
        internal static IEnumerable<Cargo> obtenerCargos(string selectedCargo)
        {
            return from c in cargos
                   where c.id.Equals(selectedCargo)
                   select c;
        }

        internal static IEnumerable<Tienda> obtenerTiendas(string selectedTienda)
        {
            return from t in tiendas
                   where t.id.Equals(selectedTienda)
                   select t;
        }

        
    }
}

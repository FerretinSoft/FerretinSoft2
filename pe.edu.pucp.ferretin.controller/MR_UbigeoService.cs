using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MR_UbigeoService
    {
        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<UbigeoDepartamento> _departamentos;
        public static IEnumerable<UbigeoDepartamento> departamentos
        {
            get
            {
                if (_departamentos == null)
                    _departamentos = from d in db.UbigeoDepartamento select d;
                return _departamentos;
            }
            set
            {
                _departamentos = value;
            }
        }

        private static IEnumerable<UbigeoProvincia> _provincias;
        public static IEnumerable<UbigeoProvincia> provincias
        {
            get
            {
                if (_provincias == null)
                    _provincias = from p in db.UbigeoProvincia select p;
                return _provincias;
            }
            set
            {
                _provincias = value;
            }
        }
        private static IEnumerable<UbigeoDistrito> _distritos;
        public static IEnumerable<UbigeoDistrito> distritos
        {
            get
            {
                if (_distritos == null)
                    _distritos = from d in db.UbigeoDistrito select d;
                return _distritos;
            }
            set
            {
                _distritos = value;
            }
        }


        internal static IEnumerable<UbigeoDistrito> obtenerDistritos(string selectedProvincia)
        {
            return from d in distritos
                   where d.id_ubig_provincia.Equals(selectedProvincia)
                   select d;
        }

        internal static IEnumerable<UbigeoProvincia> obtenerProvincias(string selectedDepartamento)
        {
            return from d in provincias
                   where d.id_ubig_departamento.Equals(selectedDepartamento)
                   select d;
        }
    }
}

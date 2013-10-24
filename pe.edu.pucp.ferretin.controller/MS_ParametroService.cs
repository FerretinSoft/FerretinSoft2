using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MS_ParametroService
    {
        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Parametro> _listaParametros = null;

        private static IEnumerable<Parametro> listaParametros
        {
            get
            {
                if (_listaParametros == null)
                {
                    _listaParametros = obtenerListaParametros();
                }
                return _listaParametros;
            }
            set
            {
                _listaParametros = value;
            }
        }

        public static IEnumerable<Parametro> obtenerListaParametros()
        {
            listaParametros = from p in db.Parametro
                            orderby p.id
                            select p;
            return listaParametros;
        }

        public static void actualizarParametro(Parametro parametro)
        {
            db.SubmitChanges();
        }
    }
}

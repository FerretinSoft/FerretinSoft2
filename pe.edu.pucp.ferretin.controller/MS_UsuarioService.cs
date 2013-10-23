using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MS_UsuarioService
    {

        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Usuario> _listaUsuarios = null;

        private static IEnumerable<Usuario> listaUsuarios
        {
            get
            {
                if (_listaUsuarios == null)
                {
                    obtenerListaUsuarios();
                }
                return _listaUsuarios;
            }
            set
            {
                _listaUsuarios = value;
            }
        }

        public static IEnumerable<Usuario> obtenerListaUsuarios()
        {
            listaUsuarios = from p in db.Usuarios
                            orderby p.dni
                            select p;
            return listaUsuarios;
        }


    }
}

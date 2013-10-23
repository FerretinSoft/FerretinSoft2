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

        /*******************************************************/
        public static IEnumerable<Usuario> obtenerListaUsuariosBy(Usuario usuario)
        {
            return from c in listaUsuarios
                   where
                   (c.dni != null && c.dni.Contains(usuario.dni)
                       && c.nombre != null && c.nombre.Contains(usuario.nombre)
                       && c.contrasena != null && c.contrasena.Contains(usuario.contrasena)
                       /*&& c.id_perfil != null && c.id_perfil.Contains(usuario.id_perfil)
                       && c.estado != null && c.estado.Contains(usuario.estado)*/
                    )
                   orderby c.nombre
                   select c;
        }
        /*******************************************************/
        public static void insertarUsuario(Usuario usuario)
        {
            db.Usuarios.InsertOnSubmit(usuario);
            db.SubmitChanges();
        }
        /*******************************************************/
        public static void actualizarUsuario(Usuario usuario)
        {
            db.SubmitChanges();
        }

    }
}

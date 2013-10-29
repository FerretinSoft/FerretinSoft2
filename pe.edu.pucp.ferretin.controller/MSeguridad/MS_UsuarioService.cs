using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Linq;

using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_UsuarioService : MS_ComunService
    {

        /*******************************************************/
        public static IEnumerable<Usuario> _listaUsuarios = null;
        public static IEnumerable<Usuario> listaUsuarios
        {
            get
            {
                if (_listaUsuarios == null)
                {
                    _listaUsuarios = db.Usuario;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaUsuarios);
                return _listaUsuarios;
            }
            set
            {
                _listaUsuarios = value;
            }
        }
        /*******************************************************/
        public static IEnumerable<Usuario> obtenerListaUsuarios()
        {
            listaUsuarios = from p in db.Usuario
                            orderby p.id_empleado
                            select p;
            return listaUsuarios;
        }

        /*******************************************************/
        public static IEnumerable<Usuario> obtenerListaUsuariosBy(Usuario usuario)
        {
            return from c in listaUsuarios
                   where
                   (c.id_empleado != null && c.id_empleado == usuario.id_empleado
                       && c.nombre != null && c.nombre.ToLower().Contains(usuario.nombre.ToLower().Trim())
                       && c.contrasena != null && c.contrasena.Contains(usuario.contrasena)
                       /*&& c.id_perfil != null && c.id_perfil.Contains(usuario.id_perfil)
                       && c.estado != null && c.estado.Contains(usuario.estado)*/
                    )
                   orderby c.nombre
                   select c;
        }
        /*******************************************************/
        //public static void insertarUsuario(Usuario usuario)
        //{
        //    db.Usuario.InsertOnSubmit(usuario);
        //    db.SubmitChanges();
        //}
        /*******************************************************/
        public static void actualizarUsuario(Usuario usuario)
        {
            db.SubmitChanges();
        }

        private static IEnumerable<Empleado> _listaEmpleados;
        private static IEnumerable<Empleado> listaEmpleados
        {
            get
            {
                if (_listaEmpleados == null)
                {
                    _listaEmpleados = MR_EmpleadoService.obtenerListaEmpleados();
                }
                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
            }
        }

        public static System.Collections.IEnumerable obtenerListaUsuariosBy(Usuario usuario, Empleado empleado)
        {

            return from u in listaUsuarios
                   where
                   (u.id_empleado != null && u.id_empleado == usuario.id_empleado
                       && u.nombre != null && u.nombre.ToLower().Contains(usuario.nombre.ToLower().Trim())
                       && u.Empleado != null &&
                            (u.Empleado.nombre != null && u.Empleado.nombre.ToLower().Contains(empleado.nombre.ToLower().Trim())
                            && u.Empleado.apPaterno != null && u.Empleado.apPaterno.ToLower().Contains(empleado.apPaterno.ToLower().Trim())
                       //&& u.Empleado.apMaterno != null && u.Empleado.apMaterno.ToLower().Contains(empleado.apMaterno.ToLower().Trim()) 
                            )
                       && (usuario.id_perfil == null || (u.id_perfil != null && u.id_perfil.Equals(usuario.id_perfil)))
                       && (usuario.estado == null || (u.estado != null && u.estado.Equals(usuario.estado)))
                    )
                   orderby u.nombre
                   select u;
        }

        /*******************************************************/
        public static IEnumerable<Usuario> buscar(string codigo, string nomUsuario, int perfil, string nombres, string apellidos, int estado)
        {
            return from u in listaUsuarios
                   where (
                       //Cada fila es un filtro
                   (u.codUsuario != null && u.codUsuario.Contains(codigo.ToLower().Trim())
                       && u.nombre != null && u.nombre.ToLower().Contains(nomUsuario.ToLower().Trim())
                       && (perfil == 0 || (u.id_perfil != null && u.id_perfil.Equals(perfil)))
                       && u.Empleado != null &&
                            (u.Empleado.nombre != null && u.Empleado.nombre.ToLower().Contains(nombres.ToLower().Trim())
                            && u.Empleado.apPaterno != null && u.Empleado.apPaterno.ToLower().Contains(apellidos.ToLower().Trim())
                       //&& u.Empleado.apMaterno != null && u.Empleado.apMaterno.ToLower().Contains(apellidos.apMaterno.ToLower().Trim()) 
                       )
                       && (estado == 0 || (u.estado != null && u.estado.Equals(estado == 1 ? true : false)))
                    )
                    )
                   orderby u.nombre
                   select u;
        }
        /*******************************************************/
        public static bool insertarUsuario(Usuario usuario)
        {
            if (!db.Usuario.Contains(usuario))
            {
                db.Usuario.InsertOnSubmit(usuario);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        /*******************************************************/

        private static IEnumerable<Perfil> _listaPerfiles;

        private static IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                if (_listaPerfiles == null)
                {
                    _listaPerfiles = from p in db.Perfil
                                     select p;
                }
                return _listaPerfiles;
            }
            set
            {
                _listaPerfiles = value;
            }
        }

        public static IEnumerable<Perfil> obtenerPerfiles()
        {
            return listaPerfiles;
        }
    }
}
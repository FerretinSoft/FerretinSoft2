﻿using System;
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
        /*******************************************************
                            PARA USUARIOS
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


        /*******************************************************
                            PARA EMPLEADOS
        /*******************************************************/
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
        /*******************************************************/
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
        public static IEnumerable<Usuario> buscar(string codigo, string nomUsuario, Perfil perfil, string nombres, string apellidos, int estado)
        {
            
            IEnumerable<Usuario> usuarios = listaUsuarios;
            //Filtro por código
            usuarios = usuarios.Where(u => u.codUsuario.Contains(codigo));
            //Filtro por nombre
            usuarios = usuarios.Where(u => u.nombre.Contains(nomUsuario));
            //Filtro por perfil
            usuarios = usuarios.Where(u => (perfil==null) || (perfil.id<=0) || (u.Perfil.id == perfil.id) );
            //Filtro por nombre y apellido
            usuarios = usuarios.Where(u => u.Empleado.nombre.Contains(nombres) && (u.Empleado.apMaterno.Contains(apellidos) || u.Empleado.apPaterno.Contains(apellidos))) ;
            //Filtro por estado
            usuarios = usuarios.Where(u => (estado == 0) || (u.estado == estado));

            return usuarios;

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

        /*******************************************************
                            PARA PERFILES
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
        /*******************************************************/
        public static IEnumerable<Perfil> obtenerPerfiles()
        {
            return listaPerfiles;
        }
    }
}
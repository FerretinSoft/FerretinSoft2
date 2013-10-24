﻿using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
                    _listaUsuarios = obtenerListaUsuarios();
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
            listaUsuarios = from p in db.Usuario
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
            db.Usuario.InsertOnSubmit(usuario);
            db.SubmitChanges();
        }
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
                   (u.dni != null && u.dni.Contains(usuario.dni)
                       && u.nombre != null && u.nombre.Contains(usuario.nombre)
                       && u.Empleado != null && 
                            ( u.Empleado.nombre != null && u.Empleado.nombre.Contains(empleado.nombre)
                            && u.Empleado.apPaterno != null && u.Empleado.apPaterno.Contains(empleado.apPaterno)
                            && u.Empleado.apMaterno != null && u.Empleado.apMaterno.Contains(empleado.apMaterno) 
                            )
                       && (usuario.id_perfil == 0 || ( u.id_perfil > 0 && u.id_perfil == usuario.id_perfil ) )
                       && (usuario.estado == null  || ( u.estado == null && u.estado == usuario.estado ) )
                    )
                   orderby u.nombre
                   select u;
        }

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

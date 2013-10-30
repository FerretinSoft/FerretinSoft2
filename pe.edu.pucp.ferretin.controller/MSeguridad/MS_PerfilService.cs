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
    public class MS_PerfilService : MS_ComunService
    {

        /*******************************************************
                                PARA PERFILES
        *******************************************************/
        public static IEnumerable<Perfil> _listaPerfiles = null;
        public static IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                if (_listaPerfiles == null)
                {
                    _listaPerfiles = db.Perfil;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaPerfiles);
                return _listaPerfiles;
            }
            set
            {
                _listaPerfiles = value;
            }
        }
        /*******************************************************/
        public static IEnumerable<Perfil> obtenerListaPerfiles()
        {
            listaPerfiles = from p in db.Perfil
                            orderby p.id
                            select p;
            return listaPerfiles;
        }
        /*******************************************************/
        public static IEnumerable<Perfil> obtenerListaPerfilesBy(Perfil perfil)
        {
            
            return from c in listaPerfiles
                   where
                   (//c.id != null && c.id.Contains(perfil.id)
                       c.nombre != null && c.nombre.Contains(perfil.nombre)
                       //&& c.contrasena != null && c.contrasena.Contains(perfil.contrasena)
                       /*&& c.id_perfil != null && c.id_perfil.Contains(usuario.id_perfil)
                       && c.estado != null && c.estado.Contains(usuario.estado)*/
                    )
                   orderby c.nombre
                   select c;            
           
        }
        /*******************************************************/
        public static bool insertarPerfil(Perfil perfil)
        {
            if (!db.Perfil.Contains(perfil))
            {
                db.Perfil.InsertOnSubmit(perfil);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }
        /*******************************************************/
        public static void actualizarPerfil(Perfil perfil)
        {
            db.SubmitChanges();
        }
        /*******************************************************/
        public static IEnumerable<Perfil> buscar(int perfil,int modulo)
        {
            return from p in listaPerfiles
                   where (
                       //Cada fila es un filtro
                   (perfil == 0 || (p.nombre != null && p.nombre.Equals(perfil))
                       //&& (modulo == 0 || (p.estado != null && u.estado.Equals(estado == 1 ? true : false)))
                    )
                    )
                   orderby p.nombre
                   select p;
        }
        /*******************************************************/
    }
}

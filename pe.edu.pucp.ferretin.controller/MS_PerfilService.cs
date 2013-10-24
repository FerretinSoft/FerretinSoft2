using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MS_PerfilService
    {

        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Perfil> _listaPerfiles = null;

        private static IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                if (_listaPerfiles == null)
                {
                    obtenerListaPerfiles();
                }
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
        public static void insertarPerfil(Perfil perfil)
        {
            db.Perfil.InsertOnSubmit(perfil);
            db.SubmitChanges();
        }
        /*******************************************************/
        public static void actualizarPerfil(Perfil perfil)
        {
            db.SubmitChanges();
        }


    }
}

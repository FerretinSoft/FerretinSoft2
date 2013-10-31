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
        
        /*******************************************************/
        private static IEnumerable<Perfil> _listaPerfilesCombo;
        private static IEnumerable<Perfil> listaPerfilesCombo
        {
            get
            {
                if (_listaPerfilesCombo == null)
                {
                    _listaPerfilesCombo = from p in db.Perfil
                                     select p;
                }
                return _listaPerfilesCombo;
            }
            set
            {
                _listaPerfilesCombo = value;
            }
        }
        /*******************************************************/
        public static IEnumerable<Perfil> obtenerPerfiles()
        {
            return listaPerfilesCombo;
        }
        /*******************************************************/
        public static string obtenerModulo(int valor)
        {
            string modulo = null;

            if (valor == 1) modulo = "Modulo Administrador";
            if (valor == 2) modulo = "Modulo Almacen";
            if (valor == 3) modulo = "Modulo Compras";
            if (valor == 4) modulo = "Modulo Recursos Humanos";
            if (valor == 5) modulo = "Modulo Ventas";
            if (valor == 6) modulo = "Modulo Seguridad";

            return modulo;
        }

        /*******************************************************/
        public static IEnumerable<Perfil> buscar(int modulo, string descripcion)
        {
            IEnumerable<Perfil> perfiles = listaPerfiles;
            //Filtro por modulo         
            perfiles = perfiles.Where(p => (modulo == 0) || (p.modulo.ToLower().Contains(obtenerModulo(modulo).ToLower().Trim())) );
            //Filtro por descripcion
            perfiles = perfiles.Where(p => p.descripcion.ToLower().Contains(descripcion.ToLower().Trim()));
            return perfiles;
        }
        /*
        return listaPerfiles.Where(p => p.descripcion.Contains(searchDescripcion)) ;
        /*******************************************************/
        public static Menu menuPadre
        {
            get
            {
                return db.Menu.Single(m => m.id_menu_padre == null || m.id_menu_padre <= 0);
            }
        }
        

    }
}

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

        public static IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                return db.Perfil;
            }
        }
     
        /*******************************************************/
        public static bool insertarPerfil(Perfil perfil)
        {
            if (perfil.nombre.Trim().Length <= 0) return false;
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
        public static string obtenerModulo(int valor)
        {
            string modulo = null;

            if (valor == 1) modulo = "Modulo Almacen";
            if (valor == 2) modulo = "Modulo Compras";
            if (valor == 3) modulo =  "Modulo Recursos Humanos";
            if (valor == 4) modulo =  "Modulo Ventas";
            if (valor == 5) modulo =  "Modulo Administrador";            

            return modulo;
        }

        /*******************************************************/
        public static IEnumerable<Perfil> buscar(int modulo, string descripcion)
        {            
            //Falta Filtrar por descripcion
            //perfiles = perfiles.Where(p => p.descripcion.ToLower().Contains(descripcion.ToLower().Trim()));

            IEnumerable<Perfil> perfiles=listaPerfiles;
            //Filtrar por modulo
            perfiles = perfiles.Where(p => (modulo == 0) || (p.modulo.ToLower().Contains(obtenerModulo(modulo).ToLower().Trim())));
            // Filtrar por descripcion
            perfiles = perfiles.Where(p => p.descripcion.ToLower().Contains(descripcion.ToLower().Trim()));

            return perfiles;
        }
        
        public static Menu menuPadre
        {
            get
            {
                return db.Menu.Single(m => m.id_menu_padre == null || m.id_menu_padre <= 0);
            }
        }        
        

    }
}

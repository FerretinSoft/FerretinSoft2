using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_TransaccionService : MS_ComunService
    {
        public static IEnumerable<Transaccion> _listaTransacciones = null;
        public static IEnumerable<Transaccion> listaTransacciones
        {
            get
            {
                if (_listaTransacciones == null)
                {
                    _listaTransacciones = db.Transaccion;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaTransacciones);
                return _listaTransacciones;
            }
            set
            {
                _listaTransacciones = value;
            }
        }


        /*******************************************************/
        public static IEnumerable<Transaccion> buscar(string nomUsuario, Perfil perfil, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            Usuario user;
            int id = 0;
            if (nomUsuario != null)
            {
                try
                {
                    user = db.Usuario.Single(u => u.nombre.Contains(nomUsuario));
                    if (user != null) id = user.id;
                }
                catch (Exception e)
                {
                    
                }
            }
            
            //return from t in listaTransacciones
            //       where                   
            //       (id==0 || (t.id_usuario != 0 && t.id_usuario==id))
            //       && ()
            //       orderby t.fecha
            //       select t;       
     
            IEnumerable<Transaccion> transacciones = listaTransacciones;
            
            //Filtro por nombre
            transacciones = transacciones.Where(t => ((id==0) || (t.id_usuario==id)) );
            //Filtro por perfil
            transacciones = transacciones.Where(t => (perfil==null) || (perfil.id<=0) || (t.Usuario.Perfil.id == perfil.id) );
            //Filtro por fechaInicio
            transacciones = transacciones.Where(t => (fechaDesde == null || (t.fecha != null && t.fecha >= fechaDesde)) );
            //Filtro por fechaFin
            transacciones = transacciones.Where(t => (fechaHasta == null || (t.fecha != null && t.fecha <= fechaHasta)) );

            return transacciones;
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


    }
}

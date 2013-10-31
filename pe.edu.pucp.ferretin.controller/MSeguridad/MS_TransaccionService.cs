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
        public static IEnumerable<Transaccion> buscar(string nomUsuario, Perfil perfil)
        {                        
            return from t in listaTransacciones
                   where
                   (t.nroIP != null)
                   orderby t.nroIP
                   select t;            
        }


    }
}

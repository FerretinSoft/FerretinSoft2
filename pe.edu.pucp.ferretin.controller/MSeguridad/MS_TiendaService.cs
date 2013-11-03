using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_TiendaService : MS_ComunService
    {

        public static IEnumerable<Tienda> _listaTiendas;
        public static IEnumerable<Tienda> listaTiendas
        {
            get
            {
                if (_listaTiendas == null)
                {
                    _listaTiendas = db.Tienda;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaTiendas);
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
            }
        }


        public static Tienda obtenerTiendaByCodigo(String codigoTienda)
        {
            IEnumerable<Tienda> tiendas = (from t in listaTiendas
                                             where t.codigo != null && t.codigo.Contains(codigoTienda)
                                             select t);
            if (tiendas.Count() > 0)
                return tiendas.First();
            else
                return null;
        }
        

        public static void actualizarTienda(Tienda tienda)
        {            
            db.SubmitChanges();
        }

        public static IEnumerable<Tienda> buscar(string codTienda, string nombre, UbigeoDistrito distrito, int tipo, int estado)
        {
            return from t in listaTiendas
                   where (
                       //Cada fila es un filtro
                          (t.codigo != null && t.codigo.ToLower().Contains(codTienda.ToLower().Trim()))
                       && ((t.nombre != null && t.nombre.ToLower().Contains(nombre.ToLower().Trim())))
                       && (tipo == 0 || (t.tipo != null && t.tipo==tipo-1))
                       && (estado == 0 || (t.estado != null && t.estado == estado-1))
                       && (distrito == null || (t.UbigeoDistrito.id != null && t.UbigeoDistrito.id == distrito.id ))
                    )
                   orderby t.codigo
                   select t;
            /*
             * Alternativa al código anterior
            return listaAlmacenes
                .Where(t=>t.codigo!=null && t.codigo.ToLower().Contains(codTienda.ToLower().Trim()))
                .Where(t=>(t.nombre != null && t.nombre.ToLower().Contains(nombre.ToLower().Trim())))
                .Where(t=>(tipo == 0 || (t.tipo != null && t.tipo.Equals(tipo == 1 ? true : false))))
                .Where(t=>(estado == 0 || (t.estado != null && t.estado.Equals(estado == 1 ? true : false))))
                .Where(t=>(distrito == null || (t.UbigeoDistrito.id != null && t.UbigeoDistrito.id == distrito.id )));
            */
                   
        }

        public static bool insertarAlmacen(Tienda tienda)
        {
            if (db.Tienda.Single(t=> t.codigo == tienda.codigo)== null)
            {                
                db.Tienda.InsertOnSubmit(tienda);
                return enviarCambios();
            }
            else
            {                
                return false;
            }
        }

        
    }
}

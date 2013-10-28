using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_TiendaService : MS_ComunService
    {

        public static IEnumerable<Almacen> _listaAlmacenes;
        public static IEnumerable<Almacen> listaAlmacenes
        {
            get
            {
                if (_listaAlmacenes == null)
                {
                    _listaAlmacenes = db.Almacen;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaAlmacenes);
                return _listaAlmacenes;
            }
            set
            {
                _listaAlmacenes = value;
            }
        }


        public static Almacen obtenerTiendaByCodigo(String codigoTienda)
        {
            IEnumerable<Almacen> tiendas = (from t in listaAlmacenes
                                             where t.codigo != null && t.codigo.Contains(codigoTienda)
                                             select t);
            if (tiendas.Count() > 0)
                return tiendas.First();
            else
                return null;
        }


        public static void insertarTienda(Almacen tienda)
        {
            db.Almacen.InsertOnSubmit(tienda);
            db.SubmitChanges();
        }

        public static void actualizarTienda(Almacen tienda)
        {
            db.SubmitChanges();
        }

        public static IEnumerable<Almacen> buscar(string codTienda, string nombre, UbigeoDistrito distrito, int tipo, int estado)
        {
            return from t in listaAlmacenes
                   where (
                       //Cada fila es un filtro
                          (t.codigo != null && t.codigo.ToLower().Contains(codTienda.ToLower().Trim()))
                       && ((t.nombre != null && t.nombre.ToLower().Contains(nombre.ToLower().Trim())))
                       && (tipo == 0 || (t.tipo != null && t.tipo.Equals(tipo == 1 ? true : false)))
                       && (estado == 0 || (t.estado != null && t.estado.Equals(estado == 1 ? true : false)))
                       && (distrito == null || (t.UbigeoDistrito.id != null && t.UbigeoDistrito.id == distrito.id ))
                    )
                   orderby t.codigo
                   select t;
        }

        public static bool insertarAlmacen(Almacen almacen)
        {
            if (!db.Almacen.Contains(almacen))
            {
                db.Almacen.InsertOnSubmit(almacen);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

        
    }
}

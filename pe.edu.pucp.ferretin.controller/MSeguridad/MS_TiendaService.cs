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
                //db.Refresh(RefreshMode.OverwriteCurrentValues, _listaTiendas);
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
            }
        }

        public static IEnumerable<Tienda> obtenerTiendas()
        {
            IEnumerable<Tienda> tiendas = from t in db.Tienda
                                          select t;

            return tiendas;
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
           
                   
        }

        public static bool insertarAlmacen(Tienda tienda)
        {
            Tienda tiend;
            try
            {
                try
                {
                    tiend = db.Tienda.Single(t => t.codigo == tienda.codigo);
                    return false;
                }
                catch (Exception e)
                {
                    //Obtiene el ultimo codigo de la tienda y le suma 1 para que se el codigo de la tienda a agregar
                    tiend = db.Tienda.OrderByDescending(t => t.id).First();
                    String tempCadenaNumero = (Int32.Parse(tiend.codigo.Substring(tiend.codigo.Length - 3)) + 1).ToString();
                    if (tempCadenaNumero.Length == 1)
                        tienda.codigo = "TND00" + tempCadenaNumero;
                    else if (tempCadenaNumero.Length == 2)
                        tienda.codigo = "TND0" + tempCadenaNumero;
                    else
                        tienda.codigo = "TND" + tempCadenaNumero;
                    //*************************************************//
                    db.Tienda.InsertOnSubmit(tienda);
                    return enviarCambios();
                }                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MS_TiendaService
    {

        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Tienda> _listaTiendas = null;

        private static IEnumerable<Tienda> listaTiendas
        {
            get
            {
                if (_listaTiendas == null)
                {
                    obtenerListaTiendas();
                }
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
            }
        }

        public static IEnumerable<Tienda> obtenerListaTiendas()
        {
            listaTiendas = from p in db.Tiendas
                            orderby p.id
                            select p;
            return listaTiendas;
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

        public static IEnumerable<Tienda> obtenerListaTiendasBy(Tienda tienda)
        {
            return from t in listaTiendas
                   where
                   (t.codigo != null && t.codigo.Contains(tienda.codigo)
                       && t.nombre != null && t.nombre.Contains(tienda.nombre)
                       && t.tipo != null && t.tipo.Equals(tienda.tipo)
                       && t.estado != null && t.estado.Equals(tienda.estado)
                       && t.UbigeoDistrito != null && t.UbigeoDistrito.Equals(tienda.UbigeoDistrito)
                    )
                   orderby t.codigo
                   select t;
        }

        public static void insertarTienda(Tienda tienda)
        {
            db.Tiendas.InsertOnSubmit(tienda);
            db.SubmitChanges();
        }

        public static void actualizarTienda(Tienda tienda)
        {
            db.SubmitChanges();
        }
    }
}

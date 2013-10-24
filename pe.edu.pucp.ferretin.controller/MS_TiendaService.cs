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

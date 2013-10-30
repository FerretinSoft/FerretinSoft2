using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_VentaService : MV_ComunService
    {

        #region Private Zone
        #endregion

        #region Public Zone
        private static IEnumerable<Venta> _listaVentas;
        public static IEnumerable<Venta> listaVentas
        {
            get
            {
                if (_listaVentas == null)
                {
                    _listaVentas = db.Venta;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaVentas);
                return _listaVentas;
            }
            set
            {
                _listaVentas = value;
            }
        }

        public static Venta obtenerVentaByCodVenta(String nroDocumento)
        {
            Venta venta = (from c in listaVentas
                           where c.nroDocumento != null && c.nroDocumento.Equals(nroDocumento)
                               select c).First();
            return venta;
        }

        public static IEnumerable<Venta> buscarVentas(string nroDocumento)
        {
            return from c in listaVentas
                   where
                   (c.nroDocumento != null && c.nroDocumento.Contains(nroDocumento)
                    )
                   orderby c.nroDocumento
                   select c;
        }
        
        #endregion

    }

}

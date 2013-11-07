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

        private static IEnumerable<VentaProducto> _listaProductos;
        public static IEnumerable<VentaProducto> listaProductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    _listaProductos = db.VentaProducto;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductos);
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
            }
        }

        private static IEnumerable<VentaMedioPago> _listaMedioPago;
        public static IEnumerable<VentaMedioPago> listaMedioPago
        {
            get
            {
                if (_listaMedioPago == null)
                    _listaMedioPago = db.VentaMedioPago;
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaMedioPago);
                return _listaMedioPago;
            }
            set
            {
                _listaMedioPago = value;
            }
        }

        public static Venta obtenerVentaByCodVenta(String nroDocumento)
        {
            if (nroDocumento != "")
            {
                Venta venta = (from c in listaVentas
                               where c.nroDocumento != null && c.nroDocumento.Equals(nroDocumento)
                               select c).First();
                return venta;
            }
            else
                return null;
        }

        public static IEnumerable<Venta> buscarVentas(string nroDocumento, string nroDocCliente, DateTime fechaInicio, DateTime fechaFin, string searchVendedor)
        {
            return from c in listaVentas
                   where
                   (c.nroDocumento != null && c.nroDocumento.Contains(nroDocumento)
                   && c.Cliente.nroDoc != null && c.Cliente.nroDoc.Contains(nroDocCliente)
                   && c.fecha != null && c.fecha >= fechaInicio
                   && c.fecha != null && c.fecha <= fechaFin
                   && c.Usuario.Empleado.dni != null && c.Usuario.Empleado.dni.Contains(searchVendedor)

                    )
                   orderby c.nroDocumento
                   select c;
        }

        public static IEnumerable<VentaProducto> obtenerProductosbyIdVenta(long id_venta)
        {
            return from c in listaProductos
                   where
                   (c.id_venta != null && c.id_venta.Equals(id_venta)
                    )
                   orderby c.id_venta
                   select c;
        }
        public static IEnumerable<VentaProducto> obtenerProductosSinPuntosbyIdVenta(long id_venta)
        {
            return from c in listaProductos
                   where
                   (c.id_venta != null && c.id_venta.Equals(id_venta)
                   && c.canjeado != true
                    )
                   orderby c.id_venta
                   select c;
        }

        

        public static IEnumerable<VentaMedioPago> obtenerMedioDePagobyIdVenta(long id_venta)
        {
            return from c in listaMedioPago
                   where
                   (c.id_venta != null && c.id_venta.Equals(id_venta)
                   )
                   orderby c.id_venta
                   select c;
        }
        public static VentaProducto obtenerVentaProductobyId(long id)
        {
            if (id >= 1)
            {
                VentaProducto prodSelec = (from c in listaProductos
                                   where  c.id.Equals(id)
                                   select c).First();
                return prodSelec;
            }
            else
                return null;
        }
        

        #endregion




        public static string generarNroDoc(bool isFactura)
        {
            
            Int64 numCodProf = listaVentas.Max(p => p.id) + 1;
            string codDev = Convert.ToString(numCodProf);
            while (true)
            {
                if (codDev.Length == 8)
                    break;
                else
                    codDev = "0" + codDev;
            }
            return (isFactura?"F-":"B") + codDev + "-" + DateTime.Now.Year.ToString();
            
        }
    }

}

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
        public static FerretinDataContext dbVenta
        {
            get
            {
                return db;
            }
        }

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
                    _listaVentas = dbVenta.Venta;
                }
                //dbVenta.Refresh(RefreshMode.OverwriteCurrentValues, _listaVentas);
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
                    _listaProductos = dbVenta.VentaProducto;
                }
                //dbVenta.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductos);
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
                    _listaMedioPago = dbVenta.VentaMedioPago;
                //dbVenta.Refresh(RefreshMode.OverwriteCurrentValues, _listaMedioPago);
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

        public static IEnumerable<Venta> buscarVentas(string nroDocumento, long? nroDocCliente, DateTime fechaInicio, DateTime fechaFin, string searchVendedor)
        {

            IEnumerable<Venta> ventas = listaVentas;


            ventas = ventas.Where(t => (((nroDocumento == "") || (t.nroDocumento.Contains(nroDocumento)))));
         
            ventas = ventas.Where(t => (nroDocCliente == null) || (t.Cliente != null && t.Cliente.nroDoc == nroDocCliente));

            ventas = ventas.Where(t => (searchVendedor == "") || (t.Usuario.Empleado != null && t.Usuario.Empleado.dni == searchVendedor));

            ventas = ventas.Where(t => (((t.estado == 1))));

            ventas = ventas.Where(t => (fechaInicio.Equals(DateTime.Parse("10/09/2013")) || (t.fecha != null && t.fecha >= fechaInicio)));
         
            ventas = ventas.Where(t => (fechaFin.Equals(DateTime.Today) || (t.fecha != null && t.fecha <= fechaFin)));

            return ventas;

            
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
                    && c.id_servicio == null
                   
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

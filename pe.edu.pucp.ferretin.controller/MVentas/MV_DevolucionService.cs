using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_DevolucionService : MV_ComunService
    {
        #region Private Zone
        #endregion

        #region Public Zone
        private static IEnumerable<Devolucion> _listaDevoluciones;
        public static IEnumerable<Devolucion> listaDevoluciones
        {
            get
            {
                if (_listaDevoluciones == null)
                {
                    _listaDevoluciones = db.Devolucion;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaDevoluciones);
                return _listaDevoluciones;
            }
            set
            {
                _listaDevoluciones = value;
            }
        }


        private static IEnumerable<NotaCredito> _listaNotasCredito;
        public static IEnumerable<NotaCredito> listaNotasCredito
        {
            get
            {
                if (_listaNotasCredito == null)
                {
                    _listaNotasCredito = db.NotaCredito;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaNotasCredito);
                return _listaNotasCredito;
            }
            set
            {
                _listaNotasCredito = value;
            }
        }
        private static IEnumerable<DevolucionProducto> _listaProductos;
        public static IEnumerable<DevolucionProducto> listaProductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    _listaProductos = db.DevolucionProducto;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductos);
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
            }
        }

        public static IEnumerable<Devolucion> buscarDevoluciones(string nroDevolucion, string nroVenta, string nroDocCliente, DateTime fechaInicio, DateTime fechaFin, string searchVendedor)
        {
            return from c in listaDevoluciones
                   where
                   (c.codigo != null && c.codigo.Contains(nroDevolucion)
                   && c.Venta.nroDocumento != null && c.Venta.nroDocumento.Contains(nroVenta)
                   && c.fecEmision != null && c.fecEmision >= fechaInicio
                   && c.fecEmision != null && c.fecEmision <= fechaFin
                   && c.Venta.Usuario.Empleado.dni != null && c.Venta.Usuario.Empleado.dni.Contains(searchVendedor)
                   && c.Venta.Cliente.nroDoc != null && c.Venta.Cliente.nroDoc.Contains(nroDocCliente)
                    )
                   orderby c.codigo
                   select c;
        }

        public static IEnumerable<DevolucionProducto> obtenerProductosbyIdDevolucion(long id_devolucion)
        {
            return from c in listaProductos
                   where
                   (c.id_devolucion != null && c.id_devolucion.Equals(id_devolucion)
                    )
                   orderby c.id_devolucion
                   select c;
        }

        public static string obtenerCodDevolucion()
        {
            string codDev = (from c in listaDevoluciones
                               select c.codigo).Max();
            long numDev = Convert.ToInt64(codDev) + 1;
            codDev = Convert.ToString(numDev);       
            return codDev;
        }

        public static NotaCredito obtenerNotaCredbyIdDevolucion(long id_devolucion)
        {
           NotaCredito notaCredito = (from c in listaNotasCredito
                               where c.id_devolucion.Equals(id_devolucion)
                               select c).Single();
            
            return notaCredito;
        }

        public static bool insertarDevolucion(Devolucion devolucion)
        {
            if (!db.Devolucion.Contains(devolucion))
            {
                db.Devolucion.InsertOnSubmit(devolucion);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }
        
        #endregion

    }
}

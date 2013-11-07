using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_NotaCreditoService : MV_ComunService
    {
        #region Private Zone
       
        #endregion

        #region Public Zone
        public  int prueba;
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



        public static IEnumerable<NotaCredito> buscarNotaCredito(string nroNotaCredito, string nroDocCliente, DateTime fechaInicio, DateTime fechaFin, string searchVendedor)
        {
            return from c in listaNotasCredito
                   where
                   (c.codigo != null && c.codigo.Contains(nroNotaCredito)
                   && c.Devolucion.Venta.Cliente.nroDoc != null && c.Devolucion.Venta.Cliente.nroDoc.Contains(nroDocCliente)
                   && c.Devolucion.fecEmision != null && c.Devolucion.fecEmision >= fechaInicio
                   && c.Devolucion.fecEmision != null && c.Devolucion.fecEmision <= fechaFin
                   && c.Devolucion.Venta.Usuario.Empleado.dni != null && c.Devolucion.Venta.Usuario.Empleado.dni.Contains(searchVendedor)

                    )
                   orderby c.codigo
                   select c;
        }

        public static NotaCredito obtenerNotaCreditoById(long id)
        {
            NotaCredito notaCredito = (from c in listaNotasCredito
                               where c.id.Equals(id)
                               select c).Single();

            return notaCredito;
        }

        public static bool insertarNotaCredito(NotaCredito notaCredito)
        {
            if (!db.NotaCredito.Contains(notaCredito))
            {
                db.NotaCredito.InsertOnSubmit(notaCredito);
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

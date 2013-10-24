using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller
{
    public static class MV_ClienteService
    {
        private static FerretinDataContext db = new FerretinDataContext();

        private static IEnumerable<Cliente> _listaClientes = null;

        private static IEnumerable<Cliente> listaClientes
        {
            get
            {
                if (_listaClientes == null)
                {
                    obtenerListaClientes();
                }
                return _listaClientes;
            }
            set
            {
                _listaClientes = value;
            }
        }

        public static IEnumerable<Cliente> obtenerListaClientes()
        {
            listaClientes = from p in db.Cliente
                            orderby p.nroDoc
                            select p;
            return listaClientes;
        }

        public static Cliente obtenerClienteByNroDoc(String nroDoc)
        {
            IEnumerable<Cliente> clientes = (from c in listaClientes
                                             where c.nroDoc != null && c.nroDoc.Equals(nroDoc)
                                             select c);
            if (clientes.Count() > 0)
                return clientes.First();
            else
                return null;
        }

        public static IEnumerable<Cliente> obtenerListaClientesBy(Cliente cliente)
        {
            return from c in listaClientes
                   where
                   (c.nroDoc != null && c.nroDoc.Contains(cliente.nroDoc)
                       && c.nombre != null && c.nombre.Contains(cliente.nombre)
                       && c.apPaterno != null && c.apPaterno.Contains(cliente.apPaterno)
                       && c.apMaterno != null && c.apMaterno.Contains(cliente.apMaterno)
                       && c.tipoDocumento != null && c.tipoDocumento.Contains(cliente.tipoDocumento)
                    )
                   orderby c.nroDoc
                   select c;
        }

        public static void insertarCliente(Cliente cliente)
        {
            db.Cliente.InsertOnSubmit(cliente);
            db.SubmitChanges();
        }

        public static void actualizarCliente(Cliente cliente)
        {
            db.SubmitChanges();
        }
    }
}

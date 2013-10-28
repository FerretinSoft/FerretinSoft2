using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_ClienteService : MV_ComunService
    {

        #region Private Zone
        #endregion

        #region Public Zone

        ///<summary>
        ///Variable privada que almacena la lista de clientes en memoria, para su posterior uso
        ///</summary>
        ///<remarks>
        ///Todas las operaciones se realizan en base a esta lista
        ///</remarks>
        private static IEnumerable<Cliente> _listaClientes;
        public static IEnumerable<Cliente> listaClientes
        {
            get
            {
                if (_listaClientes == null)
                {
                    _listaClientes = db.Cliente;
                }
                //Usando concurrencia pesimista:
                ///La lista de clientes se actualizara para ver los cambios
                ///Si quisiera usar concurrencia optimista quito la siguiente linea
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaClientes);
                return _listaClientes;
            }
            set
            {
                _listaClientes = value;
            }
        }

        ///<summary>
        ///Busca un Cliente por el Número de Documento
        ///</summary>
        ///<return>
        ///Devuelve un Cliente que coincida el Número de Documento 
        ///</return>
        ///<param name="nroDoc">
        ///El Número de Documento que se usará para la busqueda
        ///</param>
        public static Cliente obtenerClienteByNroDoc(String nroDoc)
        {
            Cliente cliente = (from c in listaClientes
                                where c.nroDoc != null && c.nroDoc.Equals(nroDoc)
                                select c).First();
            return cliente;
        }

        ///<summary>
        ///Busca un Cliente por el Número de Documento
        ///</summary>
        ///<return>
        ///Devuelve un Cliente que coincida el ID registrado en la Base de Datos 
        ///</return>
        ///<param name="id">
        ///El Identificador que se usará para la busqueda
        ///</param>
        public static Cliente obtenerClienteById(int id)
        {
            Cliente cliente = (from c in listaClientes
                               where c.id.Equals(id)
                               select c).Single();
            
            return cliente;
        }
        /// <summary>
        /// Guarda un nuevo Cliente en la Base de Datos
        /// </summary>
        /// <param name="cliente">el Cliente a guardar</param>
        public static bool insertarCliente(Cliente cliente)
        {
            if (!db.Cliente.Contains(cliente))
            {
                db.Cliente.InsertOnSubmit(cliente);
                return enviarCambios();
            }else{
                return false;
            }
        }

        public static IEnumerable<Cliente> buscarClientes(string nroDoc, string nombre, string apPaterno, string apMaterno, string tipoDocumento)
        {
            return from c in listaClientes
                   where
                   (c.nroDoc != null && c.nroDoc.Contains(nroDoc)
                       && c.nombre != null && c.nombre.Contains(nombre)
                       && c.apPaterno != null && c.apPaterno.Contains(apPaterno)
                       && c.apMaterno != null && c.apMaterno.Contains(apMaterno)
                       && c.tipoDocumento != null && c.tipoDocumento.Contains(tipoDocumento)
                    )
                   orderby c.nroDoc
                   select c;
        }

        #endregion

    }
}

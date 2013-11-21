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
        public static FerretinDataContext dbCliente
        {
            get
            {                
                return db;
            }
        }

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
                    _listaClientes = from c in dbCliente.Cliente where c.nroDoc>0  select c;
                }
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
        public static Cliente obtenerClienteByNroDoc(long? nroDoc)
        {
            if (nroDoc > 0)
            {
                Cliente cliente = (from c in listaClientes
                                   where c.nroDoc != null && c.nroDoc.Equals(nroDoc)
                                   select c).First();
                return cliente;
            }
            else
                return null;
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
            if (!dbCliente.Cliente.Contains(cliente))
            {
                //dbCliente.Cliente.InsertOnSubmit(cliente);
                return enviarCambios(dbCliente);
            }else{
                return false;
            }
        }

        public static IEnumerable<Cliente> buscarClientes(long? nroDoc, string nombre, string apPaterno, string apMaterno, string tipoDocumento)
        {
            return from c in listaClientes
                   where
                   ( (c.nroDoc == null || nroDoc == null ||  nroDoc<=0 || c.nroDoc.ToString().Contains(nroDoc.ToString() ) )
                       && ( c.nombre == null || c.nombre.ToLower().Contains(nombre.ToLower().Trim()) )
                       && ( c.apPaterno == null || c.apPaterno.ToLower().Contains(apPaterno.ToLower().Trim()) )
                       && ( c.apMaterno == null || c.apMaterno.ToLower().Contains(apMaterno.ToLower().Trim()) )
                       && ( c.tipoDocumento == null || c.tipoDocumento.Contains(tipoDocumento) )
                    )
                   orderby c.nroDoc
                   select c;
        }

        #endregion

    }
}

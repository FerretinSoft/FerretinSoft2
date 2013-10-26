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
        private static IEnumerable<Cliente> getClientesBD()
        {
            return from p in db.Cliente
                   orderby p.nroDoc
                   select p;
        }
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
                    _listaClientes = getClientesBD();
                }
                return _listaClientes;
            }
            set
            {
                _listaClientes = value;
            }
        }

        ///<summary>
        ///Realiza una consulta sobre los clientes
        ///</summary>
        ///<return>
        ///Devuelve una lista de Clientes, si aún no se ha solicitado, actualiza los datos
        ///</return>
        ///<param name="realdata">
        ///Si es True, se obtendrán nuevos Datos de la BD, si es False, se obtendrán datos en caché
        ///</param>
        public static IEnumerable<Cliente> obtenerListaClientes(Boolean realdata)
        {
            if(realdata){
                listaClientes = getClientesBD();
            }
            return listaClientes;
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

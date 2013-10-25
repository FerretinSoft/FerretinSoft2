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
        
        ///<summary>
        ///Variable privada que almacena la lista de clientes en memoria, para su posterior uso
        ///</summary>
        ///<remarks>
        ///Todas las operaciones se realizan en base a esta lista
        ///</remarks>
        private static IEnumerable<Cliente> listaClientes;

        ///<summary>
        ///Realiza una consulta sobre los clientes
        ///</summary>
        ///<return>
        ///Devuelve una lista de Clientes, si aún no se ha solicitado, actualiza los datos
        ///</return>
        public static IEnumerable<Cliente> obtenerListaClientes()
        {
            return obtenerListaClientes(false);
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
            if(realdata || listaClientes == null){
                listaClientes = from p in db.Cliente
                            orderby p.nroDoc
                            select p;
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
                                select c).Single();
            return cliente;
        }

        /// <summary>
        /// Busca Clientes por Numero de Documento, Nombre, Apellido Panterno, Apellido Materno, Tipo de Documento
        /// </summary>
        /// <param name="cliente">El Objeto Cliente que contiene los parámetros de Búsqueda/// </param>
        /// <returns>Una lista de Clientes con los resultados encontrados</returns>
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

        /// <summary>
        /// Guarda un nuevo Cliente en la Base de Datos
        /// </summary>
        /// <param name="cliente">el Cliente a guardar</param>
        public static void insertarCliente(Cliente cliente)
        {
            if (!db.Cliente.Contains(cliente))
            {
                db.Cliente.InsertOnSubmit(cliente);
                enviarCambios();
            }
        }

        
    }
}

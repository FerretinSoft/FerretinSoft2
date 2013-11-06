using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MCompras
{
   public class MC_ProveedorService:MC_ComunService
    {
        #region Public Zone

        ///<summary>
        ///Variable privada que almacena la lista de clientes en memoria, para su posterior uso
        ///</summary>
        ///<remarks>
        ///Todas las operaciones se realizan en base a esta lista
        ///</remarks>
        private static IEnumerable<Proveedor> _listaProveedores;
        public static IEnumerable<Proveedor> listaProveedores
        {
            get
            {
                if (_listaProveedores == null)
                {
                    _listaProveedores = db.Proveedor;
                }
            
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProveedores);
                return _listaProveedores;
            }
            set
            {
                _listaProveedores = value;
            }
        }

      

        
        /// <summary>
        /// Guarda un nuevo Proveedor en la Base de Datos
        /// </summary>
        /// <param name="cliente">el Proveedor a guardar</param>
        public static bool insertarProveedor(Proveedor proveedor)
        {
            if (!db.Proveedor.Contains(proveedor))
            {
                db.Proveedor.InsertOnSubmit(proveedor);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }

      // public static bool 

        public static IEnumerable<Proveedor> buscarProveedores(string ruc, string razonSoc, Rubro rubro)
        {
            return from p in listaProveedores
                   where
                   (p.razonSoc != null && p.razonSoc.Contains(razonSoc)
                       && p.ruc != null && p.ruc.Contains(ruc)&&
                      
                        (rubro==null|| rubro.id==0 ||( p.id_rubro!=null && p.id_rubro == rubro.id))
                  
                    )
                   orderby p.razonSoc
                   select p;
        }

        #endregion
    }
}

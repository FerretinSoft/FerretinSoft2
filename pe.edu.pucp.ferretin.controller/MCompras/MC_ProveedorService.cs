using pe.edu.pucp.ferretin.model;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;


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

        private static IEnumerable<ProveedorProducto> _listaProductos;
        public static IEnumerable<ProveedorProducto> listaProductos
        {
            get
            {
                if (_listaProductos == null)
                {
                    _listaProductos = db.ProveedorProducto;
                }
                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaProductos);
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
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

        public static IEnumerable<Proveedor> buscarProveedores(string ruc, string razonSoc, Rubro rubro,string tipo)
        {
            return from p in listaProveedores
                   where
                   (p.razonSoc != null && p.razonSoc.ToLower().Contains(razonSoc.ToLower().Trim())
                       && p.ruc != null && p.ruc.Contains(ruc)&&
                       p.tipo!=null && p.tipo.Contains(tipo)&&
                      
                        (rubro==null|| rubro.id==0 ||( p.id_rubro!=null && p.id_rubro == rubro.id))
                  
                    )
                   orderby p.razonSoc
                   select p;
        }

       //buscarProveedorByName

        public static Proveedor buscarProveedorByName(string razonSoc)
        {
            IEnumerable<Proveedor> proveedores = (from p in listaProveedores
                                                  where
                                                  (p.razonSoc != null && p.razonSoc.Contains(razonSoc))
                                                  orderby p.razonSoc
                                                  select p);
            if (proveedores.Count() > 0)
                return proveedores.First();
            else
                return null;
        }

        public static IEnumerable<ProveedorProducto> obtenerProductosbyIdProveedor(int id_proveedor)
        {
            return from c in listaProductos
                   where
                   (c.id_proveedor != null && c.id_proveedor.Equals(id_proveedor)
                    )
                   orderby c.id_proveedor
                   select c;
        }

        public static IEnumerable<ProveedorProducto> obtenerProductosProveedor(string proveedor, string producto)
        {
            return from c in listaProductos
                   where
                   (c.id_proveedor != null && c.Proveedor.razonSoc.ToLower().Trim().Contains(proveedor.ToLower().Trim())
                   && (c.Producto.nombre.ToLower().Trim().Contains(producto.ToLower().Trim())) 
                   )
                   orderby c.Producto.nombre
                   select c;
        }

        public static bool InsertarProducto(ProveedorProducto provprod)
        {
            if (!db.ProveedorProducto.Contains(provprod))
            {
                db.ProveedorProducto.InsertOnSubmit(provprod);
                
                return false;
            }
            else
            {
                return false;
            }
        }

        public static IEnumerable<Proveedor> obtenerPosiblesProveedores(Producto producto)
        {
            IEnumerable<ProveedorProducto> productos = (from p in listaProductos
                                                  where
                                                  (p.Producto != null && p.Producto.id == producto.id)                                                
                                                  select p);

            IEnumerable<Proveedor> proveedores = (from p in productos
                                                  select p.Proveedor).Take(4);

            return proveedores;
        }

        #endregion
    }
}

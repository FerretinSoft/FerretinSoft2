using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_ProveedoresViewModel:ViewModelBase
    {
        #region Constructor
        public MC_ProveedoresViewModel()
        {
            _proveedor = new Proveedor();
        }
        #endregion

        public IEnumerable<Proveedor> listaProveedores
        {
            get
            {
                return MC_ProveedorService.listaProveedores;
            }

        }

        #region lista de Proveedores
        private Proveedor _proveedor;
        public Proveedor proveedor
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
                NotifyPropertyChanged("proveedor");
            }
        }


        #endregion
    }
}

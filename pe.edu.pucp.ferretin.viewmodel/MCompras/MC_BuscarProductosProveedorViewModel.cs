using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_BuscarProductosProveedorViewModel : ViewModelBase
    {
        #region Constructor
        public MC_BuscarProductosProveedorViewModel()
        {
            _productoProveedor = new ProveedorProducto();  
        }
        #endregion

        #region Valores para los controles de la ventana

        public string _searchProveedor = "";
        public string searchProveedor { get { return _searchProveedor; } set { _searchProveedor = value; NotifyPropertyChanged("searchProveedor"); } }

        public string _searchProducto = "";
        public string searchProducto { get { return _searchProducto; } set { _searchProducto = value; NotifyPropertyChanged("searchProducto"); } }

        #endregion

        #region Lista de Productos del Proveedor y otras listas

        private ProveedorProducto _productoProveedor;
        public ProveedorProducto productoProveedor
        {
            get
            {
                return _productoProveedor;
            }
            set
            {
                _productoProveedor = value;
                NotifyPropertyChanged("productoProveedor");
            }
        }

        private IEnumerable<ProveedorProducto> _listaProductosProveedor = null;
        public IEnumerable<ProveedorProducto> listaProductosProveedor
        {
            get
            {
                if (searchProducto != "" || searchProveedor != "")
                {
                    if (searchProveedor == "")
                    {
                        MessageBox.Show("Debe ingresar el Proveedor");
                    }
                    else
                        _listaProductosProveedor = MC_ProveedorService.obtenerProductosProveedor(searchProveedor, searchProducto);
                }
                if (searchProveedor == "" && searchProducto == "")
                    _listaProductosProveedor = null;
                
                return _listaProductosProveedor;
            }
            set
            {
                _listaProductosProveedor = value;
                NotifyPropertyChanged("listaProductosProveedor");
            }
        }

        #endregion

        #region Relay Commands

        RelayCommand _actualizarListaProductosProveedorCommand;
        public ICommand actualizarListaProductosProveedorCommand
        {
            get
            {
                if (_actualizarListaProductosProveedorCommand == null)
                {
                    _actualizarListaProductosProveedorCommand = new RelayCommand(param => NotifyPropertyChanged("listaProductosProveedor"));
                }
                return _actualizarListaProductosProveedorCommand;
            }
        }

        #endregion 
    }
}

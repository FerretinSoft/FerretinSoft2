using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_AdministrarPrecioProdViewModel : ViewModelBase
    {
        #region Constructor
        public MV_AdministrarPrecioProdViewModel()
        {

            _productoPrecio = new ProductoPrecio();
            _productoPrecio.fechaRegistro = null;
            
        }
        #endregion

        #region Datos de Búscadores
        public String _searchProducto = "";
        public String searchProducto { get { return _searchProducto; } set { _searchProducto = value; NotifyPropertyChanged("searchProducto"); } }

        private int _selectedTab = 0;
        public int selectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                NotifyPropertyChanged("selectedTab");
            }
        }

        #endregion

        #region Lista
        private ProductoPrecio _productoPrecio;
        public ProductoPrecio productoPrecio
        {
            get
            {
                return _productoPrecio;
            }
            set
            {

                _productoPrecio = value;
                NotifyPropertyChanged("productoPrecio");
            }
        }

        private IEnumerable<Producto> _listaProducto;
        public IEnumerable<Producto> listaProducto
        {
            get
            {

                _listaProducto = MV_ProductoPrecioService.buscarProductos(searchProducto);

                return _listaProducto;
            }
            set
            {
                _listaProducto = value;
                NotifyPropertyChanged("listaProducto");
            }
        }

        private IEnumerable<ProductoPrecio> _historialPrecios;
        public IEnumerable<ProductoPrecio> historialPrecios
        {
            get
            {
                return _historialPrecios;
            }
            set
            {
                _historialPrecios = value;
                NotifyPropertyChanged("historialPrecios");
            }
        }


        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaProductoPrecioCommand;
        public ICommand actualizarListaProductoPrecioCommand
        {
            get
            {
                if (_actualizarListaProductoPrecioCommand == null)
                {
                    _actualizarListaProductoPrecioCommand = new RelayCommand(param => NotifyPropertyChanged("listaProducto"));
                }
                return _actualizarListaProductoPrecioCommand;
            }
        }

        RelayCommand _viewDetailHistorialProdCommand;
        public ICommand viewDetailHistorialProdCommand
        {
            get
            {
                if (_viewDetailHistorialProdCommand == null)
                {
                    _viewDetailHistorialProdCommand = new RelayCommand(viewDetailHistorialProd);
                }
                return _viewDetailHistorialProdCommand;
            }
        }

        RelayCommand _cancelPrecioProductoCommand;

        public ICommand cancelPrecioProductoCommand
        {
            get
            {
                if (_cancelPrecioProductoCommand == null)
                {
                    _cancelPrecioProductoCommand = new RelayCommand(cancelPrecioProducto);
                }
                return _cancelPrecioProductoCommand;
            }
        }

        #endregion

        #region commands


        public void viewDetailHistorialProd(Object id)
        {
            try
            {
                this.productoPrecio = new ProductoPrecio();
                Producto prodSelected = MA_ProductoService.obtenerProductoxCodigo(Convert.ToString(id));
                this.historialPrecios = MV_ProductoPrecioService.obtenerHistorialbyProd(prodSelected);
                NotifyPropertyChanged("historialPrecios");
                this.productoPrecio.Producto = prodSelected;
                this.productoPrecio.fechaRegistro = DateTime.Today;
                NotifyPropertyChanged("productoPrecio");
                selectedTab = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public void cancelPrecioProducto(Object obj)
        {
            string messageBoxText;
            messageBoxText = "¿Desea cancelar la transacción? Usted perderá la información ingresada";
            string caption = "Mensaje de confirmación";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
            switch (result)
            {
                case MessageBoxResult.OK:
                    selectedTab = 0;
                    this.listaProducto = MV_ProductoPrecioService.listaproductos;
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        #endregion


    }
}

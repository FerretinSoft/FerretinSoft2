using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
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

        RelayCommand _savePrecioProductoCommand;
        public ICommand savePrecioProductoCommand
        {
            get
            {
                if (_savePrecioProductoCommand == null)
                {
                    _savePrecioProductoCommand = new RelayCommand(savePrecioProducto);
                }
                return _savePrecioProductoCommand;
            }
        }

        #endregion

        #region commands

        public void savePrecioProducto(Object obj)
        {
            if (productoPrecio.moneda == null || productoPrecio.precioString == null || productoPrecio.precioString == "")
                MessageBox.Show("Ingrese los campos obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string messageBoxText = "¿Desea confirmar la transacción? Se procederá a almacenar la información ingresada";
                string caption = "Mensaje de confirmación";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                switch (result)
                {
                    case MessageBoxResult.OK:

                        ComunService.idVentana(56);
                        if (productoPrecio.precioString.Last() == '.')
                            productoPrecio.precioString = productoPrecio.precioString + "00";

                        productoPrecio.precio = Convert.ToDecimal(productoPrecio.precioString);
                        productoPrecio.Producto.precioLista = productoPrecio.precio;
                        productoPrecio.Producto.precioPuntos = productoPrecio.precioPuntos;
                        productoPrecio.Producto.moneda = productoPrecio.moneda;
                        productoPrecio.Producto.ganarPuntos = productoPrecio.ganarPuntos;
                        
                        productoPrecio.estado = true;
                        NotifyPropertyChanged("productoPrecio");
                        if (historialPrecios.Count() != 0)
                        {

                            ProductoPrecio updProducto = (from c in historialPrecios
                                                          where c.estado == true
                                                          select c).Single();
                            updProducto.estado = false;
                        }

                        
                        if (!MV_ProductoPrecioService.insertarPrecio(productoPrecio))
                        {
                            MessageBox.Show("No se pudo agregar el nuevo precio", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("El nuevo precio fue agregado con éxito", "Mensaje de confirmación", MessageBoxButton.OK,MessageBoxImage.Information);
                            this.listaProducto = MV_ProductoPrecioService.buscarProductos(searchProducto);
                            NotifyPropertyChanged("listaProducto");
                            selectedTab = 0;
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }

        public void viewDetailHistorialProd(Object id)
        {
            try
            {
                this.productoPrecio = new ProductoPrecio();
                this.searchProducto = "";
                Producto prodSelected = MA_ProductoService.obtenerProductoxCodigo(Convert.ToString(id));
                this.historialPrecios = MV_ProductoPrecioService.obtenerHistorialbyProd(prodSelected);
                NotifyPropertyChanged("historialPrecios");
                this.productoPrecio.Producto = prodSelected;
                this.productoPrecio.fechaRegistro = DateTime.Today;
                ProductoPrecio precioActual  = MV_ProductoPrecioService.obtenerPrecioActualbyProd(prodSelected);
                this.productoPrecio.ganarPuntos = precioActual.ganarPuntos;
                this.productoPrecio.precio = precioActual.precio;
                this.productoPrecio.precioPuntos = precioActual.precioPuntos;
                this.productoPrecio.moneda = precioActual.moneda;
                this.productoPrecio.precioString = precioActual.precioString;
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
            messageBoxText = "Al salir, perderá todos los datos ingresados. ¿Desea continuar?";
            string caption = "ATENCIÓN";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    this.searchProducto = "";
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

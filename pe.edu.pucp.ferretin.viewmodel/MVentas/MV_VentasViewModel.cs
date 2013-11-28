using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_VentasViewModel : ViewModelBase
    {
        #region Constructor
        public MV_VentasViewModel()
        {
            _venta = new Venta();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda


        public String _nroNotaCredito = "";
        public String nroNotaCredito { get { return _nroNotaCredito; } set { _nroNotaCredito = value; NotifyPropertyChanged("nroNotaCredito"); } }


        public String _searchVendedor = "";
        public String searchVendedor { get { return _searchVendedor; } set { _searchVendedor = value; NotifyPropertyChanged("searchVendedor"); } }

        public String _nombreVendedor = "";
        public String nombreVendedor { get { return _nombreVendedor; } set { _nombreVendedor = value; NotifyPropertyChanged("nombreVendedor"); } }


        public String _searchNroDocumento = "";
        public String searchNroDocumento { get { return _searchNroDocumento; } set { _searchNroDocumento = value; NotifyPropertyChanged("searchNroDocumento"); } }

        public String _searchNroDocCliente = "";
        public String searchNroDocCliente { get { return _searchNroDocCliente; } set { _searchNroDocCliente = value; NotifyPropertyChanged("searchNroDocCliente"); } }

        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today;
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        public String _nombreCliente = "";
        public String nombreCliente { get { return _nombreCliente; } set { _nombreCliente = value; NotifyPropertyChanged("nombreCliente"); } }

        public System.Windows.Visibility _existeNota = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility existeNota { get { return _existeNota; } set { _existeNota = value; NotifyPropertyChanged("existeNota"); } }

        public long _id = 0;
        public long id { get { return _id; } set { _id = value; NotifyPropertyChanged("id"); } }

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


        private bool _soloSeleccionarVenta = false;
        public bool soloSeleccionarVenta
        {
            get
            {
                return _soloSeleccionarVenta;
            }
            set
            {
                _soloSeleccionarVenta = value;
                NotifyPropertyChanged("soloSeleccionarVenta");
                NotifyPropertyChanged("noSoloSeleccionarVenta");

            }
        }

        private System.Windows.Visibility _soloEscogerVenta = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility soloEscogerVenta
        {
            get
            {
                return _soloEscogerVenta;
            }
            set
            {
                _soloEscogerVenta = value;
                NotifyPropertyChanged("soloEscogerVenta");
               

            }
        }
        public bool noSoloSeleccionarVenta
        {
            get
            {
                return !soloSeleccionarVenta;
            }
        }

        #endregion

        #region Lista Ventas y Edicion de Venta
        private Venta _venta;
        public Venta venta
        {
            get
            {
                return _venta;
            }
            set
            {
                _venta = value;
                NotifyPropertyChanged("venta");
            }
        }

        private IEnumerable<Venta> _listaVentas;
        public IEnumerable<Venta> listaVentas
        {
            get
            {
                if (searchNroDocCliente!="")
                    _listaVentas = MV_VentaService.buscarVentas(searchNroDocumento, Convert.ToInt32(searchNroDocCliente), searchFechaInicio, searchFechaFin, searchVendedor);
                else
                    _listaVentas = MV_VentaService.buscarVentas(searchNroDocumento, null, searchFechaInicio, searchFechaFin, searchVendedor);
                
                return _listaVentas;
            }
            set
            {
                _listaVentas = value;
                NotifyPropertyChanged("listaVentas");
            }
        }

        private IEnumerable<VentaProducto> _listaProductos;
        public IEnumerable<VentaProducto> listaProductos
        {
            get
            {
                return _listaProductos;
            }
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("listaProductos");
            }
        }

        private IEnumerable<VentaMedioPago> _listaMedioPago;
        public IEnumerable<VentaMedioPago> listaMedioPago
        {
            get
            {
                return _listaMedioPago;
            }
            set
            {
                _listaMedioPago = value;
                NotifyPropertyChanged("listaMedioPago");
            }

        }
        #endregion

        #region RalayCommand

        RelayCommand _cargarClienteCommand;
        public ICommand cargarClienteCommand
        {
            get
            {
                if (_cargarClienteCommand == null)
                {
                    _cargarClienteCommand = new RelayCommand(cargarCliente);
                }
                return _cargarClienteCommand;
            }
        }

        RelayCommand _actualizarListaVentasCommand;
        public ICommand actualizarListaVentasCommand
        {
            get
            {
                if (_actualizarListaVentasCommand == null)
                {
                    _actualizarListaVentasCommand = new RelayCommand(param => NotifyPropertyChanged("listaVentas"));
                }
                return _actualizarListaVentasCommand;
            }
        }

        RelayCommand _viewDetailVentaCommand;
        public ICommand viewDetailVentaCommand
        {
            get
            {
                if (_viewDetailVentaCommand == null)
                {
                    _viewDetailVentaCommand = new RelayCommand(viewDetailVenta);
                }
                return _viewDetailVentaCommand;
            }
        }

        RelayCommand _cargarVendedorCommand;
        public ICommand cargarVendedorCommand
        {
            get
            {
                if (_cargarVendedorCommand == null)
                {
                    _cargarVendedorCommand = new RelayCommand(cargarVendedor);
                }
                return _cargarVendedorCommand;
            }
        }
        #endregion

        #region commands

        public void cargarVendedor(Object id)
        {
            Empleado buscado = null;
            try
            {
                buscado = MR_EmpleadoService.obtenerEmpleadoByNroDoc(searchVendedor);
                nombreVendedor = buscado.nombreCompleto;
            }
            catch { }

            if (buscado == null)
            {
                nombreVendedor = "";
                searchVendedor = "";
                MessageBox.Show("No se encontro ningún vendedor con el número de documento proporcionado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void viewDetailVenta(Object id)
        {
            try
            {
                NotaCredito nota = MV_NotaCreditoService.obtenerNotaCreditoByIdVenta((long)id);
                if (nota != null)
                {
                    this.nroNotaCredito = nota.codigo;
                    this.existeNota = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.nroNotaCredito = "";
                    this.existeNota = System.Windows.Visibility.Collapsed;
                }
                this.searchVendedor = "";
                this.searchNroDocumento = "";
                this.searchNroDocCliente = "";
                this.nombreCliente = "";
                this.nombreVendedor = "";
                this.venta = listaVentas.Single(venta => venta.id == (long)id);
                this.listaProductos = MV_VentaService.obtenerProductosbyIdVenta((long)id);
                int numServicio = 0;
                for (int i = 0; i < listaProductos.Count(); i++)
                {
                    if (listaProductos.ElementAt(i).id_servicio != null){
                        listaProductos.ElementAt(i).nombreProducto = listaProductos.ElementAt(i).Servicio.ServicioLinea.ElementAt(numServicio).descripcion;
                        numServicio++;
                    }
                }
                this.listaMedioPago = MV_VentaService.obtenerMedioDePagobyIdVenta((long)id);
                selectedTab = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(Convert.ToInt32(searchNroDocCliente));
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ningún cliente con el número de documento proporcionado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                nombreCliente = "";
                searchNroDocCliente="";
            }
            else
            {
                nombreCliente = buscado.nombreCompleto;
                searchNroDocCliente = Convert.ToString(buscado.nroDoc);
            }
            NotifyPropertyChanged("nombreCliente");
            NotifyPropertyChanged("searchNroDocCliente");
        }

        #endregion

    }
}

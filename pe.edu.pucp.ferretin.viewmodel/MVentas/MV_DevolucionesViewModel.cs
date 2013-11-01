using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_DevolucionesViewModel : ViewModelBase
    {
        #region Constructor
        public MV_DevolucionesViewModel()
        {
            _devolucion = new Devolucion();
        }
        #endregion


        #region Valores para el cuadro de Búsqueda
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

        public String _searchNroDevolucion = "";
        public String searchNroDevolucion { get { return _searchNroDevolucion; } set { _searchNroDevolucion = value; NotifyPropertyChanged("searchNroDevolucion"); } }

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
        #endregion


        #region Lista
        private Devolucion _devolucion;
        public Devolucion devolucion
        {
            get
            {
                return _devolucion;
            }
            set
            {
                _devolucion = value;
                NotifyPropertyChanged("devolucion");
            }
        }

        private NotaCredito _notaCredito;
        public NotaCredito notaCredito
        {
            get
            {
                return _notaCredito;
            }
            set
            {
                _notaCredito = value;
                NotifyPropertyChanged("notaCredito");
            }
        }
        private IEnumerable<Devolucion> _listaDevoluciones;
        public IEnumerable<Devolucion> listaDevoluciones
        {
            get
            {

                _listaDevoluciones = MV_DevolucionService.buscarDevoluciones(searchNroDevolucion, searchNroDocumento, searchNroDocCliente, searchFechaInicio, searchFechaFin);

                return _listaDevoluciones;
            }
            set
            {
                _listaDevoluciones = value;
                NotifyPropertyChanged("listaDevoluciones");
            }
        }

        private IEnumerable<DevolucionProducto> _listaProductos;
        public IEnumerable<DevolucionProducto> listaProductos
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

        private IEnumerable<DevolucionProducto> _listaProductosDev;
        public IEnumerable<DevolucionProducto> listaProductosDev
        {
            get
            {
                return _listaProductosDev;
            }
            set
            {
                _listaProductosDev = value;
                NotifyPropertyChanged("listaProductosDev");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaDevolucionesCommand;
        public ICommand actualizarListaDevolucionesCommand
        {
            get
            {
                if (_actualizarListaDevolucionesCommand == null)
                {
                    _actualizarListaDevolucionesCommand = new RelayCommand(param => NotifyPropertyChanged("listaDevoluciones"));
                }
                return _actualizarListaDevolucionesCommand;
            }
        }
        RelayCommand _viewDetailDevolucionCommand;
        public ICommand viewDetailDevolucionCommand
        {
            get
            {
                if (_viewDetailDevolucionCommand == null)
                {
                    _viewDetailDevolucionCommand = new RelayCommand(viewDetailDevolucion);
                }
                return _viewDetailDevolucionCommand;
            }
        }

        RelayCommand _addProductDevCommand;
        public ICommand addProductDevCommand
        {
            get
            {
                if (_addProductDevCommand == null)
                {
                    _addProductDevCommand = new RelayCommand(addProductDev);
                }
                return _addProductDevCommand;
            }
        }

        #endregion

        #region commands
        public void viewDetailDevolucion(Object id)
        {
            try
            {
                this.devolucion = listaDevoluciones.Single(devolucion => devolucion.id == (long)id);
                this.listaProductos = MV_DevolucionService.obtenerProductosbyIdDevolucion((long)id);
                this.notaCredito = MV_DevolucionService.obtenerNotaCredbyIdDevolucion((long)id);
                selectedTab = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void addProductDev(Object id)
        {
            try
            {
                 DevolucionProducto prodDev = new DevolucionProducto();
                 VentaProducto prodSelec = MV_VentaService.obtenerVentaProductobyId((long)id);
                 prodDev.Producto = prodSelec.Producto;
                 prodDev.Producto.codigo = prodSelec.Producto.codigo;
                 devolucion.DevolucionProducto.Add(prodDev);
                 this.listaProductosDev = devolucion.DevolucionProducto;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        
        #endregion
    }
}

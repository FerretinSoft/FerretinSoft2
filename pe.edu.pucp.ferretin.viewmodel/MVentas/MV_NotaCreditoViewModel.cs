using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using System.Windows.Documents;
using System.Windows.Controls;
using System.IO;
using System.Xml;
using System.Windows.Markup;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_NotaCreditoViewModel : ViewModelBase
    {
        #region Constructor
        public MV_NotaCreditoViewModel()
        {
            _notaCredito = new NotaCredito();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda

        public String _searchVendedor = "";
        public String searchVendedor { get { return _searchVendedor; } set { _searchVendedor = value; NotifyPropertyChanged("searchVendedor"); } }

        public String _nombreVendedor = "";
        public String nombreVendedor { get { return _nombreVendedor; } set { _nombreVendedor = value; NotifyPropertyChanged("nombreVendedor"); } }


        public String _searchNroNotaCredito = "";
        public String searchNroNotaCredito { get { return _searchNroNotaCredito; } set { _searchNroNotaCredito = value; NotifyPropertyChanged("searchNroNotaCredito"); } }

        public String _searchNroDocCliente = "";
        public String searchNroDocCliente { get { return _searchNroDocCliente; } set { _searchNroDocCliente = value; NotifyPropertyChanged("searchNroDocCliente"); } }

        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today;
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        public String _nombreCliente = "";
        public String nombreCliente { get { return _nombreCliente; } set { _nombreCliente = value; NotifyPropertyChanged("nombreCliente"); } }

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

        private IEnumerable<NotaCredito> _listaNotasDeCredito;
        public IEnumerable<NotaCredito> listaNotasDeCredito
        {
            get
            {

                _listaNotasDeCredito = MV_NotaCreditoService.buscarNotaCredito(searchNroNotaCredito, searchNroDocCliente, searchFechaInicio, searchFechaFin, searchVendedor);

                return _listaNotasDeCredito;
            }
            set
            {
                _listaNotasDeCredito = value;
                NotifyPropertyChanged("listaNotasDeCredito");
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

        #endregion

        

        #region RalayCommand
        RelayCommand _actualizarListaNotasDeCreditoCommand;
        public ICommand actualizarListaNotasDeCreditoCommand
        {
            get
            {
                if (_actualizarListaNotasDeCreditoCommand == null)
                {
                    _actualizarListaNotasDeCreditoCommand = new RelayCommand(param => NotifyPropertyChanged("listaNotasDeCredito"));
                }
                return _actualizarListaNotasDeCreditoCommand;
            }
        }

        RelayCommand _viewDetailNotaCreditoCommand;
        public ICommand viewDetailNotaCreditoCommand
        {
            get
            {
                if (_viewDetailNotaCreditoCommand == null)
                {
                    _viewDetailNotaCreditoCommand = new RelayCommand(viewDetailNotaCredito);
                }
                return _viewDetailNotaCreditoCommand;
            }
        }

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

        RelayCommand _imprimirDocCommand;
        public ICommand imprimirDocCommand
        {
            get
            {
                if (_imprimirDocCommand == null)
                {
                    _imprimirDocCommand = new RelayCommand(imprimirDoc);
                }
                return _imprimirDocCommand;
            }
        }
        



        #endregion

        #region commands

        public void imprimirDoc(Object id)
        {

            MessageBox.Show(notaCredito.codigo);
        }

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
                MessageBox.Show("No se encontro ningún vendedor con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }

        }

        public void viewDetailNotaCredito(Object id)
        {
            try
            {
                this.notaCredito = listaNotasDeCredito.Single(notaCredito => notaCredito.id == (long)id);
                NotaCredito nota = MV_NotaCreditoService.obtenerNotaCreditoById((long)id);
                this.listaProductos = MV_DevolucionService.obtenerProductosbyIdDevolucion((long)nota.id_devolucion);

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
                buscado = MV_ClienteService.obtenerClienteByNroDoc(searchNroDocCliente);
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ningún Cliente con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
                nombreCliente = "";
                searchNroDocCliente = "";
            }
            else
            {
                nombreCliente = buscado.nombreCompleto;
                searchNroDocCliente = buscado.nroDoc;
            }
            NotifyPropertyChanged("nombreCliente");
            NotifyPropertyChanged("searchNroDocCliente");
        }
        #endregion

    }
}

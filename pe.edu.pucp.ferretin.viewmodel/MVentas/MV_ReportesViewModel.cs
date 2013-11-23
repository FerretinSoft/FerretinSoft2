using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ReportesViewModel : ViewModelBase
    {
        #region Constructor
        
        public MV_ReportesViewModel()
        {
            
        }
        #endregion

        #region Valores para el cuadro de Búsqueda

        public IEnumerable<Tienda> _listaTiendas;
        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                IEnumerable<Tienda> _listaTiendas = new Tienda[] { new Tienda { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return _listaTiendas.Concat(MS_TiendaService.listaTiendas);
            }

            set
            {
                _listaTiendas = value;
                NotifyPropertyChanged("listaTiendas");
            }
        }


        public Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            get
            {
                return (_selectedTienda != null) ? _selectedTienda : usuarioLogueado.Empleado.tiendaActual;
            }

            set
            {
                _selectedTienda = value;
                NotifyPropertyChanged("selectedTienda");
            }

        }
        public String _aliasRep = "";
        public String aliasRep { get { return _aliasRep; } set { _aliasRep = value; NotifyPropertyChanged("aliasRep"); } }

        public int _searchProducto = 0;
        public int searchProducto { get { return _searchProducto; } set { _searchProducto = value; NotifyPropertyChanged("searchProducto"); } }

        public string _searchCliente = "";
        public string searchCliente { get { return _searchCliente; } set { _searchCliente = value; NotifyPropertyChanged("searchCliente"); } }


        public String _nombreBoton = "SIGUIENTE";
        public String nombreBoton { get { return _nombreBoton; } set { _nombreBoton = value; NotifyPropertyChanged("nombreBoton"); } }

        public String _nombreVentana = "";
        public String nombreVentana { get { return _nombreVentana; } set { _nombreVentana = value; NotifyPropertyChanged("nombreVentana"); } }

        public System.Windows.Visibility _visibleTienda = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility visibleTienda { get { return _visibleTienda; } set { _visibleTienda = value; NotifyPropertyChanged("visibleTienda"); } }

        public System.Windows.Visibility _visibleVendedor = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility visibleVendedor { get { return _visibleVendedor; } set { _visibleVendedor = value; NotifyPropertyChanged("visibleVendedor"); } }

        public System.Windows.Visibility _visibleCliente = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility visibleCliente { get { return _visibleCliente; } set { _visibleCliente = value; NotifyPropertyChanged("visibleCliente"); } }
        

        public String _comentRep = "";
        public String comentRep { get { return _comentRep; } set { _comentRep = value; NotifyPropertyChanged("comentRep"); } }
        
        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today.AddDays(1);
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        public String _searchVendedor = "";
        public String searchVendedor { get { return _searchVendedor; } set { _searchVendedor = value; NotifyPropertyChanged("searchVendedor"); } }

        public String _nombreVendedor = "";
        public String nombreVendedor { get { return _nombreVendedor; } set { _nombreVendedor = value; NotifyPropertyChanged("nombreVendedor"); } }

        public String _nombreCliente = "";
        public String nombreCliente { get { return _nombreCliente; } set { _nombreCliente = value; NotifyPropertyChanged("nombreCliente"); } }
        
        #endregion
        #region relay commad
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
    #endregion

        #region command
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

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(Convert.ToInt32(searchCliente));
                nombreCliente = buscado.nombreCompleto;
                searchCliente = Convert.ToString(buscado.nroDoc);
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ningún cliente con el número de documento proporcionado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                nombreCliente = "";
                searchCliente = "";
            }

            NotifyPropertyChanged("nombreCliente");
            NotifyPropertyChanged("searchCliente");
        }
        #endregion

    }
}

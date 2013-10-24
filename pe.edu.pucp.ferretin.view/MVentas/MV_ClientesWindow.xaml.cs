using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    public class MV_ClientesViewModel : INotifyPropertyChanged
    {
        #region Valores para el cuadro de Búsqueda
        public String searchNroDoc { get; set; }
        public String searchNombre { get; set; }
        public String searchApMaterno { get; set; }
        public String searchApPaterno { get; set; }
        public int searchTipoDocumento { get; set; }
        #endregion

        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        private int _statusTab = (int)tabs.BUSQUEDA; //pestaña default 
        public int statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value == 0 ? 0 : 1;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (value)
                {
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
            }
        }

        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == 0 ? 0 : 1; }
            set { statusTab = (int)tabs.AGREGAR; NotifyPropertyChanged("currentIndexTab"); }
        }

        private String _detallesTabHeader = "Agregar"; //Default
        public String detallesTabHeader
        {
            get
            {
                return _detallesTabHeader;
            }
            set
            {
                _detallesTabHeader = value;
                NotifyPropertyChanged("detallesTabHeader");
            }
        }

        private Cliente _cliente = new Cliente();
        public Cliente cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                _cliente = value;
                NotifyPropertyChanged("cliente");
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }

    /// <summary>
    /// Lógica de interacción para MV_ClientesWindow.xaml
    /// </summary>
    public partial class MV_ClientesWindow : Window
    {

        MV_ClientesViewModel MV_ClientesViewModel = new MV_ClientesViewModel();
        public MV_ClientesWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(
              new ThreadStart(
                delegate()
                {
                    clientesGrid.Dispatcher.Invoke(
                      DispatcherPriority.Normal,
                      new Action(
                        delegate()
                        {
                            clientesGrid.ItemsSource = MV_ClienteService.obtenerListaClientes();
                        }
                    ));
                }
            ));
            thread.Start();
            clientesTabControl.DataContext = MV_ClientesViewModel;
        }

        private IEnumerable<Cliente> ListaClientes()
        {
            return MV_ClienteService.obtenerListaClientes();
        }

        public void numDocumento_Click(object sender, RoutedEventArgs e)
        {
            if (clientesGrid.SelectedItem != null)
            {
                MV_ClientesViewModel.cliente = (Cliente)clientesGrid.SelectedItem;
                MV_ClientesViewModel.statusTab = (int)MV_ClientesViewModel.tabs.MODIFICAR;//Modificar
            }
        }

        public MV_ClientesWindow(MV_AdministrarVentasWindow MV_AdministrarVentasWindow)
        {
            InitializeComponent();
            Show();
        }

        private void nuevoClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesViewModel.statusTab = (int)MV_ClientesViewModel.tabs.AGREGAR;

        }



        private void cancelarListaClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesViewModel.statusTab = (int)MV_ClientesViewModel.tabs.BUSQUEDA;
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.nroDoc = (MV_ClientesViewModel.searchNroDoc == null) ? "" : MV_ClientesViewModel.searchNroDoc;
            cliente.nombre = (MV_ClientesViewModel.searchNombre == null) ? "" : MV_ClientesViewModel.searchNombre;
            cliente.apMaterno = (MV_ClientesViewModel.searchApMaterno == null) ? "" : MV_ClientesViewModel.searchApMaterno;
            cliente.apPaterno = (MV_ClientesViewModel.searchApPaterno == null) ? "" : MV_ClientesViewModel.searchApPaterno;
            cliente.tipoDocumento = MV_ClientesViewModel.searchTipoDocumento > 0 ? (MV_ClientesViewModel.searchTipoDocumento == 1 ? "DNI" : "RUC") : "";
            clientesGrid.ItemsSource = MV_ClienteService.obtenerListaClientesBy(cliente);
        }

        private void guardarDetalleClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Puede ser nuevo o modificar
            if (MV_ClientesViewModel.cliente.id > 0)
            {
                MV_ClienteService.actualizarCliente(MV_ClientesViewModel.cliente);

            }
            else
            {
                MV_ClienteService.insertarCliente(MV_ClientesViewModel.cliente);
            }
            MV_ClientesViewModel.statusTab = (int)MV_ClientesViewModel.tabs.BUSQUEDA;
        }

    }

}

using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
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
            MV_ClienteService.insertarCliente(MV_ClientesViewModel.cliente);
            MV_ClienteService.enviarCambios();
            MV_ClientesViewModel.statusTab = (int)MV_ClientesViewModel.tabs.BUSQUEDA;
        }

    }

}

using System.Windows;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.viewmodel.MCompras;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_AdministrarOCCotizacionWindow.xaml
    /// </summary>
    public partial class MC_AdministrarOCCotizacionWindow : Window
    {
        public MC_AdministrarOCCotizacionWindow()
        {
            InitializeComponent();
        }

        private void agregarProducto_Click(object sender, RoutedEventArgs e)
        {
            if ((string)(this.razSoc_Lbl.Content) != null)
            {
                var vm = DataContext as MC_CotizacionesOCViewModel;
                var buscador = new MC_BuscarProductosProveedorWindow(this, vm.documentoCompra.Proveedor);        
            }
            else
            {
                MessageBox.Show("Debe ingresar un Proveedor", "Mensaje error", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            
        }

        private void buscarProveedorBtn2_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MC_CotizacionesOCViewModel;
            var buscador = new MC_BuscadorProveedores(this);
            
            //MC_AdministrarProveedorWindow v = new MC_AdministrarProveedorWindow();
            //v.Owner = this;
            //var viewModel = v.DataContext as MC_ProveedoresViewModel;
            //viewModel.soloSeleccionarProveedor = true;
            //v.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
        }
    }
}

using System.Windows;
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

        private void Button_Click_Proveedor(object sender, RoutedEventArgs e)
        {
            MC_AdministrarProveedorWindow p = new MC_AdministrarProveedorWindow();
           p.Owner = this;
            var viewModel = p.DataContext as MC_AdministrarProveedorWindow;

        }

        private void agregarProducto_Click(object sender, RoutedEventArgs e)
        {

            if ((string)(this.razSoc_Lbl.Content) != "")
            {
                MC_BuscarProductosProveedorWindow v = new MC_BuscarProductosProveedorWindow();
                v.Owner = this;
                var viewModel = v.main.DataContext as MC_BuscarProductosProveedorViewModel;
                viewModel.searchProveedor = (string)(this.razSoc_Lbl.Content);
                v.proveedorTxt.IsEnabled = false;
                v.ShowDialog();              
            }
            else
            {
                MessageBox.Show("Debe ingresar un Proveedor", "Mensaje error", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            
        }
    }
}

using System.Windows;
using pe.edu.pucp.ferretin.viewmodel.MCompras;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_AdministrarGuiaRemiWindow.xaml
    /// </summary>
    public partial class MC_AdministrarGuiaRemiWindow : Window
    {
        public MC_AdministrarGuiaRemiWindow()
        {
            InitializeComponent();
        }

        //buscarOC_Click

        private void buscarOC_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MC_GuiaRemisionViewModel;
            var buscador = new MC_BuscadorCotizacionesWindow(this);

            //MC_AdministrarProveedorWindow v = new MC_AdministrarProveedorWindow();
            //v.Owner = this;
            //var viewModel = v.DataContext as MC_ProveedoresViewModel;
            //viewModel.soloSeleccionarProveedor = true;
            //v.ShowDialog();
        }


    }
}

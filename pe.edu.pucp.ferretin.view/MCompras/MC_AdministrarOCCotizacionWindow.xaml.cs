using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            var viewModel = p.main.DataContext as MC_AdministrarProveedorWindow;
        }

        private void agregarProducto_Click(object sender, RoutedEventArgs e)
        {
            MC_BuscarProductosProveedorWindow v = new MC_BuscarProductosProveedorWindow();
            v.Owner = this;
            v.ShowDialog();
            //var viewModel = v.main.DataContext as MC_BuscarProductosProveedorWindow;
        }
    }
}

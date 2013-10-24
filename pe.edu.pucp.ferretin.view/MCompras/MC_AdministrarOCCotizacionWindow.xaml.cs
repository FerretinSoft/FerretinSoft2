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

        private void edOrdenCompraBtn_Click(object sender, RoutedEventArgs e)
        {
            ordenesCompraTab.SelectedIndex = 1;
        }

        private void nuevoProdProveedorBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_AgregarProdProveedor agprodp = new MC_AgregarProdProveedor();
            agprodp.Owner = this;
            agprodp.ShowDialog();
        }

        private void buscarProveedorBtn_Click(object sender, RoutedEventArgs e)
        {
            MC_AdministrarProveedorWindow busqProv = new MC_AdministrarProveedorWindow();
            busqProv.Owner = this;
            busqProv.ShowDialog();
        }

        private void cancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            ordenesCompraTab.SelectedIndex = 0;
        }

        private void buscarProveedorBtn2_Click(object sender, RoutedEventArgs e)
        {
            MC_AdministrarProveedorWindow busqProv = new MC_AdministrarProveedorWindow();
            busqProv.Owner = this;
            busqProv.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

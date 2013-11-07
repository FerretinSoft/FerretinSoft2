using pe.edu.pucp.ferretin.view.MAlmacen;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
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

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_RegistrarVentaWindow.xaml
    /// </summary>
    public partial class MV_RegistrarVentaWindow : Window
    {
        public MV_RegistrarVentaWindow()
        {
            InitializeComponent();
        }

        

        private void pagarBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_PagoWindow pw = new MV_PagoWindow(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buscarProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarProformasWindow profWindow = new MV_AdministrarProformasWindow();
            profWindow.Show();
        }


        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.Show();
        }
    }
}

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
    /// Lógica de interacción para MV_AdministrarNotaCreditoWindow.xaml
    /// </summary>
    public partial class MV_AdministrarNotaCreditoWindow : Window
    {
        public MV_AdministrarNotaCreditoWindow()
        {
            InitializeComponent();
        }

        private void detalleNotaCreditoBtn_Click(object sender, RoutedEventArgs e)
        {
            NotaCreditoTab.SelectedIndex = 1;
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            //ClientesWindow pw = new ClientesWindow();
            //pw.Show();
        }

        private void buscarVendedorBtn_Click(object sender, RoutedEventArgs e)
        {
            //PersonalAdminWindow pw = new PersonalAdminWindow();
            //pw.Show();
        }

        private void nuevaNotaCreditoBtn_Click(object sender, RoutedEventArgs e)
        {
            //RegistrarNotaCreditoWindow pw = new RegistrarNotaCreditoWindow();
            //pw.Show();
        }

        public void seleccionarCliente()
        {

        }
    }
}

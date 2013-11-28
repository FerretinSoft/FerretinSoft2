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
    /// Lógica de interacción para MV_ServiciosWindow.xaml
    /// </summary>
    public partial class MV_ServiciosWindow : Window
    {
        private MV_RegistrarVentaViewModel vm;

        public MV_ServiciosWindow()
        {
            InitializeComponent();
        }

        public MV_ServiciosWindow(MV_RegistrarVentaViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
        }


        /// <summary>
        /// Seleccionar Cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void nuevaPromocionBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mivm = DataContext as MV_ServiciosViewModel;
            var v = new MV_TiposServiciosWindow(this);
            v.Owner = this;
            v.ShowDialog();
        }
    }
}

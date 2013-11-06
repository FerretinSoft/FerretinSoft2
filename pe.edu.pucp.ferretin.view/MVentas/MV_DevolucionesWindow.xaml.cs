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
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DevolucionesWindow.xaml
    /// </summary>
    public partial class MV_DevolucionesWindow : Window
    {
        public MV_DevolucionesWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void nuevaDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 2;
        }

        private void editarDevolucionBtn_Click(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 1;
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.Show();     
        }

        private void Button_LoadEmpresa(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
           
            v.Show();     
        }
        


        private void Button_Click_Venta(object sender, RoutedEventArgs e)
        {
            MV_AdministrarVentasWindow v = new MV_AdministrarVentasWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_VentasViewModel;
            viewModel.soloSeleccionarVenta = true;
            viewModel.soloEscogerVenta =  System.Windows.Visibility.Visible; 

            v.Show();     
        }



        private void Button_Click_Vendedor(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.Show();     
        }


        private void Button_Click_VentaLoad(object sender, RoutedEventArgs e)
        {
            MV_AdministrarVentasWindow v = new MV_AdministrarVentasWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_VentasViewModel;
            viewModel.soloSeleccionarVenta = false;
            viewModel.soloEscogerVenta =  System.Windows.Visibility.Visible; 

            v.Show();     
        }


        private void DatePicker_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }




        private void TextBox_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
      

        }
    }
}

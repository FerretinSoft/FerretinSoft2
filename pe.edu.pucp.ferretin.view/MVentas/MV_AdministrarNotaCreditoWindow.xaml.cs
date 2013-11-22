using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
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
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using System.Data;
using System.ComponentModel;

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
            //pw.ShowDialog();
        }

        private void buscarVendedorBtn_Click(object sender, RoutedEventArgs e)
        {
            //PersonalAdminWindow pw = new PersonalAdminWindow();
            //pw.ShowDialog();
        }

        private void nuevaNotaCreditoBtn_Click(object sender, RoutedEventArgs e)
        {
            //RegistrarNotaCreditoWindow pw = new RegistrarNotaCreditoWindow();
            //pw.ShowDialog();
        }

        public void seleccionarCliente()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void Button_Click_Vendedor(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.ShowDialog();
        }

        private void visualizar_DocClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void GenerarDoc_Click(object sender, RoutedEventArgs e)
        {
            MV_DocNotaCredito p = new MV_DocNotaCredito();
            MV_NotaCreditoViewModel actual = this.main.DataContext as MV_NotaCreditoViewModel;
            MV_DocNotaCreditoViewModel padre = p.main.DataContext as MV_DocNotaCreditoViewModel;
            padre.notaCredito = actual.notaCredito; 
            p.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
           
            MV_ComunService.Clean();

        }
        
        
    }
}

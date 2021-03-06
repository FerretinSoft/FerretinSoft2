﻿using pe.edu.pucp.ferretin.controller.MVentas;
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
            this.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buscarProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_AdministrarProformasWindow profWindow = new MV_AdministrarProformasWindow();
            profWindow.Owner = this;
            MV_AdministrarProformasViewModel prodViewModel = profWindow.DataContext as MV_AdministrarProformasViewModel;
            prodViewModel.soloSeleccionarProforma = true;
            profWindow.ShowDialog();
        }


        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Buscar Cliente
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MV_ComunService.Clean();            
        }


        /// <summary>
        /// Buscar Producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_RegistrarVentaViewModel;
            var buscador = new MV_BuscadorProductos(this,vm.usuarioLogueado.Empleado.tiendaActual);
        }


        /// <summary>
        /// Buscar Servicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_RegistrarVentaViewModel;
            var buscador = new MV_ServiciosWindow(vm);
            
            buscador.Owner = this;
            buscador.ShowDialog();
        }
    }
}

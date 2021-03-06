﻿using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.view.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_SolicitudAbastecimientoWindow.xaml
    /// </summary>
    public partial class MA_RegistroSolAbastecimientoWindow : Window
    {
        public MA_RegistroSolAbastecimientoWindow()
        {
            InitializeComponent();
            
        }

        private void atenderSolBtn_Click(object sender, RoutedEventArgs e)
        {
            solicitudesTab.SelectedIndex = 3;
        }

        private void buscarSolBtn_Click(object sender, RoutedEventArgs e)
        {
            solicitudesTab.SelectedIndex = 0;
        }

        private void consolidarBtn_Click(object sender, RoutedEventArgs e)
        {
            MCompras.MC_ConsolidarSolicitudesWindow consoli = new MCompras.MC_ConsolidarSolicitudesWindow();
            consoli.ShowDialog();
        }

        private void borrarProductosBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_RegistroSolAbastecimientoViewModel vm  = (MA_RegistroSolAbastecimientoViewModel)this.main.DataContext;
                
            for (int i = 0; i < productosGrid.SelectedItems.Count; i++)
			{
                MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda current =
                        (MA_SolicitudAbastecimientoService.ProductoPorSolicitudTienda)productosGrid.SelectedItems[i];
                vm.solicitud.SolicitudAbastecimientoProducto.Remove(current.productoPorSolicitud);
                vm.productosPorSolicitud.Remove(current);
			}

            vm.NotifyPropertyChanged("productosPorSolicitud");
            vm.NotifyPropertyChanged("current");
        
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
        }

        private void productosGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            if (grid.CurrentCell.Column.DisplayIndex == 2)
            {
                //Validaciones para que acepte solo numeros
                if (((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void searchProductosBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.main.DataContext as MA_RegistroSolAbastecimientoViewModel;
            var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);
        }

        
    }
}

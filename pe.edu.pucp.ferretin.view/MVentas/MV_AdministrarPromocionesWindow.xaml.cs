using pe.edu.pucp.ferretin.controller;
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
    /// Lógica de interacción para MV_AdministrarPromocionesWindow.xaml
    /// </summary>
    public partial class MV_AdministrarPromocionesWindow : Window
    {
        public MV_AdministrarPromocionesWindow()
        {
            InitializeComponent();
            //promocionesGrid.ItemsSource = ListaPromociones();
        }

        
        private void editarPromocionBtn_Click(object sender, RoutedEventArgs e)
        {
            VentasTab.SelectedIndex = 1;
        }

        private void nuevaPromocionBtn_Click(object sender, RoutedEventArgs e)
        {
            VentasTab.SelectedIndex = 1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComunService.Clean();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_PromocionesViewModel;
            var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);

        }

        /// <summary>
        /// Abrir Tipos de promociones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mivm = DataContext as MV_PromocionesViewModel; 
            var v = new MV_TiposPromocionesWindow();
            v.ShowDialog();
            
        }
    }
}

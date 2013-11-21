using pe.edu.pucp.ferretin.controller;
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
    }
}

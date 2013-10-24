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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DevolucionesTab.SelectedIndex = 0;
        }

        private void DatePicker_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private MV_AdministrarVentasWindow ventasWindow;
        private void buscarVentaBtn_Click(object sender, RoutedEventArgs e)
        {
            ventasWindow = new MV_AdministrarVentasWindow(this);
        }
        public void seleccionarVenta(object sender, RoutedEventArgs e)
        {
            codVentaList.Text = ventasWindow.selectedCodVenta.ToString();
            ventasWindow.Close();
        }


        private void codVentaList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cargarVenta();
            }
        }

        private void cargarVenta()
        {
            if (codVentaList.Text.Length > 0)
            {
                //cargar venta
            }
        }
    }
}

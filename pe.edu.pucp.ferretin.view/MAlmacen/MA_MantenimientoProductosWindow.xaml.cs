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
    /// Lógica de interacción para MA_MantenimientoProductosWindow.xaml
    /// </summary>
    public partial class MA_MantenimientoProductosWindow : Window
    {
        public MA_MantenimientoProductosWindow()
        {
            InitializeComponent();
        }

        private void nuevoProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            mantenimientoTab.SelectedIndex = 1;
        }

        private void editarProductoBtn_Click(object sender, RoutedEventArgs e)
        {
            mantenimientoTab.SelectedIndex = 1;
        }
    }
}

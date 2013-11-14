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
    /// Lógica de interacción para MA_InventarioProductosWindow.xaml
    /// </summary>
    public partial class MA_InventarioProductosWindow : Window
    {
        public MA_InventarioProductosWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Back || e.Key == Key.Tab || (e.Key == Key.Space)))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}

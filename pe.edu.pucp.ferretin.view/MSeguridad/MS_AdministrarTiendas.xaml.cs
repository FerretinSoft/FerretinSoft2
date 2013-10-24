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

namespace pe.edu.pucp.ferretin.view.MSeguridad
{
    /// <summary>
    /// Lógica de interacción para MS_AdministrarTiendas.xaml
    /// </summary>
    public partial class MS_AdministrarTiendas : Window
    {
        public MS_AdministrarTiendas()
        {
            InitializeComponent();
        }

        private void nuevaTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            tiendaTab.SelectedIndex = 1;
        }

        private void edTiendaBtn_Click(object sender, RoutedEventArgs e)
        {
            tiendaTab.SelectedIndex = 1;
        }

        public void codTienda_Click(object sender, RoutedEventArgs e)
        {
            tiendaTab.SelectedIndex = 1;
        }
    }
}

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
    /// Lógica de interacción para MA_MainWindow.xaml
    /// </summary>
    public partial class MA_MainWindow : Window
    {
        public MA_MainWindow()
        {
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OwnedWindows.Count == 0)
            {
                this.Close();
            }
        }

        private void invProdBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_InventarioProductosWindow invWindow = new MA_InventarioProductosWindow();
            invWindow.Show();
        }

        private void prodBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_MantenimientoProductosWindow prodWindow = new MA_MantenimientoProductosWindow();
            prodWindow.Show();
        }

        private void movBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_MovimientosWindow movWindow = new MA_MovimientosWindow();
            movWindow.Show();
        }

        private void repAlmacenBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_ReportesWindow repWindow = new MA_ReportesWindow();
            repWindow.Show();
        }

        private void solAbastecimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_SolicitudAbastecimientoWindow solWindow = new MA_SolicitudAbastecimientoWindow();
            solWindow.Show();
        }

        private void mantCatBtn_Click(object sender, RoutedEventArgs e)
        {
            MA_MantenimientoCategoriasWindow catWindow = new MA_MantenimientoCategoriasWindow();
            catWindow.Show();
        }
    }
}

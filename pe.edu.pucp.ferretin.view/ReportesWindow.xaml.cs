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

namespace pe.edu.pucp.ferretin.view
{
    /// <summary>
    /// Interaction logic for ReportesWindow.xaml
    /// </summary>
    public partial class ReportesWindow : Window
    {
        public ReportesWindow()
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

        private void RepAlmBtn_Click(object sender, RoutedEventArgs e)
        {
            MAlmacen.MA_ReportesWindow RAW = new MAlmacen.MA_ReportesWindow();
            RAW.Show();
        }

        private void RepVenBtn_Click(object sender, RoutedEventArgs e)
        {
            MVentas.MV_ReportesVentasWindow RVW = new MVentas.MV_ReportesVentasWindow();
            RVW.Show();
        }

        private void RepComBtn_Click(object sender, RoutedEventArgs e)
        {
            MCompras.MC_ReportesWindow RCW = new MCompras.MC_ReportesWindow();
            RCW.Show();
        }

        private void RepSegBtn_Click(object sender, RoutedEventArgs e)
        {
            MSeguridad.MS_ReporteWindow RSW= new MSeguridad.MS_ReporteWindow();
            RSW.Show();
        }


    }
}

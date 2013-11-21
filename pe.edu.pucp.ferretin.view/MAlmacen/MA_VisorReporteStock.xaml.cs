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
    /// Lógica de interacción para MA_VisorReporteStock.xaml
    /// </summary>
    public partial class MA_VisorReporteStock : Window
    {
        ReporteStock rep;

        public MA_VisorReporteStock(int idTienda)
        {
            InitializeComponent();
            rep = new ReporteStock();

            rep.SetParameterValue("idTiendaParam", idTienda);

            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            rep.Refresh();

            rep.SetParameterValue("idTiendaParam", idTienda);

            VisorReporteStock.ViewerCore.ReportSource = rep;
        }
        
        private void VisorReporteStock_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }
    }
}

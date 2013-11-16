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
    /// Interaction logic for MS_ReporteWindow.xaml
    /// </summary>
    public partial class MS_ReporteWindow : Window
    {
        ReporteDeTransacciones rep;

        public MS_ReporteWindow()
        {
            InitializeComponent();
            rep = new ReporteDeTransacciones();
            VisorDelReporte.ViewerCore.ReportSource = rep;
        }

        private void VisorDelReporte_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }
    }
}

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
    /// Lógica de interacción para MV_VisorReporte.xaml
    /// </summary>
    public partial class MV_VisorReporte : Window
    {
        
        public MV_VisorReporte(DateTime fechaInicio, DateTime fechaFin, string nombreReporte)
        {
            InitializeComponent();
            if (nombreReporte.Equals("RTienda"))
            {
                ReporteVentaTienda rep;
                rep = new ReporteVentaTienda();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);

                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                VisorReporte.ViewerCore.ReportSource = rep;
            }
            else
            {
                ReporteVentaProducto rep;
                rep = new ReporteVentaProducto();
                VisorReporte.ViewerCore.ReportSource = rep;
            }

            
        }

        private void VisorReporte_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            //rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }
    }
}

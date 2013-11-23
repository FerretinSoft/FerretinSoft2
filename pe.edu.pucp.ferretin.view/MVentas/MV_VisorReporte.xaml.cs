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
        
        public MV_VisorReporte(DateTime fechaInicio, DateTime fechaFin, int selectedItem, string nombreReporte, string codEmpleado, string codCliente)
        {
            InitializeComponent();
            if (nombreReporte.Equals("RTienda"))
            {
                ReporteVentaTienda rep;
                rep = new ReporteVentaTienda();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                VisorReporte.ViewerCore.ReportSource = rep;
            }else if (nombreReporte.Equals("RCliente")){
                ReporteVentaCliente rep;
                rep = new ReporteVentaCliente();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                VisorReporte.ViewerCore.ReportSource = rep;
            }
            else if (nombreReporte.Equals("RProducto"))
            {
                ReporteVentaProducto rep;
                rep = new ReporteVentaProducto();
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
                ReporteVentaVendedor rep;
                rep = new ReporteVentaVendedor();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                VisorReporte.ViewerCore.ReportSource = rep;
            }

            
        }

        private void VisorReporte_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            //rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }
    }
}

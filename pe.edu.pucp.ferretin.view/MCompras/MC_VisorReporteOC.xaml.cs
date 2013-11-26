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
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Lógica de interacción para MA_VisorReporteStock.xaml
    /// </summary>
    public partial class MC_VisorReporteOC : Window
    {
        ReporteOrdenCompra rep;

        public MC_VisorReporteOC(DateTime f_ini, DateTime f_fin, int selectedItem)
        {
            InitializeComponent();
            rep = new ReporteOrdenCompra();

            rep.SetParameterValue("f_ini", f_ini);
            rep.SetParameterValue("f_fin", f_fin);
            rep.SetParameterValue("idTienda", selectedItem);
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            rep.Refresh();

            rep.SetParameterValue("f_ini", f_ini);
            rep.SetParameterValue("f_fin", f_fin);
            rep.SetParameterValue("idTienda", selectedItem);
            VisorReporteOC.ViewerCore.ReportSource = rep;
        }

        private void VisorReporteOC_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }

        //private void VisorReporteOC_Loaded(object sender, RoutedEventArgs e)
        //{

        //}
    }
}

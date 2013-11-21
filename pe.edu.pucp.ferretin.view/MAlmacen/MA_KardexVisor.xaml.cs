using pe.edu.pucp.ferretin.model;
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
    /// Lógica de interacción para KardexVisor.xaml
    /// </summary>
    public partial class MA_KardexVisor : Window
    {
        ReporteKardex kardex;
        public MA_KardexVisor(DateTime fechaDesde, DateTime fechaHasta, Tienda almacen)
        {
            InitializeComponent();
            kardex = new ReporteKardex();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);
            kardex.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            kardex.Refresh();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);
            
            KardexVisor.ViewerCore.ReportSource = kardex;
        }

        private void KardexVisor_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            kardex.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            
        }
    }
}

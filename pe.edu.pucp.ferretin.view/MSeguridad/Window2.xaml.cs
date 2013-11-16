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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        CrystalReport2 cryrep;

        public Window2()
        {
            InitializeComponent();
            CrystalReport2 cr = new CrystalReport2();
            cryrep = cr;
            VisorDelReporte.ViewerCore.ReportSource = cryrep;
        }

        private void VisorDelReporte_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            cryrep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }
    }
}

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
    /// Interaction logic for MS_ReportesWindow.xaml
    /// </summary>
    public partial class MS_ReportesWindow : Window
    {
        public MS_ReportesWindow()
        {
            InitializeComponent();
        }

        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(reportesBaseListBox.SelectedIndex.ToString());

            if (reportesBaseListBox.SelectedIndex.ToString() == "0")
            {

                MS_ReporteTrasaccionesParametrosWindow RTW = new MS_ReporteTrasaccionesParametrosWindow();
                RTW.Show();
            }
            else
            {
                MessageBox.Show("Seleccione un reporte.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

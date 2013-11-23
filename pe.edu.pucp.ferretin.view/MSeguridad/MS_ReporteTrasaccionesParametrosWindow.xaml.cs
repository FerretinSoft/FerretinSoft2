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
    /// Interaction logic for MS_ReporteTrasaccionesParametrosWindow.xaml
    /// </summary>
    public partial class MS_ReporteTrasaccionesParametrosWindow : Window
    {
        DateTime fechaInicial;
        DateTime fechaFinal;
        DateTime fechaDefecto;

        public MS_ReporteTrasaccionesParametrosWindow()
        {
            InitializeComponent();
        }

        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((fechaInicial != fechaDefecto && fechaFinal != fechaDefecto) && (fechaInicial < fechaFinal) && (fechaFinal <= System.DateTime.Now))
            {
                fechaFinal = fechaFinal.AddDays(1);
                MS_ReporteWindow RTW = new MS_ReporteWindow(fechaInicial, fechaFinal);
                RTW.ShowDialog();
                fechaFinal = fechaFinal.AddDays(-1);
            }
            else
            {

                if (fechaInicial == fechaDefecto || fechaFinal == fechaDefecto)
                {
                    MessageBox.Show("Seleccione una fecha inicial y una fecha final.");
                    return;
                }

                if (fechaInicial > fechaFinal)
                {
                    MessageBox.Show("La fecha inicial no puede ser posterior a la fecha final.");
                    return;
                }

                if (fechaFinal > System.DateTime.Now)
                {
                    MessageBox.Show("La fecha final no puede ser posterior a la fecha de actual.");
                    return;
                }

                if (fechaInicial == fechaFinal)
                {
                    MessageBox.Show("La fecha inicial no puede ser igual a la fecha final.");
                    return;
                }

            }

        }

        private void finicialpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaInicial = finicialpicker.SelectedDate.Value;
        }

        private void ffinalpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaFinal = ffinalpicker.SelectedDate.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using pe.edu.pucp.ferretin.viewmodel.MCompras;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_DatosMailWindow.xaml
    /// </summary>
    public partial class MC_DatosMailWindow : Window
    {
        MC_VisorReporteOC visor;
        public MC_DatosMailWindow(MC_VisorReporteOC visor)
        {
            InitializeComponent();
            this.visor = visor;
        }

        private void Button_Click_Mail(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MC_ReportesViewModel;

            if (String.IsNullOrEmpty(vm.emailEnviar))
            {
                MessageBox.Show("Debe ingresar el email de un destinatario", "ALERTA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MailAddress m;
            try
            {
                m = new MailAddress(vm.emailEnviar);
            }
            catch
            {
                MessageBox.Show("El email ingresado no es válido", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            visor.enviarEmail(vm._emailEnviar, vm.mensajeEnviar);
            this.Close();
        }
    }
}

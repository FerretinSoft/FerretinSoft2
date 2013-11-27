using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_DatosMailWindow.xaml
    /// </summary>
    public partial class MA_DatosMailWindow : Window
    {
        MA_KardexVisor visor;
        public MA_DatosMailWindow(MA_KardexVisor visor)
        {
            InitializeComponent();
            this.visor = visor;
        }

        private void Button_Click_Mail(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MA_ReportesViewModel;

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
            visor.enviarEmail(vm.emailEnviar, vm.mensajeEnviar);
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DatosMailWindow.xaml
    /// </summary>
    public partial class MV_DatosMailWindow : Window
    {
        public MV_DatosMailWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MV_ComunService.Clean();

        }

        private void Button_Click_Mail(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_ReportesViewModel;

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
            MV_VisorReporte print = new MV_VisorReporte();
            if (vm.selectedReport == 2)
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, vm.selectedTienda.id, "RTienda", "", "", "", vm.emailEnviar, vm.mensajeEnviar);
            else if (vm.selectedReport == 1)
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, vm.selectedTienda.id, "RProducto", "", "", vm.searchProducto, vm.emailEnviar, vm.mensajeEnviar);
            else if (vm.selectedReport == 0)
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, 0, "RCliente", "", vm.searchCliente, "", vm.emailEnviar, vm.mensajeEnviar);
            else if (vm.selectedReport == 4)
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, vm.selectedTienda.id, "RDevolucion", "", "", "", vm.emailEnviar, vm.mensajeEnviar);
            else if (vm.selectedReport == 5)
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, vm.selectedTienda.id, "RServicios", "", vm.searchCliente, "", vm.emailEnviar, vm.mensajeEnviar);
            
            else
                print.enviarEmail(vm.searchFechaInicio, vm.searchFechaFin, 0, "RVendedor", vm.searchVendedor, "", "", vm.emailEnviar, vm.mensajeEnviar);
            this.Close();
        }
    }
}
